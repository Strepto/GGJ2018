using System.Collections;
using System.Collections.Generic;
using Transmission;
using UnityEngine;
using UnityEngine.UI;

public class DialogWindow : MonoBehaviour {

    private DialogHandler currentHandler;
    public Image playerImage;
    public PlayerController player;

    public Image[] playerPortraits;

    void Start () {

    }
	
	void Update () {
		
	}

    public void startDialog(DialogHandler handler)
    {
        currentHandler = handler;
        playerImage = playerPortraits[1];
        if (player.CurrentPlayerState == PlayerController.PlayerState.Boy)
        {
            playerImage = playerPortraits[1];
        }
        else
        {
            playerImage = playerPortraits[1];
        }
    }

    public void writeNpcMessage(string text)
    {

    }

    public void writePlayerMessage(string text)
    {

    }
}
