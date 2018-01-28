using Assets.Scripts.Helpers;
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

    public Text npcName;
    public Text npcDescription;
    public Image npcPortraitImage;


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

        npcName.text = currentHandler.GetNpcName();
        npcDescription.text = currentHandler.GetNpcDescription();
        npcPortraitImage.sprite = currentHandler.GetNpcPortrait();


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
        string text1 = currentHandler.getChoiceText(0);
        if (text1 == "")
        {
            firstChoiceText.GetComponentInParent<Button>().interactable = false;
        }
        else
        {
            firstChoiceText.GetComponentInParent<Button>().interactable = true;
        }
        firstChoiceText.GetComponent<Text>().text = text1;
        string text2 = currentHandler.getChoiceText(1);
        if (text2 == "")
        {
            secondChoiceText.GetComponentInParent<Button>().interactable = false;
        }
        else
        {
            secondChoiceText.GetComponentInParent<Button>().interactable = true;
        }
        secondChoiceText.GetComponent<Text>().text = text2;
        string text3 = currentHandler.getChoiceText(2);
        if (text3 == "")
        {
            thirdChoiceText.GetComponentInParent<Button>().interactable = false;
        }
        else
        {
            thirdChoiceText.GetComponentInParent<Button>().interactable = true;
        }
        thirdChoiceText.GetComponent<Text>().text = text3;
    }

    public void endDialog()
    {
        ActiveDialog = false;

        hasStartedDialog = false;
        int i = 0;
        textPanel.transform.DestroyAllChildren();
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
