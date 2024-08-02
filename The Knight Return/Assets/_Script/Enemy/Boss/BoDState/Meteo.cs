using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteo : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    private int meteoDamage = 1;

    private Rigidbody2D rb;
    private Animator anim;

    public PlayerLife playerLife;
    public PlayerMovement player;

    [Header("Sound")]
    public AudioClip meteorSound;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<PlayerMovement>();
        playerLife = playerObject.GetComponent<PlayerLife>();


        Vector2 diagonalDirection = new Vector2(1, -1).normalized; 
        rb.velocity = diagonalDirection * speed;

        SoundFxManager.instance.PlaySoundFXClip(meteorSound, transform, 1);
        Destroy(gameObject, 2.5f);
    }

    private void FixedUpdate()
    {
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

            Destroy(gameObject);
        }
    }
}
