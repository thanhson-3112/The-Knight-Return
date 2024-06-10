using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MHStateMachine : StateMachine
{
    public MHJumpState jumpState;
    public MHMovingState movingState;
    public MHAttackState attackState;
    public MHWaveAttack waveAttack;

    List<BaseState> randomStates;
    private Animator anim;
    private Rigidbody2D rb;
    BaseState LastState;
    BaseState LastTwoState;

    [Header("Jump")]
    public float jumpForce = 15f;
    public Transform player;

    [Header("DashAttack")]
    public float dashSpeed = 10f;
    public GameObject[] SpikesPrefabs;
    public Transform[] spawnPosition;

    [Header("WaveAttack")]
    public GameObject wavePrefabs;
    public Transform wavePosition;

    [Header("Other")]
    private bool facingLeft = true;
    public CameraManager cam;

    public MHLifeController MHLife;
    private float bossHealth;

    public GameObject soundWave;
    public Transform soundWavePos;
    private bool stopSoundWave = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        movingState = new MHMovingState(this, anim, rb);
        jumpState = new MHJumpState(this, anim, rb);
        attackState = new MHAttackState(this, anim, rb);
        waveAttack = new MHWaveAttack(this, anim, rb);
    }

    new void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>();
        FlipTowardsPlayer();
        anim.SetBool("MHMove", false);
        ShakeCam();
        MHLife = GetComponent<MHLifeController>();
        StartCoroutine(SoundWaveLoop());
    }

    new public void Update()
    {
        base.Update();
        if (!canUpdate) return; // Ensure no action is taken until allowed

        bossHealth = MHLife.bossHealth;

        if (bossHealth >= 10)
        {
            randomStates = new List<BaseState>() { movingState, jumpState, attackState };
        }
        else
        {
            randomStates = new List<BaseState>() { movingState, jumpState, attackState, waveAttack };
        }

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
