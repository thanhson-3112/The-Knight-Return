using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBAStateMachine : StateMachine
{
    public BBADashState dashState;
    public BBAMovingState movingState;
    public BBAAttackState attackState;

    List<BaseState> randomStates;
    public Transform target;

    public float moveSpeed = 5f;
    public float dashSpeed = 10f;

    private Animator anim;
    BaseState LastState;
    BaseState LastTwoState;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        movingState = new BBAMovingState(this, anim);
        dashState = new BBADashState(this, anim);
        attackState = new BBAAttackState(this, anim);

        randomStates = new List<BaseState>() { movingState, dashState, attackState };
    }

    new void Start()
    {
        base.Start();
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
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

}
