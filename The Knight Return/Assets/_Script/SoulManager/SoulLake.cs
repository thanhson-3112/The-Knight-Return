using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulLake : MonoBehaviour
{
    public SoulManager soulManager;

    void Start()
    {
        soulManager = GameObject.FindGameObjectWithTag("Player").GetComponent<SoulManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            soulManager.AddCurrentSoul();
        }
    }
}
