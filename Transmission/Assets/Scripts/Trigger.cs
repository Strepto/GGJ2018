using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

    private Collider2D col2d;

    public Action<Trigger> onTriggerEnter = null;
    public Action<Trigger> onTriggerExit = null;

    void Start()
    {
        col2d = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(onTriggerEnter != null && collision.CompareTag("Player"))
        {
            onTriggerEnter(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (onTriggerExit != null && collision.CompareTag("Player"))
        {
            onTriggerExit(this);
        }
    }

}
