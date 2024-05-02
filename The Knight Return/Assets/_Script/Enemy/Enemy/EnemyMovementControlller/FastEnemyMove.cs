using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemyMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public float speed = 2f;

    [Header("Check")]
    public Transform _isGround;
    private bool isGround;
    public LayerMask Ground;
    private bool isWall;
    public Transform _isWall;
    public LayerMask Wall;
    private bool isFacingRight;

    // Follow player
    public Transform playerTransform;
    public bool isChasing;
    public float chaseDistance = 5;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerTransform = playerObject.GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        isGround = Physics2D.OverlapCircle(_isGround.position, 0.2f, Ground);
        isWall = Physics2D.OverlapCircle(_isWall.position, 0.2f, Wall);

        if (isChasing)
        {
            if (Vector2.Distance(transform.position, playerTransform.position) > chaseDistance)
            {
                isChasing = false;
            }
            else
            {
                if (playerTransform.position.x - transform.position.x < 0 && !isFacingRight)
                {
                    Flip();
                }
                else if (playerTransform.position.x - transform.position.x > 0 && isFacingRight)
                {
                    Flip();
                }
                transform.position += Vector3.right * speed * 2f * Time.deltaTime;
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
            {
                isChasing = true;
            }

            transform.position += Vector3.right * speed * Time.deltaTime;
            if (!isGround && isFacingRight || isWall && isFacingRight)
            {
                Flip();
            }
            else if (!isGround && !isFacingRight || isWall && !isFacingRight)
            {
                Flip();
            }
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(new Vector3(0, 180, 0));
        speed = -speed;
    }
}
