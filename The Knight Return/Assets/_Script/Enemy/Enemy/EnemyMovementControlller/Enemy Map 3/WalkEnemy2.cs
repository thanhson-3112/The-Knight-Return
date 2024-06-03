using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEnemy2 : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public float speed = 3f;

    [Header("Check")]
    public Transform _isGround;
    private bool isGround;
    public LayerMask Ground;
    private bool isWall;
    public Transform _isWall;
    public LayerMask Wall;
    private bool isFacingRight;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetBool("isMoving", true);
        isGround = Physics2D.OverlapCircle(_isGround.position, 0.2f, Ground);
        isWall = Physics2D.OverlapCircle(_isWall.position, 0.2f, Wall);

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

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(new Vector3(0, 180, 0));
        speed = -speed;
    }
}
