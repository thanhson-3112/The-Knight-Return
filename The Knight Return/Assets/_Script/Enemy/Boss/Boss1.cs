using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss1 : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;


    [SerializeField] protected float health = 10f;
    [SerializeField] protected float recollLength = 0.2f;
    [SerializeField] protected float recollFactor = 3.5f;
    [SerializeField] protected bool isRecolling = false;
    protected float recollTimer;

    public int damage = 1;
    public PlayerLife playerLife;

    public PlayerMovement playerNormal;

    public float activationDistance = 4f; 
    private Transform playerTransform;
    private bool isAttacking = false;

    public GameObject finishLevelObject;
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public virtual void Update()
    {
        if (health <= 0)
        {
            anim.SetTrigger("EnemyDeath");
            Destroy(gameObject, 1.3f);
            Time.timeScale = 1f;

            ShowNextMap();
        }
        else
        {
            HideNextMap();
        }

        //Danh quai lui lai
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

        //Boss tan cong
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer <= activationDistance && !isAttacking)
        {
            anim.SetTrigger("MushroomAttack");
            isAttacking = true;
            anim.SetTrigger("MushroomRun");
        }
        else
        {
            isAttacking = false;
            anim.SetTrigger("MushroomRun");
        }
       
    }

    //Boss bi tan cong
    public virtual void EnemyHit(float _damageDone, Vector2 _hitDirection, float _hitForce)
    {
        health -= _damageDone;
        anim.SetTrigger("MushroomTakeHit");
        anim.SetTrigger("MushroomRun");
        if (!isRecolling)
        {
            rb.AddForce(-_hitForce * recollFactor * _hitDirection);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerNormal.KBCounter = playerNormal.KBTotalTime;
            if (collision.transform.position.x <= transform.position.x)
            {
                playerNormal.KnockFromRight = true;
            }
            if (collision.transform.position.x > transform.position.x)
            {
                playerNormal.KnockFromRight = false;
            }
            playerLife.TakeDamage(damage);
            
        }
    }

    private void ShowNextMap()
    {
        if (finishLevelObject != null)
        {
            finishLevelObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Error");
        }
    }

    private void HideNextMap()
    {
        if (finishLevelObject != null)
        {
            finishLevelObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Error");
        }
    }
}
