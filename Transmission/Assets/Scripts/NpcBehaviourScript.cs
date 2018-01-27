using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcBehaviourScript : MonoBehaviour {

    public MovementHandler movement;
    public DialogHandler dialog;

    public Vector2[] pointList;
    private int pointInList = 0;
    public Collider2D triggerCollider;
    
    void Start () {

    }

	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Stop();
            dialog.StartDialog();
        }
    }

    private void OnTriggerExit2D(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MoveToPoint(pointList[pointInList]);
        }
    }

    void Stop()
    {
        movement.Stop();
    }

    void MoveToPoint(Vector2 point)
    {
        movement.moveToPoint(point, HasMovedToPoint);
    }

    void HasMovedToPoint()
    {
        pointInList++;
        if (pointInList > pointList.Length)
        {
            pointInList = 0;
        }

        MoveToPoint(pointList[pointInList]);
    }
}
