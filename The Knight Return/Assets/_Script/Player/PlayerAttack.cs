using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    [Header("Attack Settings")]
    [SerializeField] private float damage;
    private float timeBetweenAttack = 0.3f;
    private float timeSinceAttack;
    [SerializeField] private Transform AttackTransform, UpAttackTransform, DownAttackTransform;
    [SerializeField]
    private Vector2 AttackArea = new Vector2(1.88f, 1.48f),
        UpAttackArea = new Vector2(1.88f, 2.3f), DownAttackArea = new Vector2(1.88f, 2.3f);
    [SerializeField] private LayerMask attackablelayer;

    [SerializeField] private GameObject slashEffect;
    private bool isUpArrowPressed = false;
    private bool isDownArrowPressed = false;
    private float knockbackForce = 700f;

    [Header("Ground Checking")]
    [SerializeField] public Transform _isGround;
    private bool isGround;
    [SerializeField] public LayerMask Ground;

    [Header("Sound Settings")]
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
    }

    // Attack
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(AttackTransform.position, AttackArea);
        Gizmos.DrawWireCube(UpAttackTransform.position, UpAttackArea);
        Gizmos.DrawWireCube(DownAttackTransform.position, DownAttackArea);
    }

    private void SlashEffcetAngle(GameObject _slashEffect, int _effcetAngle, Transform _attackTransform)
    {
        _slashEffect = Instantiate(_slashEffect, _attackTransform);
        _slashEffect.transform.eulerAngles = new Vector3(0, 0, _effcetAngle);
        _slashEffect.transform.localScale = new Vector2(0.76f, 1.8f);
    }

    protected virtual void Attack()
    {
        timeSinceAttack += Time.deltaTime;

        isGround = Physics2D.OverlapCircle(_isGround.position, 0.2f, Ground);
        isUpArrowPressed = Input.GetKey(KeyCode.UpArrow);
        isDownArrowPressed = Input.GetKey(KeyCode.DownArrow);

        if (Input.GetButtonDown("Attack") && timeSinceAttack >= timeBetweenAttack)
        {
            // tan cong tren
            if (isUpArrowPressed)
            {
                timeSinceAttack = 0;
                AttackSoundEffect.Play();
                anim.SetTrigger("attack");
                SlashEffcetAngle(slashEffect, 0, UpAttackTransform);
                Hit(UpAttackTransform, UpAttackArea);
            }
            // tan cong duoi
            else if (isDownArrowPressed && !isGround)
            {
                timeSinceAttack = 0;
                AttackSoundEffect.Play();
                anim.SetTrigger("attack");
                SlashEffcetAngle(slashEffect, 180, DownAttackTransform);
                Hit(DownAttackTransform, DownAttackArea);
            }
            // tan cong ngang
            else
            {
                timeSinceAttack = 0;
                AttackSoundEffect.Play();
                SlashEffcetAngle(slashEffect, sprite.flipX ? 90 : -90, AttackTransform);
                anim.SetTrigger("attack");
                Hit(AttackTransform, AttackArea);
            }
        }
    }




    private void Hit(Transform _attackTransform, Vector2 _attackArea)
    {
        Collider2D[] objectsToHit = Physics2D.OverlapBoxAll(_attackTransform.position, _attackArea, 0, attackablelayer);
        if (objectsToHit.Length > 0)
        {
            HitSoundEffect.Play();
            Debug.Log("Hit");

            // tan cong xuong se day len
            if (isDownArrowPressed && !isGround)
            {
                Vector2 direction = (objectsToHit[0].transform.position - transform.position).normalized;
                rb.AddForce(-direction * knockbackForce);
            }
        }

        foreach (Collider2D objCollider in objectsToHit)
        {
            EnemyBase enemy = objCollider.GetComponent<EnemyBase>();
            /*            BossBase boss = objCollider.GetComponent<BossBase>();
            */
            if (enemy != null)
            {
                enemy.EnemyHit(damage, (transform.position - objCollider.transform.position).normalized, 100);
            }
            /*else if (boss != null)
            {
                boss.EnemyHit(damage, (transform.position - objCollider.transform.position).normalized, 100);
            }*/
        }
    }
}
