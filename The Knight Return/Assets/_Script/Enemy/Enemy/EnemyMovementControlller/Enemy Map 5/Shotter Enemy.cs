using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotterEnemy : MonoBehaviour
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
    public float chaseDistance = 10f; // Adjusted to 10 as per your requirement

    public GameObject fireBall;
    public Transform fireBallPoint;
    private bool isAttacking = false;

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

        if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
        {
            isChasing = true;
            FacePlayer();
            if (!isAttacking)
            {
                StartCoroutine(spawnFireBall());
                isAttacking = true;
            }
        }
        else
        {
            isChasing = false;
            Patrol();
        }
    }

    private void Patrol()
    {
        anim.SetBool("isMoving", true);
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

    private void FacePlayer()
    {
        if (playerTransform.position.x - transform.position.x < 0 && !isFacingRight)
        {
            Flip();
        }
        else if (playerTransform.position.x - transform.position.x > 0 && isFacingRight)
        {
            Flip();
        }
    }

    private IEnumerator spawnFireBall()
    {
        Instantiate(fireBall, fireBallPoint.position, Quaternion.identity);
        anim.SetTrigger("attack");
        yield return new WaitForSeconds(2f);
        isAttacking = false;
    }
}
