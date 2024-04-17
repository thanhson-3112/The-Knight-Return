using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    public static LootManager Instance;

    public delegate void SoulChangeHandler(int amount);
    public event SoulChangeHandler OnSoulChange;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void AddSoul(int amount)
    {
        OnSoulChange?.Invoke(amount);
    }

}
