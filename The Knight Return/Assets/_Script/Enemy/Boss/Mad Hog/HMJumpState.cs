using System.Collections;
using UnityEngine;

public class MHJumpState : BaseState
{
    private MHStateMachine SM;
    private Animator anim;
    private Rigidbody2D rb;

    public MHJumpState(MHStateMachine stateMachine, Animator animator, Rigidbody2D rib) : base(stateMachine)
    {
        SM = stateMachine;
        anim = animator;
        rb = rib;
    }

    public override void Enter()
    {
        base.Enter();
        anim.SetBool("MHMove", false);
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
        SM.FlipTowardsPlayer();

        yield return new WaitForSeconds(2f);

        SM.GetTarget();
        if (SM.player != null)
        {
            Vector3 targetPosition = SM.player.position + new Vector3(0, -1, 0);
            Vector3 directionToPlayer = (targetPosition - SM.transform.position).normalized;

            Vector2 jumpVelocity = new Vector2(directionToPlayer.x * SM.jumpForce, SM.jumpForce);

            rb.velocity = jumpVelocity;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        yield return new WaitForSeconds(1f);
        //SM.ShakeCam(); 
        SM.NextState();
    }

    public override void Exit()
    {
        base.Exit();
        rb.velocity = Vector2.zero;
    }
}
