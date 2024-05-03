using System.Collections;
using UnityEngine;

public class JumpEnemyMove : MonoBehaviour
{
    [Header("For Patrolling")]
    [SerializeField] float speed = 5f;
    private bool isFacingRight = true;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] Transform wallCheckPoint;
    [SerializeField] LayerMask groundLayer;
    private bool isGround;
    private bool isWall;

    [Header("For Jump Attacking")]
    [SerializeField] float jumpHeight = 10f;
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform groundCheck;
    private bool isGrounded;

    [Header("Other")]
    private Animator anim;
    private Rigidbody2D rb;

    public bool isChasing;
    public float chaseDistance = 5;

    private bool isJumping = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        isGround = Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, groundLayer);
        isWall = Physics2D.OverlapCircle(wallCheckPoint.position, 0.2f, groundLayer);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (isChasing)
        {
            if (Vector2.Distance(transform.position, playerTransform.position) > chaseDistance)
            {
                isChasing = false;
            }
            else
            {
                anim.SetTrigger("SlimeChasing");
                if (playerTransform.position.x - transform.position.x < 0 && !isFacingRight && isGrounded)
                {
                    Flip();
                }
                else if (playerTransform.position.x - transform.position.x > 0 && isFacingRight && isGrounded)
                {
                    Flip();
                }

                StartCoroutine(JumpAttack());
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
            {
                isChasing = true;
            }
            anim.SetTrigger("EnemyRun");
            transform.position += Vector3.right * speed * Time.deltaTime;
            if (!isGround && isFacingRight && isGrounded || isWall && isFacingRight && isGrounded)
            {
                Flip();
            }
            else if (!isGround && !isFacingRight && isGrounded || isWall && !isFacingRight && isGrounded)
            {
                Flip();
            }
        }
    }

    IEnumerator JumpAttack()
    {
        // Ch? ch?y coroutine n?u nó ch?a ???c g?i và ?ang có tr?ng thái isGrounded và isChasing
        if (!isJumping && isGrounded && isChasing)
        {
            isJumping = true; 

            yield return new WaitForSeconds(1f);

            anim.SetTrigger("SlimeJump");
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            float jumpForceX = direction.x * jumpHeight;
            float jumpForceY = jumpHeight;
            rb.velocity = new Vector2(jumpForceX, jumpForceY);

            yield return new WaitForSeconds(0.5f);

            anim.SetTrigger("SlimeFall");

            isJumping = false;
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(new Vector3(0, 180, 0));
        speed = -speed;
    }
}
