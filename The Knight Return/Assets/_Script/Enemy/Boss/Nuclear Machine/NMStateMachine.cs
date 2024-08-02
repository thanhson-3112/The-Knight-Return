using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NMStateMachine : StateMachine
{
    public NMRampageShootState rampageShoot;
    public NMMoveShootState movingShootState;
    public NMJumpAttackState attackShoot;

    List<BaseState> randomStates;
    private Animator anim;
    private Rigidbody2D rb;
    BaseState LastState;
    BaseState LastTwoState;

    [Header("Move")]
    public float MovementSpeed = 5f;

    [Header("JumpAttack")]
    public float jumpForce = 10f;

    [Header("FireAttack")]
    public GameObject firePrefab;
    public GameObject firePrefab1;
    public Transform firing;

    [Header("Other")]
    [SerializeField] Transform checkLeft;
    [SerializeField] Transform checkRight;
    [SerializeField] Transform checkGround;
    [SerializeField] LayerMask groundLayer;
    public Transform player;

    private bool facingLeft = true;

    public bool isLeft;
    public bool isRight;
    public bool isGround;

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

        movingShootState = new NMMoveShootState(this, anim, rb);
        rampageShoot = new NMRampageShootState(this, anim, rb);
        attackShoot = new NMJumpAttackState(this, anim, rb);
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

        isLeft = Physics2D.OverlapCircle(checkLeft.position, 0.5f, groundLayer);
        isRight = Physics2D.OverlapCircle(checkRight.position, 0.5f, groundLayer);
        isGround = Physics2D.OverlapCircle(checkGround.position, 0.5f, groundLayer);

        randomStates = new List<BaseState>() { movingShootState, rampageShoot, attackShoot };
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
        LastState = movingShootState;
        LastTwoState = movingShootState;
        return movingShootState;
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
        MovementSpeed = -MovementSpeed;
    }

    public void ShakeCam()
    {
        cam.ShakeCamera();
    }
}
