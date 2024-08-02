using System.Collections;
using UnityEngine;

public class PMFollowState : BaseState
{
    private PMStateMachine SM;
    private Animator anim;
    private Rigidbody2D rb;

    public PMFollowState(PMStateMachine stateMachine, Animator animator, Rigidbody2D rib) : base(stateMachine)
    {
        SM = stateMachine;
        anim = animator;
        rb = rib;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Flower");
        SM.NextState();

    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }

    

    public override void Exit()
    {
        base.Exit();
        rb.velocity = Vector2.zero;
    }
}
