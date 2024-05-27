using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuskJumpState : BaseState
{
    private TuskStateMachine SM;
    private Animator anim;
    private Rigidbody2D rb;

    public TuskJumpState(TuskStateMachine stateMachine, Animator animator, Rigidbody2D rigidbody) : base(stateMachine)
    {
        SM = stateMachine;
        anim = animator;
        rb = rigidbody;
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
        for(int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.1f);
            rb.velocity = Vector2.zero;
            SM.FlipTowardsPlayer();

            yield return new WaitForSeconds(2f);

            SM.GetTarget();
            if (SM.player != null)
            {
                Vector3 targetPosition = SM.player.position + new Vector3(0, -1, 0);
                Vector3 directionToPlayer = (targetPosition - SM.transform.position).normalized;
                Vector2 jumpVelocity = new Vector2(directionToPlayer.x * SM.jumpForce, 20f);
                rb.velocity = jumpVelocity;
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
            yield return new WaitForSeconds(1.5f);
            SM.ShakeCam();
        }
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
