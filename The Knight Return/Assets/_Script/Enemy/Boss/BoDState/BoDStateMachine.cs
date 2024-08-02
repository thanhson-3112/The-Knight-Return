using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoDStateMachine : StateMachine
{
    public BoDMovingState movingState;
    public BoDDashState dashState;
    public BoDSpawnState spawnState;
    public BoDJumpAttackState jumpAttackState;
    public BoDWaveattackState waveAttack;
    public BoDSpellState spellState;
    public BoDMeteorState meteorState;

    List<BaseState> randomStates;
    public Transform target;
    private Animator anim;
    private Rigidbody2D rb;
    BaseState LastState;
    BaseState LastTwoState;

    public float moveSpeed = 5f;
    public float dashSpeed = 30f;

    [Header("JumpAttack")]
    public float jumpForce = 15f;

    [Header("Spawn Enemy")]
    public float bossSpawnRate = 5f; 
    public float spawnRadius = 5f; 
    public GameObject[] enemyPrefabs;

    [Header("Spell")]
    public GameObject spellPrefabs;

    [Header("Wave Attack")]
    public GameObject firePrefab;
    public GameObject firingPoint;
    public Transform firing;

    [Header("Meteor Attack")]
    public GameObject[] meteorPrefab;
    public Transform[] spawnPosition;

    [Header("SoundWave")]
    public GameObject soundWave;
    public Transform soundWavePos;
    private bool stopSoundWave = false;

    [Header("Other")]
    public BoDLifeController BoDLife;
    private float bossHealth;
    public CameraManager cam;
    private bool facingLeft = true;

    [Header("Sound")]
    public AudioClip wallHitSound;
    public AudioClip fireBallSound;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        movingState = new BoDMovingState(this, anim);
        dashState = new BoDDashState(this, anim, rb);
        spawnState = new BoDSpawnState(this, anim, rb);
        jumpAttackState = new BoDJumpAttackState(this, anim, rb);
        waveAttack = new BoDWaveattackState(this, anim);
        spellState = new BoDSpellState(this, anim);
        meteorState = new BoDMeteorState(this,anim, rb);
    }

    new void Start()
    {
        base.Start();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>();
        FlipTowardsPlayer();

        StartCoroutine(SoundWaveLoop());
    }

    new public void Update()
    {
        base.Update();
        if (!canUpdate) return;


        bossHealth = BoDLife.bossHealth;
        if (bossHealth >= 20)
        {
            randomStates = new List<BaseState>() { movingState, jumpAttackState, waveAttack, spawnState, };

        }
        else
        {
            randomStates = new List<BaseState>() { movingState, dashState, spellState, meteorState };

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
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    public void FlipTowardsPlayer()
    {
        float playerDirection = target.position.x - transform.position.x;

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

