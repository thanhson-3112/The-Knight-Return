using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MHStateMachine : StateMachine
{
    public MHJumpState jumpState;
    public MHMovingState movingState;
    public MHAttackState attackState;

    List<BaseState> randomStates;
    private Animator anim;
    private Rigidbody2D rb;
    BaseState LastState;
    BaseState LastTwoState;

    [Header("Idel")]
    public float idelMovementSpeed = 2f;
    public Vector2 idelMovementDirection = new Vector2(-5, 2);

    [Header("DashAttack")]
    public float dashSpeed = 10f;
    public Vector2 attackMovementDirection = new Vector2(-1, 2);

    [Header("Jump")]
    public float jumpForce = 15f;
    public Transform player;

    [Header("Other")]
    private bool facingLeft = true;
    public CameraManager cam;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        movingState = new MHMovingState(this, anim, rb);
        jumpState = new MHJumpState(this, anim, rb);
        attackState = new MHAttackState(this, anim, rb);

        randomStates = new List<BaseState>() { movingState, jumpState, attackState };
    }

    new void Start()
    {
        base.Start();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<Transform>();
        GameObject camObject = GameObject.FindGameObjectWithTag("MainCamera");
        cam = camObject.GetComponent<CameraManager>();
    }

    new public void Update()
    {
        base.Update();
    }

    public void NextState()
    {
        ChangeState(RandomState());
    }

    BaseState RandomState()
    {
        int ran = Random.Range(0, randomStates.Count);
        while (randomStates[ran] == LastState || randomStates[ran] == LastTwoState)
        {
            ran = Random.Range(0, randomStates.Count);
        }
        LastTwoState = LastState;
        LastState = randomStates[ran];
        return randomStates[ran];
    }

    protected override BaseState GetInitialState()
    {
        LastState = movingState;
        LastTwoState = movingState; 
        return movingState;
    }

    public void GetTarget()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    public void FlipTowardsPlayer()
    {
        float playerDirection = player.position.x - transform.position.x;

        if (playerDirection > 0 && facingLeft)
        {
            Flip();
        }
        else if (playerDirection < 0 && !facingLeft)
        {
            Flip();
        }
    }

    public void Flip()
    {
        facingLeft = !facingLeft;
        idelMovementDirection.x *= -1;
        attackMovementDirection.x *= -1;
        transform.Rotate(0, 180, 0);
    }

    public void ShakeCam()
    {
        cam.ShakeCamera();
    }
}
