using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingTripShooterEnemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;

    public Transform playerTransform;

    [SerializeField] private float enemySpeed = 5f;

    public float distanceToStop = 10f;
    public bool isChasing;
    public float chaseDistance = 20f;
    private Vector2 initialPosition;

    public GameObject FireBall;
    private Coroutine spawnCoroutine;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

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
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= chaseDistance)
        {
            if (!isChasing)
            {
                isChasing = true;
                if (spawnCoroutine == null)
                {
                    spawnCoroutine = StartCoroutine(SpawnFireBall());
                }
            }
        }
        else if (distanceToPlayer > chaseDistance)
        {
            if (isChasing)
            {
                isChasing = false;
                if (spawnCoroutine != null)
                {
                    StopCoroutine(spawnCoroutine);
                    spawnCoroutine = null;
                }
            }
        }

        if (isChasing)
        {
            anim.SetBool("isMoving", true);

            if (distanceToPlayer > distanceToStop)
            {
                Vector3 targetDirection = playerTransform.position - transform.position;
                Vector3 targetPosition = new Vector3(playerTransform.position.x, playerTransform.position.y + 1f, playerTransform.position.z);
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, enemySpeed * Time.deltaTime);

                if (targetDirection.x > 0)
                {
                    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
                else
                {
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
            }
        }
        else
        {
            anim.SetBool("isMoving", false);
            transform.position = Vector2.MoveTowards(transform.position, initialPosition, enemySpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, initialPosition) < 0.1f)
            {
                isChasing = false;
            }
        }
    }

    private IEnumerator SpawnFireBall()
    {
        while (true)
        {
            Vector2 direction = playerTransform.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Ban ra 3 cau lua theo hình non
            for (int i = 0; i < 3; i++)
            {
                float offsetAngle = angle + (i - 1) * 30f;
                Vector2 bulletDirection = new Vector2(Mathf.Cos(offsetAngle * Mathf.Deg2Rad), Mathf.Sin(offsetAngle * Mathf.Deg2Rad));

                // Instantiate bullet
                GameObject spawnedEnemy = GameObject.Instantiate(FireBall, transform.position, Quaternion.identity);
                spawnedEnemy.transform.right = bulletDirection;
            }
            yield return new WaitForSeconds(3f);
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
