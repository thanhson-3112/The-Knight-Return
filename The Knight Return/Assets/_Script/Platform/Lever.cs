using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public List<Gate> gates;

    [SerializeField] protected float lever = 1f;

    void Update()
    {

    }

    public virtual void LeverDoor(float _damageDone)
    {
        lever -= _damageDone;

        if(lever <= 0)
        {
            foreach (Gate gate in gates)
            {
                gate.OpenDoor();
            }
        }
    }
}
