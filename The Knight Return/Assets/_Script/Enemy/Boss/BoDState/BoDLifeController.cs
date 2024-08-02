using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoDLifeController : MonoBehaviour, IDamageable
{
    protected Rigidbody2D rb;
    protected Animator anim;

    [SerializeField] public float bossHealth = 20f;

    public int damage;
    public PlayerLife playerLife;
    public PlayerMovement player;

    private Vector3 originalPosition;

    private float shakeMagnitude = 0.5f;
    private bool isDying = false;

    public ParticleSystem hitEffect;
    public ParticleSystem hitEffect1;

    public GameObject finishLevelObject;

    [Header("Sound")]
    public AudioClip deathSound;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<PlayerMovement>();
        playerLife = playerObject.GetComponent<PlayerLife>();

        originalPosition = transform.position;
    }

    public virtual void Update()
    {
    }

    public virtual void TakePlayerDamage(float _damageDone)
    {
        bossHealth -= _damageDone;
        GetComponent<SoulSpawner>().InstantiateLoot(transform.position);
        if (bossHealth <= 0)
        {
            EnemyDie();
        }
        hitEffect.Play();
        anim.SetTrigger("BoDTakeHit");
        anim.SetTrigger("BoDRun");
    }

    public virtual void EnemyDie()
    {
        Rigidbody2D enemyRigidbody = GetComponent<Rigidbody2D>();
        if (enemyRigidbody != null)
        {
            enemyRigidbody.velocity = Vector2.zero;
            enemyRigidbody.isKinematic = true;
        }

        // Trigger shake effect
        isDying = true; 
        StartCoroutine(Shake());

        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        hitEffect1.Play();
        SoundFxManager.instance.PlaySoundFXClip(deathSound, transform, 1f);
        anim.SetTrigger("BoDDeath");

        // spawn cong dich chuyen khi boss chet
        Instantiate(finishLevelObject, originalPosition, Quaternion.identity);
        Destroy(gameObject, 2f);


    }

    private IEnumerator Shake()
    {
        while (isDying)
        {
            Vector3 newPos = originalPosition + Random.insideUnitSphere * shakeMagnitude;
            transform.position = newPos;
            yield return null;
        }
        transform.position = originalPosition;
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
