using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossNameText : MonoBehaviour
{
    public TMP_Text bossText; // Reference to the TMP_Text component

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Method to show the boss name text
    public void Show()
    {
        gameObject.SetActive(true);
    }

    // Method to hide the boss name text
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    // Method to set the text
    public void SetText(string text)
    {
        if (bossText != null)
        {
            bossText.text = text;
        }
    }
}
