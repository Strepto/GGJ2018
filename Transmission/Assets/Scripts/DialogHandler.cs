using System.Collections;
using System.Collections.Generic;
using Transmission;
using UnityEngine;

public class DialogHandler : MonoBehaviour {
    
    private NpcBrain brain;

    public GameObject dialogPanel;


	void Start () {
        brain = GetComponent<NpcBrain>();
    }

	void Update () {
		
	}

    public string GetNpcName()
    {
        return brain.Name;
    }

    public string GetNpcDescription()
    {
        return brain.Description;
    }

    public Sprite GetNpcPortrait()
    {
        return brain.PortaitImage;
    }
    public void startDialog()
    {
        dialogPanel.SetActive(true);

        DialogWindow.Instance.startDialog(this, brain.getInitialText());
    }

    public void endDialog()
    {
        DialogWindow.Instance.endDialog();
        dialogPanel.SetActive(false);
    }

    public string getTextAndSendReply(int choice)
    {
        return brain.getTextAndSendReply(choice);
    }

    public string getChoiceText(int choiceNr)
    {
        return brain.getChoiceText(choiceNr);
    }
}
