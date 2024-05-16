using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MHWaveAttack : BaseState
{
    private MHStateMachine SM;
    private Animator anim;
    private Rigidbody2D rb;

    public MHWaveAttack(MHStateMachine stateMachine, Animator animator, Rigidbody2D rib) : base(stateMachine)
    {
        SM = stateMachine;
        anim = animator;
        rb = rib;
    }

    public override void Enter()
    {
        base.Enter();
        anim.SetBool("MHMove", false);
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
        SM.FlipTowardsPlayer();

        yield return new WaitForSeconds(2f);

        SM.GetTarget();
        if (SM.player != null)
        {
            Vector3 targetPosition = SM.player.position + new Vector3(0, -1, 0);
            Vector3 directionToPlayer = (targetPosition - SM.transform.position).normalized;
            Vector2 jumpVelocity = new Vector2(-directionToPlayer.x * SM.jumpForce - 2f, 15f);
            rb.velocity = jumpVelocity;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        yield return new WaitForSeconds(1.5f);
        SM.FlipTowardsPlayer();

        // wave attack
        Vector3 shootDirection = -SM.transform.right;
        if (SM.transform.localScale.x < 0)
        {
            shootDirection = SM.transform.right;
        }

        GameObject fireBall = Object.Instantiate(SM.wavePrefabs, SM.wavePosition.position, Quaternion.identity);
        fireBall.transform.right = shootDirection;

        for (int j = 0; j < 5; j++)
        {
            int randomIndex = Random.Range(0, SM.spawnPosition.Length);

            int rand = Random.Range(0, SM.SpikesPrefabs.Length);
            GameObject SpikesToSpawn = SM.SpikesPrefabs[rand];

            Object.Instantiate(SpikesToSpawn, SM.spawnPosition[randomIndex].position, Quaternion.identity);
        }

        anim.SetTrigger("MHAttack");
        SM.ShakeCam();

        yield return new WaitForSeconds(1f);
        SM.NextState();
    }

    public override void Exit()
    {
        base.Exit();
        rb.velocity = Vector2.zero;
    }
}
