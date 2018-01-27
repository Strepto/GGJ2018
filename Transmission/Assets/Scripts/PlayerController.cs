﻿using SeedValue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Transmission
{

    [RequireComponent(typeof(SimpleSpriteAnimator))]
    public class PlayerController : Singleton<PlayerController>
    {
        private const float JoystickMovementThreshold = 0.1f;
        public float moveSpeed = 2.0f;


        public enum PlayerState
        {
            Boy,
            Girl
        }

        public PlayerState CurrentPlayerState = PlayerState.Boy;

        private Vector3 moveDirection = Vector3.zero;
        private SimpleSpriteAnimator spriteAnimator;
        private MovementHandler movementHandler;
        
        void Start()
        {
            spriteAnimator = GetComponent<SimpleSpriteAnimator>();
            movementHandler = GetComponent<MovementHandler>();
        }
        

        void Update()
        {

            if (DialogWindow.Instance.ActiveDialog)
            {
                // No movement during active dialog.
                return;
            }

            Vector3 currentPosition = transform.position;

            float movementX = Input.GetAxis("Horizontal");
            float movementY = Input.GetAxis("Vertical");
            var movement = new Vector2(movementX, movementY);
            if (movement.magnitude > JoystickMovementThreshold)
            {
                // 3
                Vector3 moveToward = currentPosition + new Vector3(movement.x, movement.y);
                // 4
                moveDirection = moveToward - currentPosition;
                moveDirection.z = 0;
                moveDirection.Normalize();

                Vector3 target = moveDirection * 0.1f * moveSpeed + currentPosition;
                movementHandler.MoveToPosition(target, moveSpeed);
                //transform.position = Vector3.Lerp(currentPosition, target, Time.deltaTime);
            }
            else if (Input.GetButton("Submit"))
            {
                if (CurrentPlayerState == PlayerState.Boy)
                {
                    CurrentPlayerState = PlayerState.Girl;
                    spriteAnimator.PlayAnimation("SwitchBoyGirl");
                }
                else
                {
                    CurrentPlayerState = PlayerState.Boy;
                    spriteAnimator.PlayAnimation("SwitchGirlBoy");
                }
            }
        }
    }

}
