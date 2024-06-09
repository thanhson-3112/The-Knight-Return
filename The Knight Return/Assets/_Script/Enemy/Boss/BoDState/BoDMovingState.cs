using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoDMovingState : BaseState
{
    private BoDStateMachine SM;
    public Transform target;
    private Animator anim;

    public float activationDistance = 10f;
    private bool isAttacking = false;

    public BoDMovingState(BoDStateMachine stateMachine, Animator animator) : base(stateMachine)
    {
        SM = stateMachine;
        anim = animator;
    }

    public override void Enter()
    {
        base.Enter();
        anim.SetTrigger("BoDRun");
        SM.StartCoroutine(EndState());

    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        SM.GetTarget();
    }

    public override void UpdatePhysics()
    {
        Vector3 targetDirection = SM.target.position - SM.transform.position;
        if (targetDirection.x > 0)
        {
            SM.transform.localScale = new Vector3(-Mathf.Abs(SM.transform.localScale.x), SM.transform.localScale.y, SM.transform.localScale.z);
        }
        else
        {
            SM.transform.localScale = new Vector3(Mathf.Abs(SM.transform.localScale.x), SM.transform.localScale.y, SM.transform.localScale.z);
        }
        SM.transform.position = Vector2.MoveTowards(SM.transform.position, SM.target.position, SM.moveSpeed * Time.deltaTime);

        // Tan cong khi gan player
        float distanceToPlayer = Vector2.Distance(SM.transform.position, SM.target.position);
        if (distanceToPlayer <= activationDistance && !isAttacking)
        {
            SM.ChangeState(SM.attackState);
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }
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
