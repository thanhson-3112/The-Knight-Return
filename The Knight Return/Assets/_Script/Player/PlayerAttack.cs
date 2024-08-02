using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [Header("Attack Settings")]
    [SerializeField] private float damage;
    private float timeBetweenAttack = 0.3f;
    private float timeSinceAttack;
    [SerializeField] private Transform AttackTransform, UpAttackTransform, DownAttackTransform;
    [SerializeField] private Vector2 AttackArea = new Vector2(4f, 2.4f),
        UpAttackArea = new Vector2(2f, 2.7f), DownAttackArea = new Vector2(2f, 2.7f);
    [SerializeField] private LayerMask attackablelayer;


    [SerializeField] private List<GameObject> slashEffects;
    [SerializeField] private List<Vector2> slashScales;
    private int currentSlashEffectIndex = 0; 
    [SerializeField] private GameObject slashHitEffect;
    private bool isUpArrowPressed = false;
    private bool isDownArrowPressed = false;
    public float knockbackForce = 10f;

    [Header("Ground,Wall Checking")]
    [SerializeField] public Transform _isGround;
    private bool isGround;
    [SerializeField] public LayerMask Ground;
    [SerializeField] public Transform _isWall;
    public LayerMask wallLayer;
    private bool isWall;

    [Header("Sound Settings")]
    public AudioClip AttackSoundEffect;
    public AudioClip HitSoundEffect;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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

    // hieu ung chem
    private void SlashEffcetAngle(GameObject _slashEffect, int _effcetAngle, Transform _attackTransform)
    {
        _slashEffect = Instantiate(_slashEffect, _attackTransform);
        _slashEffect.transform.eulerAngles = new Vector3(0, 0, _effcetAngle);
        _slashEffect.transform.localScale = slashScales[currentSlashEffectIndex];
        currentSlashEffectIndex = (currentSlashEffectIndex + 1) % slashEffects.Count;
    }


    protected virtual void Attack()
    {
        timeSinceAttack += Time.deltaTime;

        isGround = Physics2D.OverlapCircle(_isGround.position, 0.2f, Ground);
        isWall = Physics2D.OverlapCircle(_isWall.position, 0.2f, wallLayer);
        isUpArrowPressed = Input.GetKey(KeyCode.UpArrow);
        isDownArrowPressed = Input.GetKey(KeyCode.DownArrow);

        if (Input.GetButtonDown("Attack") && timeSinceAttack >= timeBetweenAttack && !isWall)
        {
            // tan cong tren
            if (isUpArrowPressed)
            {
                timeSinceAttack = 0;
                SoundFxManager.instance.PlaySoundFXClip(AttackSoundEffect, transform, 1f);
                anim.SetTrigger("attack");
                SlashEffcetAngle(slashEffects[currentSlashEffectIndex], 180, UpAttackTransform);
                Hit(UpAttackTransform, UpAttackArea);
            }
            // tan cong duoi
            else if (isDownArrowPressed && !isGround)
            {
                timeSinceAttack = 0;
                SoundFxManager.instance.PlaySoundFXClip(AttackSoundEffect, transform, 1f);
                anim.SetTrigger("attack");
                SlashEffcetAngle(slashEffects[currentSlashEffectIndex], 0, DownAttackTransform);
                Hit(DownAttackTransform, DownAttackArea);
            }
            // tan cong ngang
            else
            {
                timeSinceAttack = 0;
                SoundFxManager.instance.PlaySoundFXClip(AttackSoundEffect, transform, 1f);
                SlashEffcetAngle(slashEffects[currentSlashEffectIndex], transform.localScale.x > 0 ? 90 : -90, AttackTransform);
                anim.SetTrigger("attack");
                Hit(AttackTransform, AttackArea);
            }
        }
    }

    private void Hit(Transform _attackTransform, Vector2 _attackArea)
    {
        Collider2D[] objectsToHit = Physics2D.OverlapBoxAll(_attackTransform.position, _attackArea, 0, attackablelayer);
        
        foreach (Collider2D objCollider in objectsToHit)
        {
            Instantiate(slashHitEffect, objCollider.transform.position, Quaternion.identity);
        }

        if (objectsToHit.Length > 0)
        {
            SoundFxManager.instance.PlaySoundFXClip(HitSoundEffect, transform, 1f);
            Debug.Log("Hit");

            // tan cong xuong se day len
            if (isDownArrowPressed && !isGround)
            {
                Vector2 knockbackDirection = Vector2.up;
                rb.velocity = Vector2.zero; // Reset the velocity before applying force
                rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse); // Apply force with impulse for instant effect
            }
        }

        foreach (Collider2D objCollider in objectsToHit)
        {
            EnemyBase enemy = objCollider.GetComponent<EnemyBase>();
            BossLifeBase bossLifeBasess = objCollider.GetComponent<BossLifeBase>();
            Lever lever = objCollider.GetComponent<Lever>();

            Org org = objCollider.GetComponent<Org>();

            if (enemy != null)
            {
                enemy.EnemyHit(damage, (transform.position - objCollider.transform.position).normalized, 100);
            }

            if (org != null)
            {
                org.OrgHit(damage);
            }

            if(lever != null)
            {
                lever.LeverDoor(damage);
            }

            else if (bossLifeBasess != null)
            {
                bossLifeBasess.EnemyHit(damage);
            }
        }
    }
}
