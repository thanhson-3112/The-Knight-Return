using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBase : MonoBehaviour, IDamageableEnemy
{
    protected Rigidbody2D rb;
    protected Animator anim;

    [SerializeField] protected float enemyHealth;
    protected float recollLength = 0.2f;
    protected float recollFactor = 20f;
    protected bool isRecolling = false;
    protected float recollTimer;

    protected int damage;
    public PlayerLife playerLife;
    public PlayerMovement player;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<PlayerMovement>();
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

    public virtual void TakePlayerDamage(float _damageDone, Vector2 _hitDirection, float _hitForce)
    {
        enemyHealth -= _damageDone;
        GetComponent<SoulSpawner>().InstantiateLoot(transform.position);

        if (!isRecolling)
        {
            rb.AddForce(-_hitForce * recollFactor * _hitDirection);
            isRecolling = true;
        }

        if (enemyHealth <= 0)
        {
            EnemyDie();
        }
    }

    public virtual void EnemyDie()
    {
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        // roi gold
        for(int i = 0; i <= 4; i++)
        {
            GetComponent<GoldSpawner>().InstantiateLoot(transform.position);
        }

        ActivateEnemy();
        StartCoroutine(DeactivateAfterDelay(0f));

    }

    IEnumerator DeactivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }

    public virtual void ActivateEnemy()
    {
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = true;
        }

        isRecolling = false;
        recollTimer = 0;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
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
