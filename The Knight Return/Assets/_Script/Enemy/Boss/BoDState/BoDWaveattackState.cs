using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoDWaveattackState : BaseState
{
    private BoDStateMachine SM;
    private Animator anim;

    public BoDWaveattackState(BoDStateMachine stateMachine, Animator animator) : base(stateMachine)
    {
        SM = stateMachine;
        anim = animator;
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
            while (Vector2.Distance(SM.transform.position, SM.target.position) > 30f)
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
                anim.SetTrigger("BoDRun");
                SM.transform.position = Vector2.MoveTowards(SM.transform.position, SM.target.position, SM.moveSpeed * Time.deltaTime);
                yield return null; 
            }
            Attack();
            yield return new WaitForSeconds(3f);
        }
        SM.NextState();
    }

    private void Attack()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            float distance = Vector2.Distance(SM.transform.position, player.transform.position);
            if (distance <= 30f)
            {
                Vector2 direction = player.transform.position - SM.transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                // Ban ra 3 cau lua theo hình non
                for (int i = 0; i < 3; i++)
                {
                    float offsetAngle = angle + (i - 1) * 30f; 
                    Vector2 bulletDirection = new Vector2(Mathf.Cos(offsetAngle * Mathf.Deg2Rad), Mathf.Sin(offsetAngle * Mathf.Deg2Rad));

                    // Instantiate bullet
                    GameObject spawnedEnemy = GameObject.Instantiate(SM.firePrefab, SM.firing.position, Quaternion.identity);
                    spawnedEnemy.transform.right = bulletDirection;
                    anim.SetBool("BoDAttack", true);
                }
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        anim.SetBool("BoDAttack", false);
        anim.SetTrigger("BoDRun");
        SM.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }
}
