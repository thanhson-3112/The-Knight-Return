using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FPMovingState : BaseState
{
    private FPStateMachine SM;
    public Transform target;
    private Animator anim;

    private bool facingLeft = true;
    private Rigidbody2D rb;

    public FPMovingState(FPStateMachine stateMachine, Animator animator, Rigidbody2D rib) : base(stateMachine)
    {
        SM = stateMachine;
        anim = animator;
        rb = rib;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("move");
        SM.StartCoroutine(EndState());
        anim.SetBool("isMoving", false);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        SM.GetTarget();
    }

    public override void UpdatePhysics()
    {
        if (SM.isTouchingUp && SM.goingUp)
        {
            SM.ChangeDirection();
        }
        else if (SM.isTouchingDown && !SM.goingUp)
        {
            SM.ChangeDirection();
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
        rb.velocity = SM.idelMovementSpeed * SM.idelMovementDirection;
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
