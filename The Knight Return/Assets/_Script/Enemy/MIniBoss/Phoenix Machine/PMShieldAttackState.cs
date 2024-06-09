using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PMAttackState : BaseState
{
    private PMStateMachine SM;
    private Animator anim;
    private Rigidbody2D rb;

    public PMAttackState(PMStateMachine stateMachine, Animator animator, Rigidbody2D rib) : base(stateMachine)
    {
        SM = stateMachine;
        anim = animator;
        rb = rib;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Attack");
        SM.StartCoroutine(EndState());
        SM.shield.ShieldMove();

    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

    }

    public override void Exit()
    {
        base.Exit();
        SM.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        SM.shield.StopShieldMove();
    }

    IEnumerator EndState()
    {
        yield return new WaitForSeconds(10f);
        SM.NextState();
    }
}
