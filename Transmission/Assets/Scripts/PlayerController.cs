using SeedValue;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Transmission
{

    [RequireComponent(typeof(SimpleSpriteAnimator))]
    public class PlayerController : Singleton<PlayerController>
    {
        private const float JoystickMovementThreshold = 0.1f;
        public float moveSpeed = 2.0f;
        public InteractionZoneController interactionZoneController;

        public Dictionary<string, List<PickupItem>> PlayerItems = new Dictionary<string, List<PickupItem>>();
        public MusicManager musicManager;

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
            musicManager.SwitchGenderSounds(CurrentPlayerState);
        }
        
        public int ItemCheck(string itemKey)
        {
            List<PickupItem> list;
            if (PlayerItems.TryGetValue(itemKey, out list))
            {
                return list.Sum(x => x.Amount);
            }
            return 0;
        }

        public bool ItemTake(string itemKey, int count = 1)
        {
            if (ItemCheck(itemKey) < count)
            {
                return false;
            }
            int amountToRemove = count;
            var itemList = PlayerItems[itemKey];
            for (int i = itemList.Count -1 ; i >= 0 ; i--)
            {
                var item = itemList[i];
                int saldo = item.RemoveAmount(amountToRemove);
                if(item.Amount == 0)
                {
                    itemList.Remove(item);
                }
                amountToRemove = Mathf.Abs(saldo);
                if(amountToRemove <= 0)
                {
                    break;
                }
            };

            if(itemList.Count == 0)
            {
                PlayerItems.Remove(itemKey);
            }
            return true;
        }

        public void ItemGiveToPlayer(PickupItem item)
        {
            ReceiveItem(item);
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

                musicManager.SwitchGenderSounds(CurrentPlayerState);
            }
            else if (Input.GetButtonDown("Jump") )
            {
                Debug.Log("Jump" + movementHandler.CurrentDirection);
                var result = Physics2D.BoxCast(currentPosition, Vector2.one, 0f, movementHandler.CurrentDirection, 3f, LayerMask.GetMask("Interactables"));    
                if(result)
                {
                    var isNpc = result.collider.gameObject.GetComponent<NpcBrain>();
                    if (isNpc != null)
                    {
                        isNpc.PlayerInitiatedDialog();
                    }

                    var isPickupItem = result.collider.gameObject.GetComponent<PickupItem>();
                    ReceiveItem(isPickupItem);
                }
            }
            
        }

        private void ReceiveItem(PickupItem isPickupItem)
        {
            if (isPickupItem != null)
            {
                isPickupItem.PrepareForPickup();
                string itemKey = isPickupItem.ItemKey;

                List<PickupItem> list;
                if (!PlayerItems.TryGetValue(itemKey, out list))
                {
                    list = new List<PickupItem>();
                    PlayerItems[isPickupItem.ItemKey] = list;
                }
                PlayerItems[isPickupItem.ItemKey].Add(isPickupItem);
            }
        }
    }

}
