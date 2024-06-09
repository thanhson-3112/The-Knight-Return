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
        SM.StartCoroutine(SpawnFireBall());
        
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
            // Tính m?t kho?ng cách ng?u nhiên trong bán kính di chuy?n
            float randomDistance = Random.Range(0f, SM.moveRadius);

            // Tính v? trí ?ích xung quanh ng??i ch?i
            Vector2 targetPosition = (Vector2)SM.player.transform.position + new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad)) * randomDistance;

            // Di chuy?n t?i v? trí ?ích
            while ((Vector2)SM.transform.position != targetPosition)
            {
                SM.transform.position = Vector2.MoveTowards(SM.transform.position, targetPosition, SM.moveSpeed * Time.deltaTime);
                yield return null;
            }

            // Ch? m?t kho?ng th?i gian ng?u nhiên tr??c khi di chuy?n ti?p
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator SpawnFireBall()
    {
        while (isMoveState == true)
        {
            Vector2 spawnPosition;
            do
            {
                // T?o m?t góc ng?u nhiên gi?a 0 và 360 ??
                float randomAngle = Random.Range(0f, 360f);
                // Tính v? trí sinh ra s? d?ng góc và bán kính sinh ra
                spawnPosition = (Vector2)SM.player.transform.position + new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad)) * SM.spawnRadius;
            } while (spawnPosition == SM.previousSpawnPosition); // ??m b?o v? trí m?i không gi?ng v?i v? trí tr??c ?ó

            SM.previousSpawnPosition = spawnPosition;

            // T?o ra thanh ki?m t?i v? trí tính toán
            GameObject spawnedSword = GameObject.Instantiate(SM.swordPrefab, spawnPosition, Quaternion.identity);

            // Tính h??ng t? thanh ki?m ??n ng??i ch?i
            Vector2 direction = SM.player.transform.position - spawnedSword.transform.position;
            spawnedSword.transform.right = direction;

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
