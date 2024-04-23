using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class GoldSO : ScriptableObject
{
    public Sprite goldSprite;
    public string goldName;
    public int goldChance;
    public int goldAmount;

    public void Loot(string goldName, int goldChance, int goldAmount)
    {
        this.goldName = goldName;
        this.goldChance = goldChance;
        this.goldAmount = goldAmount;
    }
}
