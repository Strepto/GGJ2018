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

    public GameObject npcTextPrefab;
    public GameObject playerTextPrefab;
    public GameObject textPanel;

    private bool hasStartedDialog;

    void Start () {
        hasStartedDialog = false;
    }
	
	void Update () {
		
	}

    public void startDialog(DialogHandler handler, string initialText)
    {
        currentHandler = handler;
        ActiveDialog = true;
        if (!hasStartedDialog)
        {
            hasStartedDialog = true;
            initiateDialog(initialText);
        }
    }

    private void initiateDialog(string text)
    {
        if (player.CurrentPlayerState == PlayerController.PlayerState.Boy)
        {
            playerImage.sprite = playerPortraits[0];
        }
        else
        {
            playerImage.sprite = playerPortraits[1];
        }

        writeNpcMessage(text);
    }

    public void sendReply(int replyNr)
    {

    }

    public void endDialog()
    {
        ActiveDialog = false;
        hasStartedDialog = false;
        currentHandler.EndDialog();
    }

    public void writeNpcMessage(string text)
    {
        GameObject newTextBubble = Instantiate(npcTextPrefab, textPanel.transform);
        newTextBubble.transform.Find("Panel").transform.Find("Text").GetComponent<Text>().text = text;
    }

    public void writePlayerMessage(string text)
    {
        GameObject newTextBubble = Instantiate(playerTextPrefab, textPanel.transform);
        newTextBubble.transform.Find("Panel").transform.Find("Text").GetComponent<Text>().text = text;
    }
}
