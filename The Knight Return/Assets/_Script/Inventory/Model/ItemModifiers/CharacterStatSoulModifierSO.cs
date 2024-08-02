using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class CharacterStatSoulModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(int val)
    {
        SoulManager soul = GameObject.FindGameObjectWithTag("Player").GetComponent<SoulManager>();
        soul.maxSoul += val;
    }
}
