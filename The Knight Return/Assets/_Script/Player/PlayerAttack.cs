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
    [SerializeField] private Vector2 AttackArea = new Vector2(1.88f, 1.48f), 
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
        Gizmos.DrawWireCube(UpAttackTransform.position, UpAttackArea);
        Gizmos.DrawWireCube(DownAttackTransform.position, DownAttackArea);
    }

    private void SlashEffcetAngle(GameObject _slashEffect, int _effcetAngle, Transform _attackTransform)
    {
        _slashEffect = Instantiate(_slashEffect, _attackTransform);
        _slashEffect.transform.eulerAngles = new Vector3(0, 0, _effcetAngle);
        _slashEffect.transform.localScale = new Vector2(0.76f, 1.56f);
    }

    protected virtual void Attack()
    {
        timeSinceAttack += Time.deltaTime;

        isGround = Physics2D.OverlapCircle(_isGround.position, 0.2f, Ground);
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            isUpArrowPressed = true;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            isUpArrowPressed = false;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            isDownArrowPressed = true;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            isDownArrowPressed = false;
        }

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
                SlashEffcetAngle(slashEffect,180, DownAttackTransform);
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

            if (isDownArrowPressed && !isGround)
            {
                // L?y h??ng t? ng??i ch?i ??n quái v?t
                Vector2 direction = (objectsToHit[0].transform.position - transform.position).normalized;

                // Áp d?ng l?c ??y ng??c h??ng direction
                rb.AddForce(-direction * knockbackForce);
            }
        }

        foreach (var objToHit in objectsToHit)
        {
            var enemy = objToHit.GetComponent<IEnemy>();
            if (enemy != null)
            {
                enemy.EnemyHit(damage, (transform.position - objToHit.transform.position).normalized, 100);
            }
        }
    }
}

public interface IEnemy
{
    void EnemyHit(float damage, Vector2 hitDirection, float hitForce);
}
