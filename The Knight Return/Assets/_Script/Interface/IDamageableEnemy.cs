using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageableEnemy
{
    void TakePlayerDamage(float _damageDone, Vector2 _hitDirection, float _hitForce);
}
