using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class SoulSO : ScriptableObject
{
    public Sprite soulSprite;
    public string soulName;
    public int soulChance;
    public int soulAmount;

    public void Loot(string soulName, int soulChance, int soulAmount)
    {
        this.soulName = soulName;
        this.soulChance = soulChance;
        this.soulAmount = soulAmount;
    }

}
