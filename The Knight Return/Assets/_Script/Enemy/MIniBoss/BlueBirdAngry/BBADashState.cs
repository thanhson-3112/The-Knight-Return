using System.Collections;
using UnityEngine;

public class BBADashState : BaseState
{
    private BBAStateMachine SM;
    private Animator anim;
    private Rigidbody2D rb;

    public BBADashState(BBAStateMachine stateMachine, Animator animator, Rigidbody2D rib) : base(stateMachine)
    {
        SM = stateMachine;
        anim = animator;
        rb = rib;
    }

    public override void Enter()
    {
        base.Enter();
        anim.SetTrigger("attack");
        SM.StartCoroutine(DashAttack());
        
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }

    IEnumerator DashAttack()
    {
        rb.velocity = new Vector2(0, SM.attackMovementSpeed);
        yield return new WaitForSeconds(1); 

        rb.velocity = Vector2.zero;
        SM.FlipTowardsPlayer();
        yield return new WaitForSeconds(0.5f);  

        SM.GetTarget();
        if (SM.player != null)
        {
            Vector3 targetPosition = SM.player.position + new Vector3(0, -1, 0); 
            Vector3 targetDirection = (targetPosition - SM.transform.position).normalized;
            SM.FlipTowardsPlayer();
            rb.velocity = targetDirection * SM.attackPlayerSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        yield return new WaitForSeconds(1f);
        SM.ShakeCam();
        SM.NextState();
    }

    public override void Exit()
    {
        base.Exit();
        rb.velocity = Vector2.zero;
    }
}
