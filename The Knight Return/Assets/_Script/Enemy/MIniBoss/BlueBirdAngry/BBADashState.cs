using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BBADashState : BaseState
{
    private BBAStateMachine SM;
    public int DashNumber = 3;
    private Animator anim;

    public BBADashState(BBAStateMachine stateMachine, Animator animator, int dashNumber = 3) : base("Dash", stateMachine)
    {
        DashNumber = dashNumber;
        SM = stateMachine;
        anim = animator;
    }

    public override void Enter()
    {
        base.Enter();
        SM.StartCoroutine(Dash());
    }

    IEnumerator Dash()
    {
        for (int i = 0; i < DashNumber; i++)
        {
            Vector3 targetDirection = (SM.target.position - SM.transform.position).normalized;
            if (targetDirection.x > 0)
            {
                SM.transform.localScale = new Vector3(-Mathf.Abs(SM.transform.localScale.x), SM.transform.localScale.y, SM.transform.localScale.z);
            }
            else
            {
                SM.transform.localScale = new Vector3(Mathf.Abs(SM.transform.localScale.x), SM.transform.localScale.y, SM.transform.localScale.z);
            }
            SM.GetTarget();
            SM.gameObject.GetComponent<Rigidbody2D>().velocity = targetDirection * SM.dashSpeed;

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
    }

    public override void Exit()
    {
        base.Exit();
        SM.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }
}
