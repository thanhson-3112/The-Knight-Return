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


    [Header("Jump")]
    private bool isGround;
    [SerializeField] private float jumpForce = 12f;
    public Transform _canJump;
    public LayerMask Ground;
    private bool doubleJump;

    [Header("Dash")]
    [SerializeField] private float dashSpeed = 8f;
    [SerializeField] private float dashDuration = 0.1f;
    [SerializeField] private float dashCooldown = 0.5f;
    private bool isDashing = false;
    private bool hasDashed = false;

    //Animation
    private enum MovementState { idle, running, jumping, falling }
    private MovementState state = MovementState.idle;


    [Header("KB")]
    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;
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

        rb.velocity = new Vector2(rb.velocity.x + (sprite.flipX ? -dashSpeed : dashSpeed), rb.velocity.y);

        tr.emitting = true;
        yield return new WaitForSeconds(dashDuration);
        tr.emitting = false;


        rb.velocity = originalVelocity;

        
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
        if (isGround)
        {
            hasDashed = false;
        }
    }

    protected virtual void Jump()
    {
        isGround = Physics2D.OverlapCircle(_canJump.position, 0.2f, Ground);

        if ((isGround && !Input.GetButton("Jump")))
        {
            doubleJump = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGround || (doubleJump && !hasDashed))
            {
                JumpSoundEffect.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                doubleJump = !doubleJump; 
            }
        }
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
                rb.velocity = new Vector2(-KBForce, KBForce);
            }
            if (KnockFromRight == false)
            {
                rb.velocity = new Vector2(KBForce, KBForce);
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
            sprite.flipX = false;
        
        }
        else if (move < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
            
        }
        anim.SetInteger("state", (int)state);
    }
}
