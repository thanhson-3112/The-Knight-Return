using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBAStateMachine : StateMachine
{
    public BBADashState dashState;
    public BBAMovingState movingState;
    public BBAAttackState attackState;

    List<BaseState> randomStates;
    private Animator anim;
    private Rigidbody2D rb;
    BaseState LastState;
    BaseState LastTwoState;

    [Header("Idel")]
    public float idelMovementSpeed = 2f;
    public Vector2 idelMovementDirection = new Vector2(-5, 2);

    [Header("AttackUpNDown")]
    public float attackMovementSpeed = 20f;
    public Vector2 attackMovementDirection = new Vector2(-1, 2);

    [Header("AttackPlayer")]
    public float attackPlayerSpeed = 25f;
    public Transform player;

    [Header("Other")]
    [SerializeField] Transform goundCheckUp;
    [SerializeField] Transform goundCheckDown;
    [SerializeField] Transform goundCheckWall;
    [SerializeField] LayerMask groundLayer;

    private bool facingLeft = true;
    public bool goingUp = true;

    public bool isTouchingUp;
    public bool isTouchingDown;
    public bool isTouchingWall;

    public CameraManager cam;
    public GameObject soundWave;
    public Transform soundWavePos;
    private bool stopSoundWave = false;

    [Header("Sound")]
    public AudioClip wallTouch;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        movingState = new BBAMovingState(this, anim, rb);
        dashState = new BBADashState(this, anim, rb);
        attackState = new BBAAttackState(this, anim, rb);
    }

    new void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>();
        StartCoroutine(SoundWaveLoop());
    }

    new public void Update()
    {
        base.Update();
        if (!canUpdate) return; // Ensure no action is taken until allowed

        isTouchingUp = Physics2D.OverlapCircle(goundCheckUp.position, 0.2f, groundLayer);
        isTouchingDown = Physics2D.OverlapCircle(goundCheckDown.position, 0.2f, groundLayer);
        isTouchingWall = Physics2D.OverlapCircle(goundCheckWall.position, 0.2f, groundLayer);

        randomStates = new List<BaseState>() { movingState, dashState, attackState };
        stopSoundWave = true;

    }

    IEnumerator SoundWaveLoop()
    {
        while (!stopSoundWave)
        {
            Instantiate(soundWave, soundWavePos.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
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

    public void ShakeCam()
    {
        cam.ShakeCamera();
    }
}
