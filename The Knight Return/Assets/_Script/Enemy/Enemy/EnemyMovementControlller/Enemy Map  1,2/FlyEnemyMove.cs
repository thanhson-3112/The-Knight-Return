using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemyMove : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;

    public Transform playerTransform;

    [SerializeField] private float enemySpeed = 5f;

    public bool isChasing;
    public float chaseDistance = 10;

    private Vector2 initialPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerTransform = playerObject.GetComponent<Transform>();

        initialPosition = transform.position;
    }

    void Update()
    {
        if (!playerTransform)
        {
            GetTarget();
        }
        else
        {
            MoveTowardsTarget();
        }

    }

    private void MoveTowardsTarget()
    {
        if (isChasing)
        {
            anim.SetBool("isMoving", true);
            if (Vector2.Distance(transform.position, playerTransform.position) > chaseDistance)
            {
                isChasing = false;
            }
            else
            {
                Vector3 targetDirection = playerTransform.position - transform.position;
                if (targetDirection.x > 0)
                {
                    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
                else
                {
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, enemySpeed * Time.deltaTime);
            }
        }
        else
        {
            anim.SetBool("isMoving", false);
            transform.position = Vector2.MoveTowards(transform.position, initialPosition, enemySpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, initialPosition) < 0.1f)
            {
                isChasing = true;
            }
        }
    }

    private void GetTarget()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
}
