using System.Collections;
using UnityEngine;

public class NMRampageShootState : BaseState
{
    private NMStateMachine SM;
    private Animator anim;
    private Rigidbody2D rb;

    public NMRampageShootState(NMStateMachine stateMachine, Animator animator, Rigidbody2D rib) : base(stateMachine)
    {
        SM = stateMachine;
        anim = animator;
        rb = rib;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("JumpAttack");
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
        Vector2 direction = SM.player.transform.position - SM.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // Ban ra 5 cau lua theo hình non
        for (int i = 0; i < 10; i++)
        {
            float offsetAngle = angle + (i - 1) * 90f;
            Vector2 bulletDirection = new Vector2(Mathf.Cos(offsetAngle * Mathf.Deg2Rad), Mathf.Sin(offsetAngle * Mathf.Deg2Rad));

            GameObject spawnedEnemy = GameObject.Instantiate(SM.firePrefab1, SM.firing.position, Quaternion.identity);
            spawnedEnemy.transform.right = bulletDirection;
            SoundFxManager.instance.PlaySoundFXClip(SM.shootSound, SM.transform, 1f);
        }
    }

    public override void Exit()
    {
        base.Exit();
        SM.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }
}
