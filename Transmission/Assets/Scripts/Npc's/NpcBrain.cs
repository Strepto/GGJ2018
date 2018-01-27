using System.Collections;
using System.Collections.Generic;
using Transmission;
using UnityEngine;

public class NpcBrain : MonoBehaviour {

    public DialogHandler dialog;
    public PlayerController player;

    private int state;

    private void Start()
    {
        state = 0;
    }

    public virtual string getInitialText()
    {
        return "error";
    }

    public virtual string getTextAndSendReply(int choice)
    {
        return "error";
    }
}
