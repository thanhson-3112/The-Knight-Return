using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoDJumpAttackState : BaseState
{
    private BoDStateMachine SM;
    private Animator anim;
    private Rigidbody2D rb;


    public BoDJumpAttackState(BoDStateMachine stateMachine, Animator animator, Rigidbody2D rib) : base(stateMachine)
    {
        SM = stateMachine;
        anim = animator;
        rb = rib;
    }

    public override void Enter()
    {
        base.Enter();
        anim.SetTrigger("BoDRun");

        SM.FlipTowardsPlayer();

        SM.StartCoroutine(Jump());
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }

    IEnumerator Jump()
    {
        yield return new WaitForSeconds(0.1f);
        rb.velocity = Vector2.zero;

        // Jump towards the player
        SM.GetTarget();
        if (SM.target != null)
        {
            Vector2 direction = (SM.target.transform.position - SM.transform.position).normalized;
            float jumpForceX = direction.x * SM.jumpForce;
            rb.velocity = new Vector2(jumpForceX, 10f);
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Ground"), true);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        yield return new WaitForSeconds(1.5f);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Ground"), false);
        anim.SetTrigger("BoDAttack");
        SM.ShakeCam();
        yield return new WaitForSeconds(1f);
        SM.NextState();
    }

    public override void Exit()
    {
        base.Exit();
        rb.velocity = Vector2.zero;
    }
}
