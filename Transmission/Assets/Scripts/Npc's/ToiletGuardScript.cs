using System.Collections;
using System.Collections.Generic;
using Transmission;
using UnityEngine;

public class ToiletGuardScript : NpcBrain {

    public Trigger ladiesRoomTrigger;

    public Vector2 basePosition;
    public Vector2 blockLadiesRoom;
    public Vector2 blockMensRoom;

    protected override void Start()
    {
        base.Start();
        ladiesRoomTrigger.onTriggerEnter += ladiesRoomOnEnterTriggered;
        ladiesRoomTrigger.onTriggerExit += ladiesRoomOnExitTriggered;
        speed = 1;
        MoveToPoint(basePosition);
    }

    private void ladiesRoomOnEnterTriggered(Trigger source)
    {
        if (stateNumber != -1)
        {
            MoveToPoint(blockLadiesRoom);
            speed = 2;
        }
    }

    private void ladiesRoomOnExitTriggered(Trigger source)
    {
        if (stateNumber != -1)
        {
            MoveToPoint(basePosition);
            speed = 1;
        }
    }

    public override string getInitialText()
    {

        if (PlayerController.Instance.CurrentPlayerState == PlayerController.PlayerState.Boy)
        { 
            stateNumber = 1;
            return "You cannot enter here!";
        }
        else
        {
            stateNumber = -1;
            MoveToPoint(basePosition);
            speed = 1;
            return "You're free to pass, m'lady";
        }
    }

    public override string getTextAndSendReply(int choice)
    {
        switch (stateNumber)
        {
            case -1:
                return "endDialog()";
            case 1:
                stateNumber = 2;
                return "Because this is a girls-only bathroom!";
            case 2:
                stateNumber = 3;
                return "Stop wasting my time! Shoo!";
            case 3:
                return "endDialog()";
            default:
                return "error";
        }
    }

    public override string getChoiceText(int choiceNr)
    {
        switch (stateNumber)
        {
            case -1:
                switch (choiceNr)
                {
                    case 0:
                        return "Thank you!";
                    case 1:
                        return "Obviously.";
                    case 2:
                        return "M'lady?...";
                    default:
                        return "error";
                }
            case 1:
                switch (choiceNr)
                {
                    case 0:
                        return "Why not?";
                    case 1:
                        return "";
                    case 2:
                        return "";
                    default:
                        return "error";
                }
            case 2:
                switch (choiceNr)
                {
                    case 0:
                        return "But...";
                    case 1:
                        return "C'mon man!";
                    case 2:
                        return "What if I give you 100$?";
                    default:
                        return "error";
                }
            case 3:
                switch (choiceNr)
                {
                    case 0:
                        return "Dammit!";
                    case 1:
                        return "Screw you!";
                    case 2:
                        return "Okay...";
                    default:
                        return "error";
                }
            default:
                return "error";
        }
    }
}
