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
            // T�nh m?t g�c ng?u nhi�n
            float randomAngle = Random.Range(0f, 360f);
            // T�nh m?t kho?ng c�ch ng?u nhi�n trong b�n k�nh di chuy?n
            float randomDistance = Random.Range(0f, SM.moveRadius);

            // T�nh v? tr� ?�ch xung quanh ng??i ch?i
            Vector2 targetPosition = (Vector2)SM.player.transform.position + new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad)) * randomDistance;

            // Di chuy?n t?i v? tr� ?�ch
            while ((Vector2)SM.transform.position != targetPosition)
            {
                SM.transform.position = Vector2.MoveTowards(SM.transform.position, targetPosition, SM.moveSpeed * Time.deltaTime);
                yield return null;
            }

            // Ch? m?t kho?ng th?i gian ng?u nhi�n tr??c khi di chuy?n ti?p
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
                // T?o m?t g�c ng?u nhi�n gi?a 0 v� 360 ??
                float randomAngle = Random.Range(0f, 360f);
                // T�nh v? tr� sinh ra s? d?ng g�c v� b�n k�nh sinh ra
                spawnPosition = (Vector2)SM.player.transform.position + new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad)) * SM.spawnRadius;
            } while (spawnPosition == SM.previousSpawnPosition); // ??m b?o v? tr� m?i kh�ng gi?ng v?i v? tr� tr??c ?�

            SM.previousSpawnPosition = spawnPosition;

            // T?o ra thanh ki?m t?i v? tr� t�nh to�n
            GameObject spawnedSword = GameObject.Instantiate(SM.swordPrefab, spawnPosition, Quaternion.identity);

            // T�nh h??ng t? thanh ki?m ??n ng??i ch?i
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
