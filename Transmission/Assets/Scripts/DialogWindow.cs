using System.Collections;
using System.Collections.Generic;
using Transmission;
using UnityEngine;
using UnityEngine.UI;

public class DialogWindow : Singleton<DialogWindow> {

    private DialogHandler currentHandler;
    public Image playerImage;
    public PlayerController player;
    public bool ActiveDialog { get; private set; }

    public Sprite[] playerPortraits;

    void Start () {
        ActiveDialog = false;
    }
	
	void Update () {
		
	}

    public void startDialog(DialogHandler handler)
    {
        currentHandler = handler;
        ActiveDialog = true;

        if (player.CurrentPlayerState == PlayerController.PlayerState.Boy)
        {
            playerImage.sprite = playerPortraits[0];
        }
        else
        {
            playerImage.sprite = playerPortraits[1];
        }
    }

    public void endDialog()
    {
        ActiveDialog = false;
    }

    public void writeNpcMessage(string text)
    {

    }

    public void writePlayerMessage(string text)
    {

    }
}
