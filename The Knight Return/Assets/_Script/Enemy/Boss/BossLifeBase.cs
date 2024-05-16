using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLifeBase : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;

    [SerializeField] public float bossHealth;

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
    }

    public virtual void EnemyHit(float _damageDone)
    {
        bossHealth -= _damageDone;
        GetComponent<SoulSpawner>().InstantiateLoot(transform.position);
        if (bossHealth <= 0)
        {
            EnemyDie();
        }
    }

    public void EnemyDie()
    {
        Rigidbody2D enemyRigidbody = GetComponent<Rigidbody2D>();
        if (enemyRigidbody != null)
        {
            enemyRigidbody.velocity = Vector2.zero;
            enemyRigidbody.isKinematic = true;
        }

        StartCoroutine(RotateOverTime(transform, Vector3.forward * 180, 1.0f));
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        Destroy(gameObject, 1.25f);


        // roi gold
        for (int i = 0; i <= 30; i++)
        {
            GetComponent<GoldSpawner>().InstantiateLoot(transform.position);
        }

    }

    IEnumerator RotateOverTime(Transform objectToRotate, Vector3 rotationAmount, float duration)
    {
        Quaternion startRotation = objectToRotate.rotation;
        Quaternion endRotation = objectToRotate.rotation * Quaternion.Euler(rotationAmount);
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            objectToRotate.rotation = Quaternion.Lerp(startRotation, endRotation, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        objectToRotate.rotation = endRotation;
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
