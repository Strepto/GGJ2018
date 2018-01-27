using System.Collections;
using System.Collections.Generic;
using Transmission;
using UnityEngine;

public class DialogHandler : MonoBehaviour {

    private NpcBehaviourScript behaviour;
    private NpcBrain brain;

    public GameObject dialogPanel;


	void Start () {
        behaviour = GetComponent<NpcBehaviourScript>();
        brain = GetComponent<NpcBrain>();
    }

	void Update () {
		
	}

    public void StartDialog()
    {
        dialogPanel.SetActive(true);

        DialogWindow.Instance.startDialog(this, brain.getInitialText());
    }

    public void EndDialog()
    {
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
