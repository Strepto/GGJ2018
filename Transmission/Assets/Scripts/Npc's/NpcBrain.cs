using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcBrain : MonoBehaviour {

    public DialogHandler dialog;

    public string[] textOptions;
    public string[] playerTextOptions;
    public int numberOfTextOptions;
    public int numberOfPlayerTextOptions;


    private void Start()
    {
        numberOfTextOptions = textOptions.Length;
        numberOfPlayerTextOptions = playerTextOptions.Length;
    }

}
