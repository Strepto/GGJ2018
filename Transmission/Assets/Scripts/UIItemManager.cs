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
    int frameInteraval = 30;
    void Update()
    {
        if (Time.frameCount % 30 == 0)
        {
            this.transform.DestroyAllChildren();
            foreach(var kvp in PlayerController.Instance.PlayerItems)
            {
                var itemGo = Instantiate(ItemPrefab, this.transform);
                var uiItemEntity = itemGo.GetComponent<UIItemEntity>();
                var firstItem = kvp.Value.First();
                uiItemEntity.Initialize(firstItem.ItemKey, firstItem.ItemName, firstItem.SpriteRenderer.sprite);
                var amount = kvp.Value.Sum(x => x.Amount);
                if(amount > 0)
                {
                    uiItemEntity.UpdateCount(amount);
                }
            }

            

            //foreach (var list in allLists)
            //{
            //    list.ForEach(x =>
            //    {
            //        if (!allItemsPickedUp.Contains(x))
            //        {
            //            allItemsPickedUp.Add(x);
            //        }
            //    });
            //}
        }
    }
}
