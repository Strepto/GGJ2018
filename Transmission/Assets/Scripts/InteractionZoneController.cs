using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionZoneController : MonoBehaviour {


    public float offsetDistance = 1f;
    public Collider2D collider2D;
	// Use this for initialization
	void Start () {
		if(collider2D == null)
        {
            throw new System.Exception("Interaction Zone Contrller need a collider 2d object assigned.");
        }

	}

    public void UpdateColliderPosition(Vector2 direction)
    {
        collider2D.offset = direction * offsetDistance;
    }

    public void CheckIfTouchingObject()
    {

        //if (collider2D.IsTouchingLayers(LayerMask.GetMask("GameController")))
        //{
        //    Collider2D[] results = new Collider2D[1];
        //    var contactFilterer = new ContactFilter2D();
        //    contactFilterer.

        //    collider2D.OverlapCollider(, results);
        //}
        
    }
}
