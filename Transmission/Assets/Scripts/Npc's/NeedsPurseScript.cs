using System.Collections;
using System.Collections.Generic;
using Transmission;
using UnityEngine;

public class NeedsPurseScript : NpcBrain
{
    public Vector2 basePosition;
    public Vector2 openPosition;

    protected override void Start()
    {
        base.Start();
        speed = 1;
        movement.SetFacingDirection(Vector2.up, false);
    }

    public override string getInitialText()
    {
        if (PlayerController.Instance.ItemCheck("purse") > 0)
        {
            stateNumber = 1;
        }
        return "Have you seen my purse?";
    }

    public override string getTextAndSendReply(int choice)
    {
        switch (stateNumber)
        {
            case 0:
                return "endDialog()";
            case 1:
                stateNumber = 2;
                return "Thank you!";
            case 2:
                MoveToPoint(openPosition);
                return "endDialog()";
            default:
                return "error";
        }
    }

    public override string getChoiceText(int choiceNr)
    {
        switch (stateNumber)
        {
            case 0:
                switch (choiceNr)
                {
                    case 0:
                        return "I haven't...";
                    case 1:
                        return "Why should I care?";
                    case 2:
                        return "";
                    default:
                        return "error";
                }
            case 1:
                switch (choiceNr)
                {
                    case 0:
                        return "It's mine now!";
                    case 1:
                        return "Why should I care?";
                    case 2:
                        return "Here you go!";
                    default:
                        return "error";
                }
            case 2:
                switch (choiceNr)
                {
                    case 0:
                        return "You're welcome!";
                    case 1:
                        return "I should've kept it...";
                    case 2:
                        return "";
                    default:
                        return "error";
                }
            default:
                return "error";
        }
    }
}
