using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PickupItem : MonoBehaviour {

    public SpriteRenderer SpriteRenderer;
    public string ItemName = "Item Name";
    public string ItemKey = "testitem";

    public bool IsVisibleInInventory = true;
    
        
    [SerializeField]
    private int _amount = 1;
    public int Amount
    {
        get
        {
            return _amount;
        }
    }

    public void SetAmount(int amount)
    {
        _amount = amount;
    }

    public AudioClip audioClip;

	void Start () {
        if(Amount < 1)
        {
            throw new System.Exception("PickupItem Amount can not be less than 1");
        }
        if (ItemKey == "testitem")
        {
            Debug.LogWarning("Warning!: PickupItem at GameoBject " + gameObject.name + "has not been assigned a custom itemKey.");
        }
    }
	
    public void PrepareForPickup()
    {
        AudioSource.PlayClipAtPoint(audioClip, this.transform.position - new Vector3(0,0,0), 1f);

        gameObject.SetActive(false);
    }



    public int RemoveAmount(int max)
    {
        if (Amount - max >= 0)
        {
            _amount = Amount - max;
            return _amount;
        }
        else
        {
            _amount = 0;
            return Amount - max;
        }
    }
}
