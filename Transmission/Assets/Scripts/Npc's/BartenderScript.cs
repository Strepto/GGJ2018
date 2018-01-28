using System.Collections;
using System.Collections.Generic;
using Transmission;
using UnityEngine;

public class BartenderScript : NpcBrain
{
    public PickupItem money;
    public PickupItem vodka;
    public PickupItem cosmopolitan;

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
        return "What can I do for you?";
    }

    public override string getTextAndSendReply(int choice)
    {
        switch (state)
        {
            case "initialState":
                if (choice == 1)
                {
                    state = "buyDrink";
                    return "Of course, what do you want?";
                }
                if (choice == 2)
                {
                    return "endDialog()";
                }
                else
                {
                    state = "gossip";
                    if (hasGivenQuest)
                    {
                        return "My poor friend...";
                    }
                    return "A friend of mine is being bullied. He's having a really hard time...";
                }
            case "hasSavedVictim":
                if (choice == 2)
                {
                    state = "thanks";
                    PickupItem newMoney = Instantiate(money);
                    newMoney.SetAmount(500);
                    PlayerController.Instance.ItemGiveToPlayer(Instantiate(money));
                    return "Thank you! Take this as a reward!";
                }
                if (choice == 1)
                {
                    state = "buyDrink";
                    return "Of course, what do you want?";
                }
                else
                {
                    state = "gossip";
                    return "A friend of mine is being bullied. He's having a really hard time...";
                }
            case "gossip":
                if (choice == 0)
                {
                    hasGivenQuest = true;
                    return "Thank you! If you can help him, I'll give you a reward!";
                }
                else
                {
                    return "endDialog()";
                }
            case "buyDrink":
                if (choice == 0)
                {
                    return "endDialog()";
                }
                if (choice == 1)
                {
                    if (PlayerController.Instance.ItemCheck("money") >= 5)
                    {
                        PlayerController.Instance.ItemTake("money", 5);
                        PlayerController.Instance.ItemGiveToPlayer(Instantiate(vodka));
                        state = "hasBoughtDrink";
                        return "Here you go! Anything else?";
                    }
                    state = "initialState";
                    if (PlayerController.Instance.CurrentPlayerState == PlayerController.PlayerState.Boy)
                    {
                        return "You don't have enough cash sonny!";
                    }
                    return "You don't have enough cash lassy!";
                }
                else
                {
                    if (PlayerController.Instance.ItemCheck("money") >= 10)
                    {
                        PlayerController.Instance.ItemTake("money", 10);
                        PlayerController.Instance.ItemGiveToPlayer(Instantiate(cosmopolitan));
                        state = "hasBoughtDrink";
                        return "Here you go! Anything else?";
                    }
                    state = "initialState";
                    if (PlayerController.Instance.CurrentPlayerState == PlayerController.PlayerState.Boy)
                    {
                        return "You don't have enough cash sonny!";
                    }
                    return "You don't have enough cash lassy!";
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
                        return "Got any gossip?";
                    case 1:
                        return "Can I have a drink?";
                    case 2:
                        return "Never mind...";
                    default:
                        return "error";
                }
            case "hasSavedVictim":
                switch (choiceNr)
                {
                    case 0:
                        return "Got any gossip?";
                    case 1:
                        return "Can I have a drink?";
                    case 2:
                        return "Your friend has been saved!";
                    default:
                        return "error";
                }
            case "thanks":
                switch (choiceNr)
                {
                    case 0:
                        return "You're welcome!";
                    case 1:
                        return "Thank you!";
                    case 2:
                        return "";
                    default:
                        return "error";
                }
            case "buyDrink":
                switch (choiceNr)
                {
                    case 0:
                        return "I've changed my mind.";
                    case 1:
                        return "I want some vodka (5$)";
                    case 2:
                        return "Give me a cosmopolitan! (10$)";
                    default:
                        return "error";
                }
            case "hasBoughtDrink":
                state = "buyDrink";
                switch (choiceNr)
                {
                    case 0:
                        return "No, that's all";
                    case 1:
                        return "I want some vodka (5$)";
                    case 2:
                        return "Give me a cosmopolitan! (10$)";
                    default:
                        return "error";
                }
            case "gossip":
                switch (choiceNr)
                {
                    case 0:
                        return "Maybe I can help!";
                    case 1:
                        return "That's too bad...";
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
