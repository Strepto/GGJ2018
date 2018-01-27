using SeedValue;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Transmission
{
    [RequireComponent(typeof(SimpleSpriteAnimator))]
    public class MovementHandler : MonoBehaviour
    {
        private const float JoystickMovementThreshold = 0.1f;

        public Vector2 CurrentTarget { get; private set; }

        public Vector2 CurrentDirection { get; private set; }


        Action<bool, float> localCallback;
        Action<bool, float> callbackToCall;

        public enum MovementState
        {
            Idle,
            Stopped,
            Moving
        }

        public MovementState CurrentMovementState { get; private set; }

        float timeStartedMoving = 0f;
        bool hasFinishedMoving = false;
        float moveSpeed = 1.0f;

        private SimpleSpriteAnimator spriteAnimator;
        private PlayerController playerController;

        private Coroutine currentCallbackCoroutine;

        public void Start()
        {
            CurrentMovementState = MovementState.Idle;
            this.spriteAnimator = GetComponent<SimpleSpriteAnimator>();

            this.playerController = GetComponent<PlayerController>();
        }

        public void MoveToPosition(Vector2 targetPos, float speed = 2.0f, bool collide = true, Action<bool, float> callback = null)
        {
            CurrentMovementState = CurrentMovementState = MovementState.Moving;
            CurrentTarget = targetPos;
            if (callback != null)
            {
                localCallback = callback;
                callbackToCall = callback;
            }
            hasFinishedMoving = false;
            timeStartedMoving = Time.deltaTime;
            moveSpeed = speed;
            if (currentCallbackCoroutine != null)
            {
                StopCoroutine(currentCallbackCoroutine);
            }

            currentCallbackCoroutine = StartCoroutine(CallbackCoroutine(callback));
        }

        public IEnumerator CallbackCoroutine(Action<bool, float> callback)
        {
            while (!hasFinishedMoving)
            {
                yield return null;
            }
            if (callback != null)
            {
                bool status = CurrentMovementState == MovementState.Stopped;
                callback(status, Time.deltaTime - timeStartedMoving);
            }
            CurrentMovementState = MovementState.Idle;
        }

        public void Stop()
        {
            CurrentMovementState = MovementState.Stopped;
            hasFinishedMoving = true;
            CurrentTarget = transform.position;
        }

        void Update()
        {
            Vector3 position = transform.position;
            if (!hasFinishedMoving && Vector3.Distance(transform.position, CurrentTarget) > 0.1f)
            {
                this.transform.position = Vector3.MoveTowards(transform.position, CurrentTarget, 0.1f);
            }
            else if (!hasFinishedMoving)
            {
                hasFinishedMoving = true;
            }

            AnimateMovement(position);
        }

        private void AnimateMovement(Vector3 position)
        {

            string animExtension = string.Empty;
            if (playerController != null)
            {
                animExtension = playerController.CurrentPlayerState.ToString();
            }

            var moveDirection = new Vector3(CurrentTarget.x, CurrentTarget.y) - position;
            moveDirection.z = 0;
            moveDirection.Normalize();

            var movement = new Vector2(moveDirection.x, moveDirection.y);
            if (movement.magnitude > 0.1f)
            {

                if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
                {

                    if (movement.x > JoystickMovementThreshold)
                    {
                        CurrentDirection = Vector2.right;
                        spriteAnimator.PlayAnimation("WalkRight" + animExtension);
                    }
                    else if (movement.x < -JoystickMovementThreshold)
                    {
                        CurrentDirection = Vector2.left;
                        spriteAnimator.PlayAnimation("WalkLeft" + animExtension);
                    }
                }
                else
                {
                    if (movement.y > JoystickMovementThreshold)
                    {

                        CurrentDirection = Vector2.up;
                        spriteAnimator.PlayAnimation("WalkUp" + animExtension);
                    }
                    else if (movement.y < JoystickMovementThreshold)
                    {
                        CurrentDirection = Vector2.down;
                        spriteAnimator.PlayAnimation("WalkDown" + animExtension);

                    }
                }
            }
            else
            {

                spriteAnimator.PlayAnimation("Idle" + animExtension);
            }

        }
    }
}
