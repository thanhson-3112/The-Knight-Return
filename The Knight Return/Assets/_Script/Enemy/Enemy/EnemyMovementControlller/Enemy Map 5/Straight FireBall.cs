using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 20f;

    private int damage = 1;

    public PlayerLife playerLife;
    public PlayerMovement player;
    public GameObject target;
    private Rigidbody2D rb;

    [Header("FireBallExplosion")]
    public Transform _isGround;
    public LayerMask Ground;
    private bool isGround;
    public GameObject fireBallExplosion;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<PlayerMovement>();
        playerLife = playerObject.GetComponent<PlayerLife>();

        Destroy(gameObject, 6);
    }

    private void Update()
    {
        isGround = Physics2D.OverlapCircle(_isGround.position, 0.2f, Ground);
        if (isGround)
        {
            Instantiate(fireBallExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.right * speed;
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
            Instantiate(fireBallExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            playerLife.TakeDamage(damage);
        }
    }
}
