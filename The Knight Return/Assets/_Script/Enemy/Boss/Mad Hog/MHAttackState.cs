using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MHAttackState : BaseState
{
    private MHStateMachine SM;
    private Animator anim;
    private Rigidbody2D rb;

    public MHAttackState(MHStateMachine stateMachine, Animator animator, Rigidbody2D rib) : base(stateMachine)
    {
        SM = stateMachine;
        anim = animator;
        rb = rib;
    }

    public override void Enter()
    {
        base.Enter();
        anim.SetBool("MHMove", false);
        SM.StartCoroutine(RampageAttack());
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }

    IEnumerator RampageAttack()
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
            Vector2 jumpVelocity = new Vector2(-directionToPlayer.x * SM.jumpForce -5f, 15f);
            rb.velocity = jumpVelocity;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        yield return new WaitForSeconds(1.5f);

        // Quay trái ph?i liên t?c 4 l?n
        for (int i = 0; i < 6; i++)
        {
            SM.Flip();
            anim.SetTrigger("MHAttack");
            SM.ShakeCam();
            yield return new WaitForSeconds(0.5f); 

            for (int j = 0; j < 5; j++)
            {
                int randomIndex = Random.Range(0, SM.spawnPosition.Length);

                int rand = Random.Range(0, SM.SpikesPrefabs.Length);
                GameObject SpikesToSpawn = SM.SpikesPrefabs[rand];

                Object.Instantiate(SpikesToSpawn, SM.spawnPosition[randomIndex].position, Quaternion.identity);
            }
            
        }

        yield return new WaitForSeconds(1f);
        SM.NextState();
    }

    public override void Exit()
    {
        base.Exit();
        rb.velocity = Vector2.zero;
    }
}
