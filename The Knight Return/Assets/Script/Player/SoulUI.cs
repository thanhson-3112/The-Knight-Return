using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulUI : MonoBehaviour
{
    public Slider soulUI;
    public GameObject fillArea;
    public GameObject borderArea;

    public void SetMaxSoul(float soul)
    {
        fillArea.SetActive(true);
        borderArea.SetActive(true);
        soulUI.maxValue = soul;
        soulUI.value = soul;
    }

    public void SetSoul(float soul)
    {
        soulUI.value = soul;
    }
}
