using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BBAAttackState : BaseState
{
    private BBAStateMachine SM;
    private Animator anim;
    private Rigidbody2D rb;


    public BBAAttackState(BBAStateMachine stateMachine, Animator animator, Rigidbody2D rib) : base("Attack", stateMachine)
    {
        SM = stateMachine;
        anim = animator;
        rb = rib;
    }

    public override void Enter()
    {
        base.Enter();
        SM.StartCoroutine(EndState());
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        SM.GetTarget();

        if (SM.player != null)
        {
            Vector2 playerPosition = SM.player.position;
            Vector2 directionToPlayer = (playerPosition - (Vector2)SM.transform.position).normalized;

            SM.FlipTowardsPlayer();

            // D?ng trong kho?ng th?i gian 0.3 giây
            SM.StartCoroutine(AttackAfterDelay(directionToPlayer, playerPosition, 1f));
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    IEnumerator AttackAfterDelay(Vector2 directionToPlayer, Vector2 playerPosition, float delay)
    {
        yield return new WaitForSeconds(delay);
        Vector2 attackDirection = (playerPosition - (Vector2)SM.transform.position).normalized;
        rb.velocity = attackDirection * SM.attackPlayerSpeed;
    }


    public override void Exit()
    {
        base.Exit();
    }

    IEnumerator EndState()
    {
        yield return new WaitForSeconds(2f);
        SM.NextState();
    }
}
