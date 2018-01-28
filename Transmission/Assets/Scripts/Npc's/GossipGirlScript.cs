using System.Collections;
using System.Collections.Generic;
using Transmission;
using UnityEngine;

public class GossipGirlScript : NpcBrain
{
    public PickupItem rumor;

    private bool hasGivenRumor = false;
    private bool isInLove = false;

    private int drinks = 0;

    protected override void Start()
    {
        base.Start();
        speed = 1;
    }

    public override string getInitialText()
    {
        if (hasGivenRumor)
        {
            return "*Giggle* It's so funny..";
        }
        if (PlayerController.Instance.CurrentPlayerState == PlayerController.PlayerState.Boy)
        {
            if (drinks < 1)
            {
                state = "giveDrink";
                return "You're cute, are you gonna buy me a drink or what?";
            }
            if (drinks < 3)
            {
                state = "giveDrink";
                return "C'mon, get me one more!";
            }
            state = "tipsyBoy";
            return "I'm feeling tipsy... *giggle*";
        }
        if (drinks < 3)
        {
            state = "heyGirl"; 
            if (isInLove)
            {
                return  "Hi... You're pretty...";
            }
            return "Hey girl, anything juicy happening?";
        }
        state = "tipsyGirl";
        return "I'm feeling tipsy... *giggle*";
    }

    public override string getTextAndSendReply(int choice)
    {
        switch (state)
        {
            case "giveDrink":
                if (choice == 0)
                {
                    state = "gaveVodka";
                    return "Yuck! Who drinks that?!";
                }
                if (choice == 1)
                {
                    state = "gaveCosmopolitan";
                    PlayerController.Instance.ItemTake("cosmopolitan");
                    return "How did you know? Cosmopolitans are my favourite!";
                }
                else
                {
                    state = "goBuyDrink";
                    return "Then go buy me some, silly!";
                }
            case "gaveVodka":
                return "endDialog()";
            case "gaveCosmopolitan":
                return "endDialog()";
            case "goBuyDrink":
                return "endDialog()";
            case "tipsyBoy":
                if (choice == 0)
                {
                    state = "exit";
                    return "I'm not gonna tell you! *Giggle*";
                }
                if (choice == 1)
                {
                    state = "rejected";
                    return "Erm.. Ok... You're creepy...";
                }
                else
                {
                    state = "exit";
                    return "Mind your own business";
                }
            case "tipsyGirl":
                if (choice == 0)
                {
                    state = "givingGossip";
                    return "Okay, but you gotta keep it to yourself ok?";
                }
                if (choice == 1)
                {
                    state = "accepted";
                    isInLove = true;
                    return "... ... I kinda like you too...";
                }
                else
                {
                    state = "exit";
                    return "Mind your own business";
                }
            case "givingGossip":
                if (choice == 0)
                {
                    hasGivenRumor = true;
                    PlayerController.Instance.ItemGiveToPlayer(rumor);
                    return "*Whisper whisper* *Giggle*";
                }
                if (choice == 1)
                {
                    state = "exit";
                    return "Then I can't tell you...";
                }
                else
                {
                    state = "exit";
                    return "Mind your own business";
                }
            case "heyGirl":
                if (choice == 0)
                {
                    state = "exit";
                    return "Then why should I bother?";
                }
                if (choice == 1)
                {
                    state = "exit";
                    return "I'm not in a sharing mood";
                }
                else
                {
                    state = "exit";
                    return "*Blush*";
                }
            case "rejected":
                movement.SetCurrentDirection(-1*movement.CurrentDirection);
                return "endDialog()";
            case "accepted":
                return "endDialog()";
            case "exit":
                return "endDialog()";
            default:
                return "error";
        }
    }

    public override string getChoiceText(int choiceNr)
    {
        switch (state)
        {
            case "giveDrink":
                switch (choiceNr)
                {
                    case 0:
                        if (PlayerController.Instance.ItemCheck("vodka") > 0)
                        {
                            return "You can have this vodka";
                        }
                        return "";
                    case 1:
                        if (PlayerController.Instance.ItemCheck("cosmopolitan") > 0)
                        {
                            return "You can have a cosmopolitan";
                        }
                        return "";
                    case 2:
                        return "I don't have any to give...";
                    default:
                        return "error";
                }
            case "gaveVodka":
                switch (choiceNr)
                {
                    case 0:
                        return "Oh...";
                    case 1:
                        return "I do...";
                    case 2:
                        return "I'll get you something else";
                    default:
                        return "error";
                }
            case "heyGirl":
                switch (choiceNr)
                {
                    case 0:
                        return "Not really..";
                    case 1:
                        return "Can you tell me some gossip?";
                    case 2:
                        if (isInLove)
                        {
                            return "You're pretty too...";
                        }
                        return "";
                    default:
                        return "error";
                }
            case "gaveCosmopolitan":
                switch (choiceNr)
                {
                    case 0:
                        return "I'm psychic";
                    case 1:
                        return "Because I like you..";
                    case 2:
                        if (PlayerController.Instance.ItemCheck("cosmopolitan") > 0)
                        {
                            return "Want another?";
                        }
                        return "";
                    default:
                        return "error";
                }
            case "goBuyDrink":
                switch (choiceNr)
                {
                    case 0:
                        return "Ok..";
                    case 1:
                        return "I'm not gonna bother";
                    case 2:
                        return "Sure!";
                    default:
                        return "error";
                }
            case "tipsyBoy":
                switch (choiceNr)
                {
                    case 0:
                        return "Got any juicy gossip?";
                    case 1:
                        return "I love you";
                    case 2:
                        return "You should sober up!";
                    default:
                        return "error";
                }
            case "tipsyGirl":
                switch (choiceNr)
                {
                    case 0:
                        return "Got any juicy gossip?";
                    case 1:
                        return "I love you";
                    case 2:
                        return "You should sober up!";
                    default:
                        return "error";
                }
            case "rejected":
                switch (choiceNr)
                {
                    case 0:
                        return "Ok...";
                    case 1:
                        return "But!";
                    case 2:
                        return "May I see you again?";
                    default:
                        return "error";
                }
            case "exit":
                switch (choiceNr)
                {
                    case 0:
                        return "...";
                    case 1:
                        return "";
                    case 2:
                        return "";
                    default:
                        return "error";
                }
            case "givingGossip":
                switch (choiceNr)
                {
                    case 0:
                        return "Of course!";
                    case 1:
                        return "Can't promise anything";
                    case 2:
                        return "";
                    default:
                        return "error";
                }
            case "accepted":
                switch (choiceNr)
                {
                    case 0:
                        return "Let's drink together some time";
                    case 1:
                        return "I'll see you around";
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
