using System.Collections;
using System.Collections.Generic;
using Transmission;
using UnityEngine;

public class NpcBrain : MonoBehaviour {
    
    protected int stateNumber;
    protected float speed;

    public MovementHandler movement;
    public DialogHandler dialog;

    public Vector2 basePosition;
    public Vector2 blockLadiesRoom;
    public Vector2 blockMensRoom;

    protected virtual void Start()
    {
        MoveToPoint(basePosition);
        stateNumber = 0;
    }

    protected virtual void Update()
    {

    }

    public void PlayerInitiatedDialog()
    {
        startsDialog();
    }

    protected void startsDialog()
    {
        Stop();
        dialog.startDialog();
    }

    protected void Stop()
    {
        movement.Stop();
    }

    protected void MoveToPoint(Vector2 point)
    {
        movement.MoveToPosition(point, speed, callback: HasMovedToPoint);
    }

    protected void HasMovedToPoint(bool interrupted, float timeUsed)
    {

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
