using System.Collections;
using System.Collections.Generic;
using Transmission;
using UnityEngine;
using UnityEngine.UI;

public class DialogWindow : MonoBehaviour {

    private DialogHandler currentHandler;
    public Image playerImage;
    public PlayerController player;

    public Sprite[] playerPortraits;

    void Start () {
        
    }
	
	void Update () {
		
	}

    public void startDialog(DialogHandler handler)
    {
        currentHandler = handler;
        if (player.CurrentPlayerState == PlayerController.PlayerState.Boy)
        {
            playerImage.sprite = playerPortraits[0];
        }
        else
        {
            playerImage.sprite = playerPortraits[1];
        }
    }

    public void writeNpcMessage(string text)
    {

    }

    public void writePlayerMessage(string text)
    {

    }
}
