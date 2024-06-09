using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMStateMachine : StateMachine
{
    public PMFollowState dashState;
    public PMMovingState movingState;
    public PMAttackState attackState;

    List<BaseState> randomStates;
    private Animator anim;
    private Rigidbody2D rb;
    BaseState LastState;
    BaseState LastTwoState;

    public GameObject player;

    [Header("Spawn Sword")]
    public GameObject swordPrefab;
    public float spawnRadius = 10f;
    public Vector2 previousSpawnPosition;

    [Header("Move")]
    public float moveSpeed = 5f;
    public float moveRadius = 20f;

    [Header("Move")]
    public PMShield shield;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        movingState = new PMMovingState(this, anim, rb);
        dashState = new PMFollowState(this, anim, rb);
        attackState = new PMAttackState(this, anim, rb);

        randomStates = new List<BaseState>() { movingState, dashState, attackState };
    }

    new void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
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

}
