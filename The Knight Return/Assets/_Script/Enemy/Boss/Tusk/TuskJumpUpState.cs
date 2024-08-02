using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuskJumpUpState : BaseState
{
    private TuskStateMachine SM;
    private Animator anim;
    private Rigidbody2D rb;

    public TuskJumpUpState(TuskStateMachine stateMachine, Animator animator, Rigidbody2D rigidbody) : base(stateMachine)
    {
        SM = stateMachine;
        anim = animator;
        rb = rigidbody;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("jumpUp");
        SM.StartCoroutine(Jump());
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }

    IEnumerator Jump()
    {
        yield return new WaitForSeconds(0.1f);
        rb.velocity = Vector2.zero;
        anim.SetTrigger("attack");

        yield return new WaitForSeconds(1f);

        SM.GetTarget();
        if (SM.player != null)
        {
            Vector3 targetPosition = SM.player.position + new Vector3(0, -1, 0);
            Vector3 directionToPlayer = (targetPosition - SM.transform.position).normalized;
            Vector2 jumpVelocity = new Vector2(directionToPlayer.x * SM.jumpForce, 50f);
            rb.velocity = jumpVelocity;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        yield return new WaitForSeconds(0.7f);
        rb.simulated = false;

        SM.StartCoroutine(WaveAttackRoutine());

        yield return new WaitForSeconds(7f);
        SM.NextState();
    }

    IEnumerator WaveAttackRoutine()
    {
        for (int waveIndex = 0; waveIndex < 3; waveIndex++)
        {
            Attack();
            anim.SetTrigger("attack");
            yield return new WaitForSeconds(1f);
        }
        Attack();
    }

    private void Attack()
    {
        for (int i = 0; i < 3; i++)
        {
            float offsetAngle = (i - 1) * 90f;
            Vector2 bulletDirection = new Vector2(Mathf.Cos(offsetAngle * Mathf.Deg2Rad), Mathf.Sin(offsetAngle * Mathf.Deg2Rad));

            // Instantiate bullet
            GameObject spawnedEnemy = GameObject.Instantiate(SM.firePrefab, SM.firing.position, Quaternion.identity);
            spawnedEnemy.transform.right = bulletDirection;
        }
    }



    public override void Exit()
    {
        base.Exit();
        rb.simulated = true;
        rb.velocity = Vector2.zero;
    }
}
