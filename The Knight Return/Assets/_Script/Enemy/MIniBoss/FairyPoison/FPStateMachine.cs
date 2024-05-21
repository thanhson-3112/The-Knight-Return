using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPStateMachine : StateMachine
{
    public FPDashState dashState;
    public FPMovingState movingState;
    public FPAttackState attackState;

    List<BaseState> randomStates;
    private Animator anim;
    private Rigidbody2D rb;
    BaseState LastState;
    BaseState LastTwoState;

    [Header("Idel")]
    public float idelMovementSpeed = 2f;
    public Vector2 idelMovementDirection = new Vector2(-5, 2);

    [Header("AttackUpNDown")]
    public float attackMovementSpeed = 3f;
    public Vector2 attackMovementDirection = new Vector2(-1, 2);

    [Header("Wave Attack")]
    public GameObject firePrefab;
    public Transform firing;
    [Range(0.1f, 2f)]
    public float fireRate = 0.8f;
    
    [Header("Other")]
    [SerializeField] Transform goundCheckUp;
    [SerializeField] Transform goundCheckDown;
    [SerializeField] Transform goundCheckWall;
    [SerializeField] LayerMask groundLayer;
    public Transform player;

    private bool facingLeft = true;
    public bool goingUp = true;

    public bool isTouchingUp;
    public bool isTouchingDown;
    public bool isTouchingWall;

    public CameraManager cam;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        movingState = new FPMovingState(this, anim, rb);
        dashState = new FPDashState(this, anim, rb);
        attackState = new FPAttackState(this, anim, rb);

        randomStates = new List<BaseState>() { movingState, dashState, attackState };
    }

    new void Start()
    {
        base.Start();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<Transform>();
    }

    new public void Update()
    {
        base.Update();
        isTouchingUp = Physics2D.OverlapCircle(goundCheckUp.position, 0.5f, groundLayer);
        isTouchingDown = Physics2D.OverlapCircle(goundCheckDown.position, 0.5f, groundLayer);
        isTouchingWall = Physics2D.OverlapCircle(goundCheckWall.position, 0.5f, groundLayer);
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

    public void ChangeDirection()
    {
        goingUp = !goingUp;
        idelMovementDirection.y *= -1;
        attackMovementDirection.y *= -1;
    }

    public void Flip()
    {
        facingLeft = !facingLeft;
        idelMovementDirection.x *= -1;
        attackMovementDirection.x *= -1;
        transform.Rotate(0, 180, 0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(goundCheckUp.position, 0.2f);
        Gizmos.DrawWireSphere(goundCheckDown.position, 0.2f);
        Gizmos.DrawWireSphere(goundCheckWall.position, 0.2f);
    }
}
