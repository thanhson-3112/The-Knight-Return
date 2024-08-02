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
            anim.SetTrigger("attack");

            yield return new WaitForSeconds(1f);

            SM.GetTarget();
            if (SM.player != null)
            {
                Vector2 direction = (SM.player.transform.position - SM.transform.position).normalized;
                float jumpForceX = direction.x * SM.jumpForce;
                rb.velocity = new Vector2(jumpForceX, 30f);
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
            yield return new WaitForSeconds(1.5f);
            SM.ShakeCam();
            SoundFxManager.instance.PlaySoundFXClip(SM.wallTouch, SM.transform, 1f);
        }
        yield return new WaitForSeconds(1f);
        SM.NextState();
    }

    public override void Exit()
    {
        base.Exit();
        rb.velocity = Vector2.zero;
    }
}
