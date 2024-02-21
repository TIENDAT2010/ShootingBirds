using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI contentText;


    public void UpdateDialog(string title, string content)
    {
        if(titleText != null)
        {
            titleText.text = title;
        }
        if(contentText != null)
        {
            contentText.text = content;
        }
    }

    public void Show(bool isShow)
    {
        gameObject.SetActive(isShow);
    }
}
