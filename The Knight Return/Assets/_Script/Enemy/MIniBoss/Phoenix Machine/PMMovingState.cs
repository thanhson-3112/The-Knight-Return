using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PMMovingState : BaseState
{
    private PMStateMachine SM;
    private Animator anim;
    private Rigidbody2D rb;
    private bool isMoveState = false;

    public PMMovingState(PMStateMachine stateMachine, Animator animator, Rigidbody2D rib) : base(stateMachine)
    {
        SM = stateMachine;
        anim = animator;
        rb = rib;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("move");
        SM.StartCoroutine(EndState());
        anim.SetBool("isMoving", false);
        isMoveState = true;
        SM.StartCoroutine(MoveAroundPlayer());
        SM.StartCoroutine(SpawnSword());
        
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }

    private IEnumerator MoveAroundPlayer()
    {
        while (isMoveState == true)
        {
            // Tính m?t góc ng?u nhiên
            float randomAngle = Random.Range(0f, 360f);

            float randomDistance = Random.Range(0f, SM.moveRadius);

            // Tinh vi tri xung quanh nguoi choi
            Vector2 targetPosition = (Vector2)SM.player.transform.position + new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad)) * randomDistance;

            while ((Vector2)SM.transform.position != targetPosition)
            {
                SM.transform.position = Vector2.MoveTowards(SM.transform.position, targetPosition, SM.moveSpeed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator SpawnSword()
    {
        while (isMoveState == true)
        {
            Vector2 spawnPosition;
            do
            {
                float randomAngle = Random.Range(0f, 360f);
                // Tinh vi tri sinh ra
                spawnPosition = (Vector2)SM.player.transform.position + new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad)) * SM.spawnRadius;
            } while (spawnPosition == SM.previousSpawnPosition); // Dam bao khong trung vi tri

            SM.previousSpawnPosition = spawnPosition;

            GameObject spawnedSword = GameObject.Instantiate(SM.swordPrefab, spawnPosition, Quaternion.identity);

            // tinh huong cua thanh kiem 
            Vector2 direction = SM.player.transform.position - spawnedSword.transform.position;
            spawnedSword.transform.right = direction;
            SoundFxManager.instance.PlaySoundFXClip(SM.swordSpawnSound, SM.transform, 1f);

            yield return new WaitForSeconds(2f);
        }
    }


    public override void Exit()
    {
        base .Exit();
        SM.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        isMoveState = false;
    }

    IEnumerator EndState()
    {
        yield return new WaitForSeconds(20f);
        SM.NextState();
    }

}
