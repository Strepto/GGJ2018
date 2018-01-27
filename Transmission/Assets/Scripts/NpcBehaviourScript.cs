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
    public Collider2D triggerCollider;
    
    void Start ()
    {
        MoveToPoint(pointList[pointInList]);
    }

	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Stop();
            dialog.StartDialog();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MoveToPoint(pointList[pointInList]);
        }
    }

    void Stop()
    {
        //movement.Stop();
    }

    void MoveToPoint(Vector2 point)
    {
        movement.MoveToPosition(point, speed, callback:HasMovedToPoint);
    }

    void HasMovedToPoint(bool hasReached, float timeUsed)
    {
        pointInList++;
        if (pointInList >= pointList.Length)
        {
            pointInList = 0;
        }

        MoveToPoint(pointList[pointInList]);
    }
}
