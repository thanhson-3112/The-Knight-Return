using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    // Attack
    private float timeBetweenAttack = 0.5f;
    private float timeSinceAttack;
    [SerializeField] Transform AttackTransform;
    [SerializeField] Vector2 AttackArea = new Vector2(1.88f, 1.48f);
    [SerializeField] LayerMask attackablelayer;
    [SerializeField] float damage;

    //Sound
    [SerializeField] private AudioSource AttackSoundEffect;
    [SerializeField] private AudioSource HitSoundEffect;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        UpdateAttackTransform();
    }

    // Thay doi AttackTransform theo huong nhan vat
    public void UpdateAttackTransform()
    {
        Vector3 attackTransformPosition = AttackTransform.position;

        Vector3 characterDirection = sprite.flipX ? Vector3.left : Vector3.right;

        Vector3 newAttackTransformPosition = transform.position + characterDirection * 2f;

        AttackTransform.position = new Vector3(newAttackTransformPosition.x, attackTransformPosition.y, attackTransformPosition.z);

        float characterRotation = sprite.flipX ? 180f : 0f;

        float attackRotation = sprite.flipX ? 180f : 0f;

        AttackTransform.rotation = Quaternion.Euler(0f, 0f, characterRotation + attackRotation);
    }

    // Attack
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(AttackTransform.position, AttackArea);
    }

    protected virtual void Attack()
    {
        timeSinceAttack += Time.deltaTime;

        if (Input.GetButtonDown("Attack") && timeSinceAttack >= timeBetweenAttack)
        {
            timeSinceAttack = 0;
            AttackSoundEffect.Play();
            anim.SetTrigger("attack");
            Hit(AttackTransform, AttackArea);
        }
    }

    private void Hit(Transform _attackTransform, Vector2 _attackArea)
    {
        Collider2D[] objectsToHit = Physics2D.OverlapBoxAll(_attackTransform.position, _attackArea, 0, attackablelayer);
        if (objectsToHit.Length > 0)
        {
            HitSoundEffect.Play();
            Debug.Log("Hit");
        }
        for (int i = 0; i < objectsToHit.Length; i++)
        {
            if (objectsToHit[i].GetComponent<Enemy>() != null)
            {
                objectsToHit[i].GetComponent<Enemy>().EnemyHit
                    (damage, (transform.position - objectsToHit[i].transform.position).normalized, 100);
            }

            if (objectsToHit[i].GetComponent<Enemy1>() != null)
            {
                objectsToHit[i].GetComponent<Enemy1>().EnemyHit
                    (damage, (transform.position - objectsToHit[i].transform.position).normalized, 100);
            }

            if (objectsToHit[i].GetComponent<Enemy2>() != null)
            {
                objectsToHit[i].GetComponent<Enemy2>().EnemyHit
                    (damage, (transform.position - objectsToHit[i].transform.position).normalized, 100);
            }


            if (objectsToHit[i].GetComponent<Boss1>() != null)
            {
                objectsToHit[i].GetComponent<Boss1>().EnemyHit
                    (damage, (transform.position - objectsToHit[i].transform.position).normalized, 100);
            }
            if (objectsToHit[i].GetComponent<Boss2>() != null)
            {
                objectsToHit[i].GetComponent<Boss2>().EnemyHit
                    (damage, (transform.position - objectsToHit[i].transform.position).normalized, 100);
            }
            if (objectsToHit[i].GetComponent<Boss3>() != null)
            {
                objectsToHit[i].GetComponent<Boss3>().EnemyHit
                    (damage, (transform.position - objectsToHit[i].transform.position).normalized, 100);
            }
            if (objectsToHit[i].GetComponent<Boss4>() != null)
            {
                objectsToHit[i].GetComponent<Boss4>().EnemyHit
                    (damage, (transform.position - objectsToHit[i].transform.position).normalized, 100);
            }
        }
  
    }
}
