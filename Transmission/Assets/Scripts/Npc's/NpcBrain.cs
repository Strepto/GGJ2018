using System.Collections;
using System.Collections.Generic;
using Transmission;
using UnityEngine;

public class NpcBrain : MonoBehaviour {
    
    protected int stateNumber;
    protected float speed;

    protected int pointsLeft = 0;
    protected Vector2[] pointsToMove;

    public Vector2 InitialFacingDirection;

    public MovementHandler movement;
    public DialogHandler dialog;


    protected virtual void Start()
    {
        stateNumber = 0;
        StartCoroutine(IntializeAfterOneFrame());
    }

    protected IEnumerator IntializeAfterOneFrame()
    {
        yield return null;
        movement.SetCurrentDirection(InitialFacingDirection);
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
        pointsLeft = 0;
    }

    protected void MoveToPoint(Vector2 point, Vector2 endDirection)
    {
        movement.MoveToPosition(point, speed, endDirection, callback: HasMovedToPoint);
        pointsLeft = 0;
    }

    protected void MoveToPoint(Vector2[] points)
    {
        pointsLeft = points.Length - 1;
        pointsToMove = points;
        movement.MoveToPosition(points[0], speed, callback: HasMovedToPoint);
    }

    protected void MoveToPoint(Vector2[] points, Vector2 endDirection)
    {
        pointsLeft = points.Length - 1;
        pointsToMove = points;
        movement.MoveToPosition(points[0], speed, callback: HasMovedToPoint);
    }

    protected void HasMovedToPoint(bool interrupted, float timeUsed)
    {
        if (pointsLeft > 0)
        {
            movement.MoveToPosition(pointsToMove[pointsToMove.Length - pointsLeft], speed, callback: HasMovedToPoint);
            pointsLeft--;
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
