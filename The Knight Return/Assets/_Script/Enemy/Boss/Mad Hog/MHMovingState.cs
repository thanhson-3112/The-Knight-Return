using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MHMovingState : BaseState
{
    private MHStateMachine SM;
    private Animator anim;
    private Rigidbody2D rb;

    public MHMovingState(MHStateMachine stateMachine, Animator animator, Rigidbody2D rib) : base(stateMachine)
    {
        SM = stateMachine;
        anim = animator;
        rb = rib;
    }

    public override void Enter()
    {
        base.Enter();
        anim.SetBool("MHMove", true);
        SM.StartCoroutine(Dash());

    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        SM.GetTarget();
        
    }

    IEnumerator Dash()
    {
        yield return new WaitForSeconds(0.1f);
        rb.velocity = Vector2.zero;
        SM.FlipTowardsPlayer();

        yield return new WaitForSeconds(1f);

        SM.GetTarget();
        if (SM.player != null)
        {
            Vector3 targetPosition = SM.player.position;
            Vector3 directionToPlayer = (targetPosition - SM.transform.position).normalized;
            Vector2 dashVelocity = new Vector2(directionToPlayer.x * SM.dashSpeed, 0);
            rb.velocity = dashVelocity;
            
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        yield return new WaitForSeconds(0.2f);
        anim.SetTrigger("MHAttack");

        yield return new WaitForSeconds(1f);
        //SM.ShakeCam(); 
        SM.NextState();
    }


    public override void Exit()
    {
        base .Exit();
        SM.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    IEnumerator EndState()
    {
        yield return new WaitForSeconds(5f);
        SM.NextState();
    }

}
