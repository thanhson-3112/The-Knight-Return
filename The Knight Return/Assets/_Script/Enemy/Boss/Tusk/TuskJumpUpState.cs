using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuskJumpUpState : BaseState
{
    private TuskStateMachine SM;
    private Animator anim;
    private Rigidbody2D rb;

    public TuskJumpUpState(TuskStateMachine stateMachine, Animator animator, Rigidbody2D rigidbody) : base(stateMachine)
    {
        SM = stateMachine;
        anim = animator;
        rb = rigidbody;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("jumpUp");
        SM.StartCoroutine(Jump());
        SM.StartCoroutine(MoveTowardsPlayerForDuration(7f)); 
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }

    IEnumerator Jump()
    {
        yield return new WaitForSeconds(0.1f);
        rb.velocity = Vector2.zero;
        anim.SetTrigger("attack");

        yield return new WaitForSeconds(1f);

        SM.GetTarget();
        if (SM.player != null)
        {
            Vector3 targetPosition = SM.player.position + new Vector3(0, -1, 0);
            Vector3 directionToPlayer = (targetPosition - SM.transform.position).normalized;
            Vector2 jumpVelocity = new Vector2(directionToPlayer.x * SM.jumpForce, 50f);
            rb.velocity = jumpVelocity;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        yield return new WaitForSeconds(0.7f);
        rb.simulated = false;

        yield return new WaitForSeconds(0.2f);
        SM.StartCoroutine(WaveAttackRoutine());
    }

    IEnumerator WaveAttackRoutine()
    {
        for (int waveIndex = 0; waveIndex < 8; waveIndex++)
        {
            GameObject.Instantiate(SM.firePrefab1, SM.firing.position, Quaternion.identity);
            anim.SetTrigger("attack");
            SoundFxManager.instance.PlaySoundFXClip(SM.shootSound, SM.transform, 1f);
            yield return new WaitForSeconds(0.5f);
        }
        
    }

    private IEnumerator MoveTowardsPlayerForDuration(float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            if (SM.player != null)
            {
                Vector3 targetPosition = SM.player.position + new Vector3(0, 20, 0);
                Vector3 directionToPlayerMove = (targetPosition - SM.transform.position).normalized;
                SM.transform.position += directionToPlayerMove * SM.idleMovementSpeed * Time.deltaTime;
            }
            timer += Time.deltaTime;
            yield return null;
        }
        SM.NextState();
    }

    public override void Exit()
    {
        base.Exit();
        rb.simulated = true;
        rb.velocity = Vector2.zero;
    }
}
