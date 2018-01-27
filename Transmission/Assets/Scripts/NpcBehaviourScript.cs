using System.Collections;
using System.Collections.Generic;
using Transmission;
using UnityEngine;

public class NpcBehaviourScript : MonoBehaviour {

    public MovementHandler movement;
    public DialogHandler dialog;

    public Vector2[] pointList;
    private int pointInList = 0;
    public float speed;
    
    void Start ()
    {
        MoveToPoint(pointList[pointInList]);
    }

	void Update ()
    {
		
	}

    public void PlayerInitiatedDialog()
    {
        startsDialog();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            startsDialog();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MoveToPoint(pointList[pointInList]);
        }
    }

    private void startsDialog()
    {
        Stop();
        dialog.startDialog();
    }

    void Stop()
    {
        movement.Stop();
    }

    void MoveToPoint(Vector2 point)
    {
        movement.MoveToPosition(point, speed, callback:HasMovedToPoint);
    }

    void HasMovedToPoint(bool interrupted, float timeUsed)
    {
        if (!interrupted)
        {
            pointInList++;
            if (pointInList >= pointList.Length)
            {
                pointInList = 0;
            }

            MoveToPoint(pointList[pointInList]);
        }

    }
}
