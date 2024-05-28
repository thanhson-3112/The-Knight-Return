using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuskAttackState : BaseState
{
    private TuskStateMachine SM;
    private Animator anim;
    private Rigidbody2D rb;
    private int moveRound;

    public TuskAttackState(TuskStateMachine stateMachine, Animator animator, Rigidbody2D rib) : base(stateMachine)
    {
        SM = stateMachine;
        anim = animator;
        rb = rib;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Attack");
        SM.FlipTowardsPlayer();
        SM.StartCoroutine(WaveAttackRoutine());
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

    IEnumerator WaveAttackRoutine()
    {
        for (int waveIndex = 0; waveIndex < 3; waveIndex++)
        {
            Attack();
            anim.SetTrigger("attack");
            yield return new WaitForSeconds(1f);
        }
        SM.NextState();
    }

    private void Attack()
    {
        for (int i = 0; i < 7; i++)
        {
            float offsetAngle =  (i - 1) * 90f;
            Vector2 bulletDirection = new Vector2(Mathf.Cos(offsetAngle * Mathf.Deg2Rad), Mathf.Sin(offsetAngle * Mathf.Deg2Rad));

            // Instantiate bullet
            GameObject spawnedEnemy = GameObject.Instantiate(SM.firePrefab, SM.firing.position, Quaternion.identity);
            spawnedEnemy.transform.right = bulletDirection;
        }
    }

    public override void Exit()
    {
        base.Exit();
        SM.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }
}
