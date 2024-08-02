using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossNameText : MonoBehaviour
{
    public TMP_Text bossText; 

    void Start()
    {
        gameObject.SetActive(false);
    }

    // Hien ten boss
    public void Show()
    {
        gameObject.SetActive(true);
    }

    // An ten boss
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    // truyen ten boss
    public void SetText(string text)
    {
        if (bossText != null)
        {
            bossText.text = text;
        }
    }
}
