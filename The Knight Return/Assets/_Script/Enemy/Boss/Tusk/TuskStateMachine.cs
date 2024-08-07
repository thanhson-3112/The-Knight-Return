using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuskStateMachine : StateMachine
{
    public TuskJumpState jumpState;
    public TuskMovingState movingState;
    public TuskAttackState attackState;
    public TuskJumpUpState jumpUpState;

    List<BaseState> randomStates;
    private Animator anim;
    private Rigidbody2D rb;
    BaseState LastState;
    BaseState LastTwoState;

    [Header("Move")]
    public float idleMovementSpeed = 15f;

    [Header("JumpAttack")]
    public float jumpForce = 10f;

    [Header("Wave Attack")]
    public GameObject firePrefab;
    public GameObject firePrefab1;
    public Transform firing;
    [Range(0.1f, 2f)]
    public float fireRate = 0.8f;


    public Transform player;

    [Header("Other")]
    [SerializeField] Transform goundCheckDown;
    [SerializeField] Transform goundCheckWall;
    [SerializeField] LayerMask groundLayer;

    public bool facingLeft = true;

    public bool isTouchingDown;
    public bool isTouchingWall;

    public CameraManager cam;
    public GameObject soundWave;
    public Transform soundWavePos;
    private bool stopSoundWave = false;

    [Header("Sound")]
    public AudioClip wallTouch;
    public AudioClip shootSound;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        movingState = new TuskMovingState(this, anim, rb);
        jumpState = new TuskJumpState(this, anim, rb);
        attackState = new TuskAttackState(this, anim, rb);
        jumpUpState = new TuskJumpUpState(this, anim, rb);

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
        if (!canUpdate) return;

        isTouchingDown = Physics2D.OverlapCircle(goundCheckDown.position, 0.2f, groundLayer);
        isTouchingWall = Physics2D.OverlapCircle(goundCheckWall.position, 0.2f, groundLayer);

        randomStates = new List<BaseState>() { movingState, jumpState, attackState, jumpUpState };
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

    public void Flip()
    {
        facingLeft = !facingLeft;
        transform.Rotate(0, 180, 0);
    }

    public void ShakeCam()
    {
        cam.ShakeCamera();
    }
}
