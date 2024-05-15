using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MHAttackState : BaseState
{
    private MHStateMachine SM;
    private Animator anim;
    private Rigidbody2D rb;

    private bool facingLeft = true;

    public MHAttackState(MHStateMachine stateMachine, Animator animator, Rigidbody2D rib) : base(stateMachine)
    {
        SM = stateMachine;
        anim = animator;
        rb = rib;
    }

    public override void Enter()
    {
        base.Enter();
        SM.NextState();


    }

    public override void UpdatePhysics()
    {
        
    }

    public override void Exit()
    {
        base.Exit();
        SM.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    IEnumerator EndState()
    {
        yield return new WaitForSeconds(5f);
        SM.NextState();
    }
}
