using System.Collections;
using System.Collections.Generic;
using Transmission;
using UnityEngine;

public class DialogHandler : MonoBehaviour {

    private NpcBehaviourScript behaviour;

    public GameObject dialogPanel;

    private DialogWindow dialogWindow;

	void Start () {
        dialogWindow = dialogPanel.GetComponent<DialogWindow>();
        behaviour = GetComponent<NpcBehaviourScript>();
    }

	void Update () {
		
	}

    public void StartDialog()
    {
        dialogPanel.SetActive(true);
        dialogWindow.startDialog(this);
    }

    public void EndDialog()
    {
        dialogPanel.SetActive(false);
    }


}
