using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatHealthModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(int val)
    {
        PlayerLife health = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLife>();
        health.maxHealth += val;
    }
}