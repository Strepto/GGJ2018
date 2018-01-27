using System.Collections;
using System.Collections.Generic;
using Transmission;
using UnityEngine;

public class NpcBrain : MonoBehaviour {

    public DialogHandler dialog;

    protected int stateNumber;

    private void Start()
    {
        stateNumber = 0;
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
