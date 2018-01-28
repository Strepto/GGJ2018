using System.Collections;
using System.Collections.Generic;
using Transmission;
using UnityEngine;

public class BartenderScript : NpcBrain
{
    public Vector2 basePosition;
    public Vector2[] openPosition;

    public PickupItem money;

    private bool hasGivenQuest = false;

    protected override void Start()
    {
        base.Start();
        speed = 1;
    }

    public override string getInitialText()
    {
        if (PlayerController.Instance.ItemCheck("purse") > 0)
        {
            state = "hasPurse";
        }
        if (hasGivenQuest)
        {
            return "Did you find it?";
        }
        hasGivenQuest = true;
        return "I lost my purse in the ladies room, but the guard won't let me enter to fetch it. Can you help me please?";
    }

    public override string getTextAndSendReply(int choice)
    {
        switch (state)
        {
            case "initialState":
                return "endDialog()";
            case "hasPurse":
                if (choice == 2)
                {
                    state = "thanks";
                    PlayerController.Instance.ItemTake("purse");
                    PlayerController.Instance.ItemGiveToPlayer(Instantiate(money));
                    return "Thank you! Take this as a reward!";
                }
                else
                {
                    return "endDialog()";
                }
            case "thanks":
                MoveToPoint(openPosition, Vector2.down);
                return "endDialog()";
            default:
                return "error";
        }
    }

    public override string getChoiceText(int choiceNr)
    {
        switch (state)
        {
            case "initialState":
                switch (choiceNr)
                {
                    case 0:
                        return "Sure thing!";
                    case 1:
                        return "Why should I care?";
                    case 2:
                        return "";
                    default:
                        return "error";
                }
            case "hasPurse":
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
            case "thanks":
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
