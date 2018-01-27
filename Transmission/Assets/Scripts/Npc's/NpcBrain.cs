using System.Collections;
using System.Collections.Generic;
using Transmission;
using UnityEngine;

public class NpcBrain : MonoBehaviour {
    
    protected int stateNumber;

    public MovementHandler movement;
    public DialogHandler dialog;

    public Vector2[] pointList;
    protected int pointInList = 0;
    public float speed = 1;

    void Start()
    {
        MoveToPoint(pointList[pointInList]);
        stateNumber = 0;
    }

    void Update()
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
        movement.MoveToPosition(point, speed, callback: HasMovedToPoint);
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


    



    public virtual string getInitialText()
    {
        return "error";
    }

    public virtual string getTextAndSendReply(int choice)
    {
        return "error";
    }


    public virtual string getChoiceText(int choiceNr)
    {
        return "error";
    }
}
