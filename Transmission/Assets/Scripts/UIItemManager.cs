using Assets.Scripts.Helpers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Transmission;
using UnityEngine;

public class UIItemManager : MonoBehaviour
{

    public GameObject ItemPrefab;
    public List<PickupItem> allItemsPickedUp = new List<PickupItem>();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    int frameInteraval = 60;
    void Update()
    {
        if (Time.frameCount % frameInteraval == 0)
        {
            this.transform.DestroyAllChildren();
            foreach(var kvp in PlayerController.Instance.PlayerItems)
            {
                var firstItem = kvp.Value.First();
                if (firstItem.IsVisibleInInventory)
                {
                    var itemGo = Instantiate(ItemPrefab, this.transform);
                    var uiItemEntity = itemGo.GetComponent<UIItemEntity>();
                    uiItemEntity.Initialize(firstItem.ItemKey, firstItem.ItemName, firstItem.SpriteRenderer.sprite);
                    var amount = kvp.Value.Sum(x => x.Amount);
                    if (amount > 0)
                    {
                        uiItemEntity.UpdateCount(amount);
                    }
                }
            }
        }
    }
}
