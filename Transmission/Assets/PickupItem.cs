using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour {

    public string ItemName = "Item Name";
    public string ItemKey = "testitem";
    public AudioClip audioClip;

	void Start () {
        
	}
	
    public void PrepareForPickup()
    {
        AudioSource.PlayClipAtPoint(audioClip, this.transform.position - new Vector3(0,0,0), 1f);

        gameObject.SetActive(false);
    }


}
