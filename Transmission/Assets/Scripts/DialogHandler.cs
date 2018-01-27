﻿using System.Collections;
using System.Collections.Generic;
using Transmission;
using UnityEngine;

public class DialogHandler : MonoBehaviour {

    public NpcBehaviourScript behaviour;

    public GameObject dialogPanel;

	void Start () {
		
	}

	void Update () {
		
	}

    public void StartDialog()
    {
        dialogPanel.SetActive(true);
    }

    public void EndDialog()
    {
        dialogPanel.SetActive(false);
    }
}