using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemEntity : MonoBehaviour
{
    public Image icon;
    public GameObject CountBox;
    public Text count;
    public string ItemKey;
    public string ItemName;

    void Start()
    {

    }


    public void Initialize(string itemKey, string label, Sprite icon)
    {
        ItemKey = itemKey;
        ItemName = label;
        SetIcon(icon);
        CountBox.SetActive(false);
    }

    public void SetIcon(Sprite sprite)
    {
        icon.sprite = sprite;
    }

    public void UpdateCount(int numItems)
    {
        if (numItems > 1)
        {
            count.text = "" + numItems;
            CountBox.SetActive(true);
        }
        else
        {
            CountBox.SetActive(false);
        }
    }

}
