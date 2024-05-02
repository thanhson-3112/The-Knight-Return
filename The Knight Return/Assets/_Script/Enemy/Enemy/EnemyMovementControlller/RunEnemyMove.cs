using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunEnemyMove : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 10f;

    [Header("Check")]
    public Transform _isGround;
    private bool isGround;
    public LayerMask Ground;
    private bool isWall;
    public Transform _isWall;
    public LayerMask Wall;
    private bool isFacingRight;

    // Follow player
    /*public Transform playerTransform;
    public bool isChasing;
    public float chaseDistance = 3;*/


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        isGround = Physics2D.OverlapCircle(_isGround.position, 0.2f, Ground);
        isWall = Physics2D.OverlapCircle(_isWall.position, 0.2f, Wall);

        Vector2 moveVelocity = new Vector2(speed, rb.velocity.y);
        rb.velocity = moveVelocity;

        if (!isGround && isFacingRight || isWall && isFacingRight)
        {
            Flip();
        }
        else if (!isGround && !isFacingRight || isWall && !isFacingRight)
        {
            Flip();

        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(new Vector3(0, 180, 0));
        speed = -speed;
    }
}