using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FPAttackState : BaseState
{
    private FPStateMachine SM;
    private Animator anim;
    private Rigidbody2D rb;

    private bool facingLeft = true;

    public FPAttackState(FPStateMachine stateMachine, Animator animator, Rigidbody2D rib) : base(stateMachine)
    {
        SM = stateMachine;
        anim = animator;
        rb = rib;
    }

    public override void Enter()
    {
        base.Enter();
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
            yield return new WaitForSeconds(0.5f);
        }
        SM.NextState();
    }

    private void Attack()
    {
        Vector2 direction = SM.player.transform.position - SM.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Ban ra 3 cau lua theo hình non
        for (int i = 0; i < 12; i++)
        {
            float offsetAngle = angle + (i - 1) * 30f;
            Vector2 bulletDirection = new Vector2(Mathf.Cos(offsetAngle * Mathf.Deg2Rad), Mathf.Sin(offsetAngle * Mathf.Deg2Rad));

            // Instantiate bullet
            GameObject spawnedEnemy = GameObject.Instantiate(SM.firePrefab, SM.firing.position, Quaternion.identity);
            spawnedEnemy.transform.right = bulletDirection;
        }
    }

    public override void Exit()
    {
        base.Exit();
        anim.SetTrigger("BoDRun");
        SM.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }
}
