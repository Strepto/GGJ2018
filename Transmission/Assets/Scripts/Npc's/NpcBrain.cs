using System.Collections;
using System.Collections.Generic;
using Transmission;
using UnityEngine;

public class NpcBrain : MonoBehaviour {

    public DialogHandler dialog;
    public PlayerController player;

    private void Start()
    {

    }

    public virtual string getInitialText()
    {
        return "error";
    }

}
