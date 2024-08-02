using System.Collections;
using UnityEngine;

public class NMMoveShootState : BaseState
{
    private NMStateMachine SM;
    private Animator anim;
    private Rigidbody2D rb;
    private Coroutine waveAttackCoroutine;

    public NMMoveShootState(NMStateMachine stateMachine, Animator animator, Rigidbody2D rib) : base(stateMachine)
    {
        SM = stateMachine;
        anim = animator;
        rb = rib;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("move");
        anim.SetBool("isMoving", true); 
        if (waveAttackCoroutine != null)
        {
            SM.StopCoroutine(waveAttackCoroutine);
        }
        waveAttackCoroutine = SM.StartCoroutine(WaveAttackRoutine());
        SM.StartCoroutine(EndState());
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        SM.GetTarget();
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        SM.transform.position += Vector3.right * SM.MovementSpeed * Time.deltaTime;

        if (!SM.isLeft && SM.MovementSpeed < 0 || !SM.isRight && SM.MovementSpeed > 0)
        {
            SM.Flip();
        }
    }

    private IEnumerator WaveAttackRoutine()
    {
        while (true)
        {
            Attack();
            anim.SetTrigger("attack");
            yield return new WaitForSeconds(1f);
        }
    }

    private void Attack()
    {
        Vector2 direction = SM.player.transform.position - SM.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // ban qua cau lua
        for (int i = 1; i < 2; i++)
        {
            float offsetAngle = angle + (i - 1) * 45f;
            Vector2 bulletDirection = new Vector2(Mathf.Cos(offsetAngle * Mathf.Deg2Rad), Mathf.Sin(offsetAngle * Mathf.Deg2Rad));

            // Instantiate bullet
            GameObject spawnedEnemy = GameObject.Instantiate(SM.firePrefab, SM.firing.position, Quaternion.identity);
            spawnedEnemy.transform.right = bulletDirection;
            SoundFxManager.instance.PlaySoundFXClip(SM.shootSound, SM.transform, 1f);
        }
    }

    public override void Exit()
    {
        base.Exit();
        SM.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        anim.SetBool("isMoving", false); // Reset moving animation

        if (waveAttackCoroutine != null)
        {
            SM.StopCoroutine(waveAttackCoroutine);
        }
    }

    private IEnumerator EndState()
    {
        yield return new WaitForSeconds(10f);
        SM.NextState();
    }
}
