using SeedValue;
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
        public InteractionZoneController interactionZoneController;

        public List<PickupItem> playerItems = new List<PickupItem>();

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
                // No player interaction during active dialog.
                return;
            }

            Vector3 currentPosition = transform.position;

            float movementX = Input.GetAxis("Horizontal");
            float movementY = Input.GetAxis("Vertical");
            var movement = new Vector2(movementX, movementY);
            Vector3 moveToward = currentPosition + new Vector3(movement.x, movement.y);
            if (movement.magnitude > JoystickMovementThreshold)
            {
                // 3
                // 4
                moveDirection = moveToward - currentPosition;
                moveDirection.z = 0;
                moveDirection.Normalize();

                Vector3 target = moveDirection * 0.1f * moveSpeed + currentPosition;
                movementHandler.MoveToPosition(target, moveSpeed);
                //transform.position = Vector3.Lerp(currentPosition, target, Time.deltaTime);
            }
            else if (Input.GetButtonDown("Submit"))
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
            else if (Input.GetButtonDown("Jump") )
            {
                var result = Physics2D.BoxCast(currentPosition, Vector2.one, 0f, movementHandler.CurrentDirection, 0.5f, LayerMask.GetMask("Interactables"));    
                if(result)
                {
                    var isNpc = result.collider.gameObject.GetComponent<NpcBehaviourScript>();
                    if(isNpc != null)
                    {
                        isNpc.PlayerInitiatedDialog();
                    }

                    var isPickupItem = result.collider.gameObject.GetComponent<PickupItem>();
                    if (isPickupItem != null)
                    {
                        isPickupItem.PrepareForPickup();
                        playerItems.Add(isPickupItem);
                    }
                }
            }
            
        }
    }

}
