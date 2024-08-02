using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSkill : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private float spellSpeed = 5f;
    public int spellDamage = 1;

    public float rotateSpeed = 0.25f;

    public PlayerLife playerLife;
    public Transform target;

    private bool isDamaging = false; 
    public float damageInterval = 1f;

    [Header("Sound")]
    public AudioClip thunderSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerLife = playerObject.GetComponent<PlayerLife>();
        SoundFxManager.instance.PlaySoundFXClip(thunderSound, transform, 1f);
        Destroy(gameObject, 10f);
    }

    void Update()
    {
        if (!target)
        {
            GetTarget();
        }
        else
        {
            RotateTowardsTarget();
        }
    }

    private void RotateTowardsTarget()
    {
        Vector3 targetDirection = target.position - transform.position;
        if (targetDirection.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        transform.position = Vector2.MoveTowards(transform.position, target.position, spellSpeed * Time.deltaTime);
    }

    private void GetTarget()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartDamage();
        }
    }

    // B?t ??u gây damage liên t?c
    private void StartDamage()
    {
        if (!isDamaging)
        {
            isDamaging = true;
            StartCoroutine(DamageCoroutine());
        }
    }

    // Coroutine ?? gây damage ??nh k?
    private IEnumerator DamageCoroutine()
    {
        while (isDamaging)
        {
            if (playerLife != null)
            {
                playerLife.TakeDamage(spellDamage);
            }
            yield return new WaitForSeconds(damageInterval);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StopDamage();
        }
    }

    private void StopDamage()
    {
        isDamaging = false;
    }
}
