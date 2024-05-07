using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float fallDelay = 1f;
    [SerializeField] private float destroyDelay = 1f;

    private bool falling = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (falling)
        {
            return;
        }

        if(collision.transform.tag == "Player")
        {
            StartCoroutine(StartFall());
        }
    }

     private IEnumerator StartFall()
     {
        falling = true;

        yield return new WaitForSeconds(fallDelay);

        rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject, destroyDelay);
     }
}
