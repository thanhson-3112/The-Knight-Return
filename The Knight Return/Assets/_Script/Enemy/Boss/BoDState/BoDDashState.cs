using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoDDashState : BaseState
{
    private BoDStateMachine SM;
    public int DashNumber = 3;
    private Animator anim;
    private Rigidbody2D rb;

    public BoDDashState(BoDStateMachine stateMachine, Animator animator, Rigidbody2D rib, int dashNumber = 3) : base(stateMachine)
    {
        DashNumber = dashNumber;
        SM = stateMachine;
        anim = animator;
        rb = rib;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Dash");
        SM.StartCoroutine(Dash());

    }

    IEnumerator Dash()
    {
        for (int i = 0; i < DashNumber; i++)
        {
            Vector3 targetDirection = (SM.target.position - SM.transform.position).normalized;
            SM.FlipTowardsPlayer();
            SM.gameObject.GetComponent<Rigidbody2D>().velocity = targetDirection * SM.dashSpeed;

            anim.SetTrigger("BoDAttack");
            yield return new WaitForSeconds(2f);
        }

        SM.NextState();
    }

    public override void UpdateLogic()
        {
            base.UpdateLogic();
            SM.GetTarget();
        }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("BoDTakeHit"))
        {
            anim.SetTrigger("BoDAttack");
        }
    }

    public override void Exit()
    {
        base.Exit();
        SM.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        anim.SetTrigger("BoDRun");
    }
}
