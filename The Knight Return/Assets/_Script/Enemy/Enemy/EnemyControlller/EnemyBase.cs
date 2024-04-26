using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBase : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;

    [SerializeField] protected float enemyHealth;
    [SerializeField] protected float recollLength = 0.2f;
    [SerializeField] protected float recollFactor = 2.5f;
    [SerializeField] protected bool isRecolling = false;
    protected float recollTimer;

    protected int damage;
    public PlayerLife playerLife;
    public PlayerDash playerDash;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerDash = playerObject.GetComponent<PlayerDash>();
        playerLife = playerObject.GetComponent<PlayerLife>();
    }

    public virtual void Update()
    {
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
        enemyHealth -= _damageDone;
        if (!isRecolling)
        {
            rb.AddForce(-_hitForce * recollFactor * _hitDirection);
        }

        if (enemyHealth <= 0)
        {
            EnemyDie();
        }
    }

    public void EnemyDie()
    {
        damage = 0;
        anim.SetTrigger("EnemyDeath");

        Rigidbody2D enemyRigidbody = GetComponent<Rigidbody2D>();
        if (enemyRigidbody != null)
        {
            enemyRigidbody.velocity = Vector2.zero; // D?ng quái v?t di chuy?n
            enemyRigidbody.isKinematic = true; // T?t v?n ??ng v?t lý
        }

        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        Destroy(gameObject, 1.25f);

        GetComponent<SoulSpawner>().InstantiateLoot(transform.position);
        GetComponent<GoldSpawner>().InstantiateLoot(transform.position);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerDash.KBCounter = playerDash.KBTotalTime;
            if (collision.transform.position.x <= transform.position.x)
            {
                playerDash.KnockFromRight = true;
            }
            if (collision.transform.position.x > transform.position.x)
            {
                playerDash.KnockFromRight = false;
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
