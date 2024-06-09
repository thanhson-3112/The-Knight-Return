using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoDSpellState : BaseState
{
    private BoDStateMachine SM;
    private Animator anim;
    private bool spawning = true;

    public BoDSpellState(BoDStateMachine stateMachine, Animator animator) : base(stateMachine)
    {
        SM = stateMachine;
        anim = animator;
    }

    public override void Enter()
    {
        base.Enter();
        spawning = true;
        anim.SetTrigger("BoDCastSpell");
        SM.StartCoroutine(Spawner());
        SpawnSpell();
        SM.StartCoroutine(EndState());
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

    }
    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(0);

        while (spawning)
        {
            anim.SetTrigger("BoDCastSpell");
            yield return wait;
        }
    }

    private void SpawnSpell()
    {
        GameObject spellToSpawn = SM.spellPrefabs;

        Vector3 spawnPosition = SM.transform.position + Random.insideUnitSphere * SM.spawnRadius;
        spawnPosition.z = 0f;

        GameObject spawnedSpell = GameObject.Instantiate(spellToSpawn, spawnPosition, Quaternion.identity);
    }

    public override void Exit()
    {
        base.Exit();
        SM.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        spawning = false;
    }

    IEnumerator EndState()
    {
        yield return new WaitForSeconds(10f);
        SM.NextState();
    }
}
