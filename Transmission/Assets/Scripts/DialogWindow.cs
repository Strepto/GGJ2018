using System.Collections;
using System.Collections.Generic;
using Transmission;
using UnityEngine;
using UnityEngine.UI;

public class DialogWindow : Singleton<DialogWindow> {

    private DialogHandler currentHandler;
    public Image playerImage;
    public bool ActiveDialog { get; private set; }

    public Sprite[] playerPortraits;

    public GameObject npcTextPrefab;
    public GameObject playerTextPrefab;
    public GameObject textPanel;

    public Text firstChoiceText;
    public Text secondChoiceText;
    public Text thirdChoiceText;

    private bool hasStartedDialog;

    void Start () {
        hasStartedDialog = false;
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
        if (PlayerController.Instance.CurrentPlayerState == PlayerController.PlayerState.Boy)
        {
            playerImage.sprite = playerPortraits[0];
        }
        else
        {
            playerImage.sprite = playerPortraits[1];
        }

        writeNpcMessage(text);
        updateTextOptions();
    }

    public void sendReply(int replyNr)
    {
        writePlayerMessage(currentHandler.getChoiceText(replyNr));
        string text = currentHandler.getTextAndSendReply(replyNr);
        if (text == "endDialog()")
        {
            currentHandler.endDialog();
        }
        else
        {
            writeNpcMessage(text);
            updateTextOptions();
        }
    }

    public void updateTextOptions()
    {
        firstChoiceText.GetComponent<Text>().text = currentHandler.getChoiceText(0);
        secondChoiceText.GetComponent<Text>().text = currentHandler.getChoiceText(1);
        thirdChoiceText.GetComponent<Text>().text = currentHandler.getChoiceText(2);
    }

    public void endDialog()
    {
        ActiveDialog = false;
        hasStartedDialog = false;
        int i = 0;
        while(textPanel.transform.childCount > 0 && i < 200)
        {
            DestroyImmediate(textPanel.transform.GetChild(0).gameObject);
            i++;
        }
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
