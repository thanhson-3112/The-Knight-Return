using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    public static LootManager Instance;

    public delegate void SoulChangeHandler(float amount);
    public event SoulChangeHandler OnSoulChange;

    public delegate void GoldChangeHandler(int goldamount);
    public event GoldChangeHandler OnGoldChange;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void AddSoul(float amount)
    {
        OnSoulChange?.Invoke(amount);
    }

    public void AddGold(int goldamount)
    {
        OnGoldChange?.Invoke(goldamount);
    }
}
