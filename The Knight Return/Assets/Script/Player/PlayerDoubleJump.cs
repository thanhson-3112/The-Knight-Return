using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoubleJump : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    private float move;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 12f;

    //Jump
    private bool canJump;
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

    //Sound
    [SerializeField] private AudioSource RunSoundEffect;
    [SerializeField] private AudioSource JumpSoundEffect;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }


    void Update()
    {

        Move();
        Jump();
        UpdateAnimationState();
        KnockBackCouter();

 

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
        }
    }
   
    // Di chuyen
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
    }

    protected virtual void Jump()
    {
        canJump = Physics2D.OverlapCircle(_canJump.position, 0.2f, Ground);

        if ((canJump && !Input.GetButton("Jump")))
        {
            doubleJump = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (canJump || doubleJump )
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
