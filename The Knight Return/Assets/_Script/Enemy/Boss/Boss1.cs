using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss1 : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] protected float health = 10f;

    protected float recollTimer;

    public int damage = 1;
    public PlayerLife playerLife;

    public PlayerMovement player;
    private Transform playerTransform;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<PlayerMovement>();
    }

    public virtual void Update()
    {
        if (health <= 0)
        {
            anim.SetTrigger("EnemyDeath");
            Destroy(gameObject, 1.3f);
            Time.timeScale = 1f; 
        }

    }

    //Boss bi tan cong
    public virtual void EnemyHit(float _damageDone, Vector2 _hitDirection, float _hitForce)
    {
        health -= _damageDone;
    }

    private void OnCollisionEnter2D(Collision2D collision)
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
            playerLife.TakeDamage(damage);

        }
    }
}
