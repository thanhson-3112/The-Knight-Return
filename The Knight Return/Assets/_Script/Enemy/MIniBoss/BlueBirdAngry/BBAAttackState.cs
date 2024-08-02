using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BBAAttackState : BaseState
{
    private BBAStateMachine SM;
    private Animator anim;
    private Rigidbody2D rb;

    private bool facingLeft = true;

    public BBAAttackState(BBAStateMachine stateMachine, Animator animator, Rigidbody2D rib) : base(stateMachine)
    {
        SM = stateMachine;
        anim = animator;
        rb = rib;
    }

    public override void Enter()
    {
        base.Enter();
        SM.StartCoroutine(EndState());
        anim.SetBool("isMoving", true);
    }

    public override void UpdatePhysics()
    {
        if (SM.isTouchingUp && SM.goingUp)
        {
            SM.ChangeDirection();
            SM.ShakeCam();
        }
        else if (SM.isTouchingDown && !SM.goingUp)
        {
            SM.ChangeDirection();
            SM.ShakeCam();
        }

        if (SM.isTouchingWall)
        {
            if (facingLeft)
            {
                SM.Flip();
            }
            else if (!facingLeft)
            {
                SM.Flip();
            }
        }
        rb.velocity = SM.attackMovementSpeed * SM.attackMovementDirection;
    }

    public override void Exit()
    {
        base.Exit();
        SM.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    IEnumerator EndState()
    {
        yield return new WaitForSeconds(5f);
        SM.NextState();
    }
}
