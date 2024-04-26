using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy2 : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;


    [SerializeField] protected float health = 4f;
    [SerializeField] protected float recollLength = 0.2f;
    [SerializeField] protected float recollFactor = 3.5f;
    [SerializeField] protected bool isRecolling = false;
    protected float recollTimer;

    public int damage = 1;
    public PlayerLife playerLife;

    public PlayerDoubleJump playerDoubleJump;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerDoubleJump = playerObject.GetComponent<PlayerDoubleJump>();
        playerLife = playerObject.GetComponent<PlayerLife>();

    }

    public virtual void Update()
    {
        if (health <= 0)
        {
            damage = 0;
            anim.SetTrigger("EnemyDeath");
            Destroy(gameObject, 1.25f);
        }
        if (isRecolling)
        {
            if (recollTimer < recollLength)
            {
                recollTimer += Time.deltaTime;
            }
            else
            {
                isRecolling = false;
                recollTimer = 0;
            }
        }
    }

    public virtual void EnemyHit(float _damageDone, Vector2 _hitDirection, float _hitForce)
    {
        health -= _damageDone;
        if (!isRecolling)
        {
            rb.AddForce(-_hitForce * recollFactor * _hitDirection);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerDoubleJump.KBCounter = playerDoubleJump.KBTotalTime;
            if (collision.transform.position.x <= transform.position.x)
            {
                playerDoubleJump.KnockFromRight = true;
            }
            if (collision.transform.position.x > transform.position.x)
            {
                playerDoubleJump.KnockFromRight = false;
            }
            playerLife.TakeDamage(damage);
        }
        if (collision.gameObject.tag == "Trap")
        {
            anim.SetTrigger("EnemyDeath");
            Destroy(gameObject, 1f);
        }
    }
}
