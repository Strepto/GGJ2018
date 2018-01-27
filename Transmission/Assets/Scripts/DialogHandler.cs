using System.Collections;
using System.Collections.Generic;
using Transmission;
using UnityEngine;

public class DialogHandler : MonoBehaviour {

    private NpcBehaviourScript behaviour;
    private DialogWindow dialogWindow;
    private NpcBrain brain;

    public GameObject dialogPanel;


	void Start () {
        dialogWindow = dialogPanel.GetComponent<DialogWindow>();
        behaviour = GetComponent<NpcBehaviourScript>();
        brain = GetComponent<NpcBrain>();
    }

	void Update () {
		
	}

    public void StartDialog()
    {
        dialogPanel.SetActive(true);

        dialogWindow.startDialog(this, brain.getInitialText());
    }

    public void EndDialog()
    {
        dialogPanel.SetActive(false);
    }


}
