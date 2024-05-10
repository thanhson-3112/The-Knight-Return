using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBAAttackState : BaseState
{
    private BBAStateMachine SM;
    private Animator anim;
    private bool hasAttacked;
    private bool isAttacking;

    public BBAAttackState(BBAStateMachine stateMachine, Animator animator) : base("Attack", stateMachine)
    {
        SM = stateMachine;
        anim = animator;
    }

    public override void Enter()
    {
        base.Enter();
        /*anim.SetTrigger("BoDAttack");*/
        hasAttacked = false;
        isAttacking = false;
        Debug.Log("Enter state: BoDAttackState");
        SM.NextState();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        SM.GetTarget();

        if (!hasAttacked)
        {
            hasAttacked = true;
            isAttacking = true;
        }

/*        if (isAttacking && !anim.GetCurrentAnimatorStateInfo(0).IsName("BoDAttack"))
        {
            isAttacking = false;
            anim.SetTrigger("BoDRun");

        }*/
    }

    public override void Exit()
    {
        base.Exit();
        hasAttacked = false;
        Debug.Log("Exit state: BoDAttackState");
    }
}
