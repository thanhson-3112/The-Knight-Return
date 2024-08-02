using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronBall : MonoBehaviour
{
    [SerializeField] protected float ironBall = 3f;
    void Start()
    {
        
    }

    public virtual void IronBallHit(float _damageDone)
    {
        ironBall -= _damageDone;

        if (ironBall <= 0)
        {
            Destroy(gameObject);
        }
    }
}
