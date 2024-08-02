using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuskMovingState : BaseState
{
    private TuskStateMachine SM;
    private Animator anim;
    private Rigidbody2D rb;
    private int moveRound = 0;

    public TuskMovingState(TuskStateMachine stateMachine, Animator animator, Rigidbody2D rigidbody) : base(stateMachine)
    {
        SM = stateMachine;
        anim = animator;
        rb = rigidbody;
    }

    public override void Enter()
    {
        Debug.Log("move");
        base.Enter();
        anim.SetBool("isMoving", true);
        moveRound = 0;
        SM.transform.eulerAngles = Vector3.zero;
        SM.StartCoroutine(EndState());
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        SM.GetTarget();
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        SM.GetTarget();

        if (moveRound == 0)
        {
            rb.velocity = new Vector2(-SM.idleMovementSpeed, rb.velocity.y);

            if (SM.isTouchingWall)
            {
                SM.Flip();
                moveRound = 1;
            }
        }
        else if (moveRound == 1)
        {
            rb.velocity = new Vector2(SM.idleMovementSpeed, rb.velocity.y);

            if (SM.isTouchingWall)
            {
                rb.velocity = new Vector2(rb.velocity.x, SM.idleMovementSpeed * 4f);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        SM.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    IEnumerator EndState()
    {
        yield return new WaitForSeconds(7f);
        SM.NextState();
    }
}
