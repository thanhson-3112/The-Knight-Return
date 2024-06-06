using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMFireBall : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 20f;
    protected int damage = 1;

    public PlayerLife playerLife;
    public PlayerMovement player;
    public GameObject target;

    private Vector2 initialPosition;
    private bool isFalling = false;

    [Header("FireBallExplosion")]
    public Transform _isGround;
    public LayerMask Ground;
    private bool isGround;
    public GameObject fireBallExplosion;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<PlayerMovement>();
        playerLife = playerObject.GetComponent<PlayerLife>();

        initialPosition = transform.position;
        target = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(MoveUp());

        Destroy(gameObject, 6);
    }

    private void Update()
    {
        isGround = Physics2D.OverlapCircle(_isGround.position, 0.2f, Ground);
        if (isGround)
        {
            Instantiate(fireBallExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private IEnumerator MoveUp()
    {
        Vector3 moveDirection = new Vector3(Random.Range(-1f, 1f), 1f, 0f).normalized;
        while (true)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, initialPosition) >= 2f)
            {
                break;
            }
            yield return null;
        }

        isFalling = true;
        yield return MoveDown();
    }

    private IEnumerator MoveDown()
    {
        Vector3 targetPosition = target.transform.position;
        Vector2 direction = (targetPosition - transform.position).normalized;
        float distance = Vector2.Distance(transform.position, targetPosition);
        float gravity = Physics2D.gravity.magnitude;
        float angle = 45f * Mathf.Deg2Rad; 

        float velocity = Mathf.Sqrt(distance * gravity / Mathf.Sin(2 * angle));

        float vx = velocity * Mathf.Cos(angle);
        float vy = velocity * Mathf.Sin(angle);

        rb.velocity = new Vector2(vx * direction.x, vy);

        while (rb.velocity.y <= 0 && !isFalling)
        {
            yield return null;
        }

        isFalling = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.KBCounter = player.KBTotalTime;
            if (collision.transform.position.x <= transform.position.x)
            {
                player.KnockFromRight = true;
            }
            if (collision.transform.position.x > transform.position.x)
            {
                player.KnockFromRight = false;
            }
            playerLife.TakeDamage(damage);
        }
    }

}