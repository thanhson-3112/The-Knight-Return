using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapActive : MonoBehaviour
{
    public GameObject Map1;
    public GameObject Map2;

    void Start()
    {
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Map1.gameObject.SetActive(true);
            Map2.gameObject.SetActive(false);
        }
    }
}
