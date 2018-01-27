using System.Collections;
using System.Collections.Generic;
using Transmission;
using UnityEngine;

public class ToiletGuardScript : NpcBrain {

    


    public override string getInitialText()
    {

        if (PlayerController.Instance.CurrentPlayerState == PlayerController.PlayerState.Boy)
        {
            return "You cannot enter here!";
        }
        else
        {
            return "You're free to pass, m'lady";
        }
    }

    public override string getTextAndSendReply(int choice)
    {
        return "error";
    }



    public override string getChoiceText(int choiceNr)
    {
        return "error";
    }
}
