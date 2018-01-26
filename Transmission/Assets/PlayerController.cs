﻿using SeedValue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SimpleSpriteAnimator))]
public class PlayerController : MonoBehaviour
{
    private const float JoystickMovementThreshold = 0.1f;
    public float moveSpeed = 1.0f;

    private Vector3 moveDirection = Vector3.zero;
    private SimpleSpriteAnimator spriteAnimator;

    // Use this for initialization
    void Start()
    {
        spriteAnimator = GetComponent<SimpleSpriteAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 1
        Vector3 currentPosition = transform.position;
        // 2
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

            if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
            {

                if (movement.x > JoystickMovementThreshold)
                {
                    spriteAnimator.PlayAnimation("WalkRight");
                }
                else if (movement.x < -JoystickMovementThreshold)
                {
                    spriteAnimator.PlayAnimation("WalkLeft");
                }
                else
                {

                    spriteAnimator.PlayAnimation("Idle");
                }
            }
            else
            {
                if (movement.y > JoystickMovementThreshold)
                {

                    spriteAnimator.PlayAnimation("WalkUp");
                }
                else if (movement.y < JoystickMovementThreshold)
                {
                    spriteAnimator.PlayAnimation("WalkDown");

                }
                else
                {
                    spriteAnimator.PlayAnimation("Idle");

                }
            }

            Vector3 target = moveDirection * moveSpeed + currentPosition;
            transform.position = Vector3.Lerp(currentPosition, target, Time.deltaTime);
        }
        else if (Input.GetButton("Submit"))
        {

            spriteAnimator.PlayAnimation("Interaction");


        }
        else
        {

            spriteAnimator.PlayAnimation("Idle");
        }


    }
}
