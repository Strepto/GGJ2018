using System.Collections;
using System.Collections.Generic;
using Transmission;
using UnityEngine;

public class BartenderScript : NpcBrain
{
    public PickupItem money;

    private bool hasGivenQuest = false;

    protected override void Start()
    {
        base.Start();
        speed = 1;
    }

    public override string getInitialText()
    {
        if (PlayerController.Instance.ItemCheck("savedVictim") > 0)
        {
            state = "hasSavedVictim";
        }
        if (hasGivenQuest)
        {
            return "Have you saved him yet?";
        }
        hasGivenQuest = true;
        return "A friend of mine is being bullied, can you save him for me?";
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
                    PickupItem newMoney = Instantiate(money);
                    //newMoney.Amount = 500;
                    PlayerController.Instance.ItemGiveToPlayer(Instantiate(money));
                    return "Thank you! Take this as a reward!";
                }
                else
                {
                    return "endDialog()";
                }
            case "thanks":
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
