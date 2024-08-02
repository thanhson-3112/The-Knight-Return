using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoDMovingState : BaseState
{
    private BoDStateMachine SM;
    public Transform target;
    private Animator anim;

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
        SM.FlipTowardsPlayer();
        SM.transform.position = Vector2.MoveTowards(SM.transform.position, SM.target.position, SM.moveSpeed * Time.deltaTime);
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
