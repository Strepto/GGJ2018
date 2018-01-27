using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PickupItem : MonoBehaviour {

    public string ItemName = "Item Name";
    public string ItemKey = "testitem";
    [SerializeField]
    private int _amount = 1;
    public int Amount
    {
        get
        {
            return _amount;
        }
    }

    public int RemoveAmount(int max)
    {
        if(Amount - max >= 0)
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

    public AudioClip audioClip;

	void Start () {
        if(Amount < 1)
        {
            throw new System.Exception("PickupItem Amount can not be less than 1");
        }
	}
	
    public void PrepareForPickup()
    {
        AudioSource.PlayClipAtPoint(audioClip, this.transform.position - new Vector3(0,0,0), 1f);

        gameObject.SetActive(false);
    }


}
