using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomButterfly : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;

    public Transform playerTransform;

    [SerializeField] private float enemySpeed = 5f;

    public float distanceToStop = 20f;
    public bool isChasing;
    public float chaseDistance = 30f;
    private Vector2 initialPosition;

    public GameObject litterButterfly;
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
                    spawnCoroutine = StartCoroutine(SpawnLitterButterfly());
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

    private IEnumerator SpawnLitterButterfly()
    {
        while (true)
        {
            Instantiate(litterButterfly, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(6f);
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
