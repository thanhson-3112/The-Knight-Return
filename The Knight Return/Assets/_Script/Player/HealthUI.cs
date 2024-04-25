using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Slider healthUI;
    public GameObject fillArea;
    public GameObject borderArea;

    public void SetMaxHealth(float health)
    {
        fillArea.SetActive(true);
        borderArea.SetActive(true);
        healthUI.maxValue = health;
        healthUI.value = health;
    }

    public void SetHealth(float health)
    {
        healthUI.value = health;
    }

    public void SetHealthBar()
    {
        fillArea.SetActive(false);
        borderArea.SetActive(false);
    }
}
