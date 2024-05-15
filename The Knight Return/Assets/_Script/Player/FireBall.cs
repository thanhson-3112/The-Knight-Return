using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifeTime = 2f;
    [SerializeField] private float fireBallDamage = 3f;
    [SerializeField] private LayerMask attackableLayer;

    private Rigidbody2D rb;
    /*public GameObject explosionPrefab;*/

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed; 
        Destroy(gameObject, lifeTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & attackableLayer) != 0)
        {
            Hit(collision);
        }
    }

    private void Hit(Collider2D objCollider)
    {
        EnemyBase enemy = objCollider.GetComponent<EnemyBase>();
        BossLifeBase bossLifeBase = objCollider.GetComponent<BossLifeBase>();
        Org org = objCollider.GetComponent<Org>();
        IronBall ironBall = objCollider.GetComponent<IronBall>();

        if (enemy != null)
        {
            enemy.EnemyHit(fireBallDamage, (transform.position - objCollider.transform.position).normalized, 100);
        }
        else if (org != null)
        {
            org.OrgHit(fireBallDamage);
        }
        else if (bossLifeBase != null)
        {
            bossLifeBase.EnemyHit(fireBallDamage);
        }

        else if (ironBall != null)
        {
            ironBall.IronBallHit(fireBallDamage);
        }

        // Instantiate the explosion effect
        /*        if (explosionPrefab != null)
                {
                    Instantiate(explosionPrefab, transform.position, transform.rotation);
                }*/

    }
}
