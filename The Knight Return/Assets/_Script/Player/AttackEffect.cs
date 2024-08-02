using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    public float timeEffect;

    void Start()
    {
        Destroy(gameObject, 0.2f);
    }
}
