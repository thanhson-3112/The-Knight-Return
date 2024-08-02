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
    private bool hasDamaged = false; 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasDamaged && (((1 << collision.gameObject.layer) & attackableLayer) != 0))
        {
            Hit(collision);
            hasDamaged = true; 
        }
    }

    private void Hit(Collider2D objCollider)
    {
        IronBall ironBall = objCollider.GetComponent<IronBall>();
        IDamageable isCanTakeDamage = objCollider.GetComponent<IDamageable>();
        IDamageableEnemy isCanTakeDamageEnemy = objCollider.GetComponent<IDamageableEnemy>();


        if (isCanTakeDamage != null)
        {
            isCanTakeDamage.TakePlayerDamage(fireBallDamage);
        }

        if (isCanTakeDamageEnemy != null)
        {
            isCanTakeDamageEnemy.TakePlayerDamage(fireBallDamage, (transform.position - objCollider.transform.position).normalized, 100);
        }
        else if (ironBall != null)
        {
            ironBall.IronBallHit(fireBallDamage);
        }
    }
}
