using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;


public class NMJumpAttackState : BaseState
{
    private NMStateMachine SM;
    private Animator anim;
    private Rigidbody2D rb;

    public NMJumpAttackState(NMStateMachine stateMachine, Animator animator, Rigidbody2D rib) : base(stateMachine)
    {
        SM = stateMachine;
        anim = animator;
        rb = rib;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("jump");
        SM.StartCoroutine(Jump());
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }

    IEnumerator Jump()
    {
        yield return new WaitForSeconds(0.1f);
        Vector3 initialPosition = SM.transform.position;
        rb.velocity = Vector2.zero;
        SM.FlipTowardsPlayer();
        anim.SetTrigger("attack");

        yield return new WaitForSeconds(1f);

        // Jump towards the player
        if (SM.player != null)
        {
            Vector2 direction = (SM.player.transform.position - SM.transform.position).normalized;
            float jumpForceX = direction.x * SM.jumpForce;
            rb.velocity = new Vector2(jumpForceX, 40f);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        yield return new WaitForSeconds(1.5f);
        SM.ShakeCam();

        yield return new WaitForSeconds(3f);
        // Jump back to initial position
        Vector2 directionToInitialPosition = (initialPosition - SM.transform.position).normalized;
        float jumpForceXBack = directionToInitialPosition.x * SM.jumpForce;
        rb.velocity = new Vector2(jumpForceXBack, 40f);

        yield return new WaitForSeconds(1.5f);
        SM.ShakeCam();
        yield return new WaitForSeconds(2f);
        SM.NextState();
    }

    public override void Exit()
    {
        base.Exit();
        rb.velocity = Vector2.zero;
    }
}
