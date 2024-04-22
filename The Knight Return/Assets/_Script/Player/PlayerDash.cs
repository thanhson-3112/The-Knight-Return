using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private TrailRenderer tr;
    private float move;
    [SerializeField] private float speed = 5f;
    private bool isFacingRight = true;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 8f;
    private bool isGround;
    public Transform _isGround;
    public LayerMask Ground;
    private bool doubleJump;
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJump;

    [Header("Dash")]
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float dashCooldown = 0.5f;
    private bool isDashing = false;
    private bool hasDashed = false;

    [Header("Wall jump")]
    [SerializeField] public Transform _isWall;
    public LayerMask wallLayer;
    private bool isWall;
    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;

    private bool isWallJumping;
    private float wallJumpingDirection;
    [SerializeField] private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    [SerializeField] private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(8f, 16f); 


    //Animation
    private enum MovementState { idle, running, jumping, falling }
    private MovementState state = MovementState.idle;


    [Header("KB")]
    public float KBForce = 10;
    public float KBCounter;
    public float KBTotalTime = 0.2f;
    public bool KnockFromRight;

    [Header("Sound")]
    //Sound
    [SerializeField] private AudioSource RunSoundEffect;
    [SerializeField] private AudioSource JumpSoundEffect;
    [SerializeField] private AudioSource DashSoundEffect;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        tr = GetComponent<TrailRenderer>();
    }


    void Update()
    {
        if (!isDashing)
        {
            Move();
            Jump();
            UpdateAnimationState();
            KnockBackCouter();
            WallSide();
            WallJump();
        }
        if (!isWallJumping)
        {
            Flip();
        }
        
        // Dash input 
        if (Input.GetButtonDown("Dash") && !isDashing && !hasDashed)
        {
            DashSoundEffect.Play();
            RunSoundEffect.Stop();
            StartCoroutine(Dash());
        }
       
    }

    // Di chuyen
    IEnumerator Dash()
    {
        isDashing = true;
        hasDashed = true;

        Vector2 originalVelocity = rb.velocity;
        rb.gravityScale = 0;


        rb.velocity = new Vector2(transform.localScale.x *dashSpeed, 0);

        tr.emitting = true;
        yield return new WaitForSeconds(dashTime);
        tr.emitting = false;

        rb.velocity = originalVelocity;
        rb.gravityScale = 2;

        yield return new WaitForSeconds(dashCooldown);
        isDashing = false;
    }

    protected virtual void Move()
    {
        move = Input.GetAxisRaw("Horizontal");
        if (isGround)
        {
            if (move != 0f && !RunSoundEffect.isPlaying)
            {
                RunSoundEffect.Play();
            }
            else if (move == 0f)
            {
                RunSoundEffect.Stop();
            }
        }
        else
        {
            RunSoundEffect.Stop();
        }
        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        // Reset hasDashed khi cham dat
        if (isGround || isWall)
        {
            hasDashed = false;
        }
    }

    protected virtual void Jump()
    {
        isGround = Physics2D.OverlapCircle(_isGround.position, 0.2f, Ground);

        if (Input.GetButtonDown("Jump"))
        {
            if (isGround || (doubleJump && !hasDashed))
            {
                JumpSoundEffect.Play();
                isJump = true;
                jumpTimeCounter = jumpTime;
                rb.velocity = Vector2.up * jumpForce;
                doubleJump = !doubleJump;
            }
        }

        if (Input.GetButton("Jump") && isJump)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJump = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJump = false;
        }
    }

    protected virtual void WallSide()
    {
        isWall = Physics2D.OverlapCircle(_isWall.position, 0.2f, wallLayer);

        if(isWall && !isGround && move != 0)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed,float.MaxValue));
            anim.SetTrigger("Hanging");
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if(Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if(transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }


    public virtual void KnockBackCouter()
    {
        if (KBCounter <= 0)
        {
            rb.velocity = new Vector2(move * speed, rb.velocity.y);
        }
        else
        {
            if (KnockFromRight == true)
            {
                rb.velocity = new Vector2(-KBForce, rb.velocity.y);
            }
            if (KnockFromRight == false)
            {
                rb.velocity = new Vector2(KBForce, rb.velocity.y);
            }
            KBCounter -= Time.deltaTime;
            if (KBCounter <= 0)
            {
                KBCounter = 0;
            }
        }
    }


    protected virtual void UpdateAnimationState()
    {
        this.state = MovementState.idle;

        if (move > 0f)
        {
            state = MovementState.running;
        
        }
        else if (move < 0f)
        {
            state = MovementState.running;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f && !isWallSliding)
        {
            state = MovementState.falling;
            
        }
        anim.SetInteger("state", (int)state);
    }

    private void Flip()
    {
        if(isFacingRight && move < 0f || !isFacingRight && move > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
