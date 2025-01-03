using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    private float move;
    [SerializeField] private float speed = 7f;
    private bool isFacingRight = true;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 13f;
    private bool isGround;
    public Transform _isGround;
    public LayerMask Ground;
    private bool canDoubleJump;
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJump;

    [Header("Dash")]
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private float dashTime = 0.1f;
    [SerializeField] private float dashCooldown = 0.5f;
    private bool isDashing = false;
    public bool canDash = true;

    [Header("Wall jump")]
    [SerializeField] public Transform _isWall;
    public LayerMask wallLayer;
    private bool isWall;
    private bool isWallSliding;
    private float wallSlidingSpeed = 1f;

    private bool isWallJumping;
    private float wallJumpingDirection;
    [SerializeField] private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    [SerializeField] private float wallJumpingDuration = 0.4f;
    public Vector2 wallJumpingPower = new Vector2(25f, 20f);

    [SerializeField] public bool lockDash = true;
    [SerializeField] public bool lockDoubleJump = true;
    [SerializeField] public bool lockSlideWall = true;

    //Animation
    private enum MovementState { idle, running, jumping, falling, doubleJumping }
    private MovementState state = MovementState.idle;

    [Header("KB")]
    [SerializeField] public float KBForce = 10;
    [SerializeField] public float KBCounter;
    [SerializeField] public float KBTotalTime = 0.2f;
    [SerializeField] public bool KnockFromRight;

    [Header("Dust")]
    public ParticleSystem jumpDust;
    public ParticleSystem groundDust;

    [Header("Sound")]
    [SerializeField] private AudioClip RunSoundEffect;
    [SerializeField] private AudioClip JumpSoundEffect;
    [SerializeField] private AudioClip DashSoundEffect;
    [SerializeField] private AudioClip touchLandEffect;
    private bool isRunSoundPlaying = false;

    private void Awake()
    {
        if (PlayerMovement.instance != null) Debug.LogError("Only 1 ScoreManager allow");
        PlayerMovement.instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
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
        if (!lockDash && Input.GetButtonDown("Dash") && !isDashing && canDash)
        {
            SoundFxManager.instance.PlaySoundFXClip(DashSoundEffect, transform, 1f);
            SoundFxManager.instance.StopAudio(RunSoundEffect);
            anim.SetTrigger("dash");
            StartCoroutine(Dash());
        }
    }

    // Di chuyen
    IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;

        if (isWallSliding)
        {
            rb.velocity = new Vector2(-transform.localScale.x * dashSpeed, 0);
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
            canDash = false;
        }
        else
        {
            rb.velocity = new Vector2(transform.localScale.x * dashSpeed, 0);
        }

        jumpDust.Play();
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        yield return new WaitForSeconds(dashTime);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);

        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    protected virtual void Move()
    {
        move = Input.GetAxisRaw("Horizontal");

        if (isGround && move != 0f)
        {
            if (!isRunSoundPlaying)
            {
                SoundFxManager.instance.PlaySoundFXClip(RunSoundEffect, transform, 1f);
                isRunSoundPlaying = true;
            }
        }
        else
        {
            if (isRunSoundPlaying)
            {
                SoundFxManager.instance.StopAudio(RunSoundEffect);
                isRunSoundPlaying = false;
            }
        }

        rb.velocity = new Vector2(move * speed, rb.velocity.y);
    }

    protected virtual void Jump()
    {
        bool wasGrounded = isGround;
        isGround = Physics2D.OverlapCircle(_isGround.position, 0.2f, Ground);

        if (isGround && !wasGrounded)
        {
            groundDust.Play();
            SoundFxManager.instance.PlaySoundFXClip(touchLandEffect, transform, 1f);
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGround)
            {
                SoundFxManager.instance.PlaySoundFXClip(JumpSoundEffect, transform, 1f);
                isJump = true;
                jumpTimeCounter = jumpTime;
                rb.velocity = Vector2.up * jumpForce;
                canDoubleJump = true;
                state = MovementState.jumping;
                jumpDust.Play();
            }
            else if (!lockDoubleJump && canDoubleJump)
            {
                SoundFxManager.instance.PlaySoundFXClip(JumpSoundEffect, transform, 1f);
                isJump = true;
                jumpTimeCounter = jumpTime;
                rb.velocity = Vector2.up * jumpForce;
                canDoubleJump = false;
                state = MovementState.doubleJumping;
                jumpDust.Play();
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

    // wall
    protected virtual void WallSide()
    {
        isWall = Physics2D.OverlapCircle(_isWall.position, 0.2f, wallLayer);

        if (!lockSlideWall && isWall && !isGround)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
            anim.SetBool("Hanging", true);
        }
        else
        {
            isWallSliding = false;
            anim.SetBool("Hanging", false);
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

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            SoundFxManager.instance.PlaySoundFXClip(JumpSoundEffect, transform, 1f);
            isWallJumping = true;
            anim.SetBool("Hanging", false);
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
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
            if (state != MovementState.doubleJumping && !isGround)
            {
                state = canDoubleJump ? MovementState.jumping : MovementState.doubleJumping;
            }
        }
        else if (rb.velocity.y < -.1f && !isWallSliding)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private void Flip()
    {
        if (isFacingRight && move < 0f || !isFacingRight && move > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void UnlockDash()
    {
        lockDash = false;
    }

    public void UnlockDoubleJump()
    {
        lockDoubleJump = false;
    }

    public void UnlockSlideWall()
    {
        lockSlideWall = false;
    }

    // save game
    public virtual void FromJson(string jsonString)
    {
        GameData obj = JsonUtility.FromJson<GameData>(jsonString);
        if (obj == null) return;
        this.lockDash = obj.lockDash;
        this.lockDoubleJump = obj.lockDoubleJump;
        this.lockSlideWall = obj.lockSlideWall;

    }

}
