using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoDMeteorState : BaseState
{
    private BoDStateMachine SM;
    public Transform target;
    private Animator anim;
    private Rigidbody2D rb;

    public BoDMeteorState(BoDStateMachine stateMachine, Animator animator, Rigidbody2D rib) : base(stateMachine)
    {
        SM = stateMachine;
        anim = animator;
        rb = rib;
    }

    public override void Enter()
    {
        base.Enter();
        SM.StartCoroutine(MeteorAttack());
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }

    IEnumerator MeteorAttack()
    {
        yield return new WaitForSeconds(2f);

        for (int j = 0; j < 40; j++)
        {
            int randomIndex = Random.Range(0, SM.spawnPosition.Length);
            int rand = Random.Range(0, SM.meteorPrefab.Length);
            GameObject SpikesToSpawn = SM.meteorPrefab[rand];

            Object.Instantiate(SpikesToSpawn, SM.spawnPosition[randomIndex].position, Quaternion.identity);
            anim.SetBool("BoDCastSpell", true);

            yield return new WaitForSeconds(0.3f);
        }

        anim.SetBool("BoDCastSpell", false);

        yield return new WaitForSeconds(1f);
        SM.NextState();
    }

    public override void Exit()
    {
        base.Exit();
        anim.SetBool("BoDCastSpell", false);
        rb.velocity = Vector2.zero;
    }
}
