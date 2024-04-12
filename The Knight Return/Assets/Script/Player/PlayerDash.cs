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

    [Header("Dash")]
    [SerializeField] private float dashForce = 8f;
    [SerializeField] private float dashDuration = 0.5f;
    private bool isDashing = false;
    private bool hasDashed = false;

    [Header("Jump")]
    private bool canJump;
    [SerializeField] private float jumpForce = 12f;
    public Transform _canJump;
    public LayerMask Ground;
    private bool doubleJump;

    //Animation
    private enum MovementState { idle, running, jumping, falling }
    private MovementState state = MovementState.idle;


    //KnockBack
    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;
    public bool KnockFromRight;

    [Header("Sound")]
    //Sound
    [SerializeField] private AudioSource RunSoundEffect;
    [SerializeField] private AudioSource JumpSoundEffect;
    [SerializeField] private AudioSource AttackSoundEffect;
    [SerializeField] private AudioSource DashSoundEffect;
    [SerializeField] private AudioSource HitSoundEffect;


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

        // Dash input detection
        if (Input.GetMouseButtonDown(1) && !isDashing && !hasDashed)
        {
            DashSoundEffect.Play();
            anim.SetTrigger("dash");
            StartCoroutine(Dash());
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
   

    // Di chuyen
    IEnumerator Dash()
    {
        isDashing = true;
        hasDashed = true; 
        
        Vector2 originalVelocity = rb.velocity;
        
        rb.velocity = new Vector2(rb.velocity.x + (sprite.flipX ? -dashForce : dashForce), rb.velocity.y);
        
        yield return new WaitForSeconds(dashDuration);
        
        rb.velocity = originalVelocity;
        isDashing = false;
    }

    protected virtual void Move()
    {
        move = Input.GetAxisRaw("Horizontal");
        if (canJump)
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
        if (canJump)
        {
            hasDashed = false;
        }
    }

    protected virtual void Jump()
    {
        canJump = Physics2D.OverlapCircle(_canJump.position, 0.2f, Ground);

        if ((canJump && !(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))))
        {
            doubleJump = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            if (canJump || (doubleJump && !hasDashed))
            {
                JumpSoundEffect.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                doubleJump = !doubleJump;
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
