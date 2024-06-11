using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteo : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    private int meteoDamage = 1;

    private Rigidbody2D rb;
    public GameObject explosionPrefab;
    private Animator anim;

    public PlayerLife playerLife;
    public PlayerMovement player;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<PlayerMovement>();
        playerLife = playerObject.GetComponent<PlayerLife>();

        // Apply diagonal velocity towards the bottom-right
        Vector2 diagonalDirection = new Vector2(1, -1).normalized; // You can adjust this for other directions
        rb.velocity = diagonalDirection * speed;

        Destroy(gameObject, 5f);
    }

    private void FixedUpdate()
    {
        // Flip based on direction to make it look correct
        Vector2 moveDirection = rb.velocity.normalized;

        if (moveDirection.x < 0f)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.KBCounter = player.KBTotalTime;
            if (collision.transform.position.x <= transform.position.x)
            {
                player.KnockFromRight = true;
            }
            if (collision.transform.position.x > transform.position.x)
            {
                player.KnockFromRight = false;
            }
            playerLife.TakeDamage(meteoDamage);

            // Instantiate explosion effect and destroy the meteo
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
