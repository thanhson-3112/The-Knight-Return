using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

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
        Debug.Log("waveAttack");
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
            while (Vector2.Distance(SM.transform.position, SM.target.position) > 20f)
            {
                SM.FlipTowardsPlayer();
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
            if (distance <= 20f)
            {
                Vector2 direction = player.transform.position - SM.transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                // B?n ra 3 c?u l?a theo hình nón
                for (int i = 0; i < 12; i++)
                {
                    float offsetAngle = angle + (i - 1) * 30f;
                    Vector2 bulletDirection = new Vector2(Mathf.Cos(offsetAngle * Mathf.Deg2Rad), Mathf.Sin(offsetAngle * Mathf.Deg2Rad));

                    // sinh ra cau lua
                    GameObject spawnedEnemy = GameObject.Instantiate(SM.firePrefab, SM.firing.position, Quaternion.identity);
                    spawnedEnemy.transform.right = bulletDirection;
                    SoundFxManager.instance.PlaySoundFXClip(SM.fireBallSound, SM.transform, 1);
                    anim.SetTrigger("BoDAttack");
                }
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        anim.SetTrigger("BoDRun");
        SM.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }
}
