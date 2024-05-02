using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class JumpEnemyMove : EnemyMovementBase
{
    public float jumpForce = 10f;
    public float jumpCooldown = 1f;
    private bool isJumping = false;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void EnemyChasing()
    {
        if (Vector2.Distance(transform.position, playerTransform.position) > chaseDistance)
        {
            isChasing = false;
        }
        else
        {
            anim.SetBool("SlimeChasing", true);
            if (transform.position.x > playerTransform.position.x)
            {
                transform.localScale = new Vector3(-enemyScaleX, enemyScaleY, enemyScaleZ);
                transform.position += Vector3.left * moveSpeed * 1f  * Time.deltaTime;
                if (isChasing && !isJumping)
                {
                    StartCoroutine(JumpAndAttack());
                }
            }
            if (transform.position.x < playerTransform.position.x)
            {
                transform.localScale = new Vector3(enemyScaleX, enemyScaleY, enemyScaleZ);
                transform.position += Vector3.right * moveSpeed * 1f * Time.deltaTime;
                if (isChasing && !isJumping)
                {
                    StartCoroutine(JumpAndAttack());
                }
            }
        }
    }

    /*IEnumerator JumpAndAttack()
    {
        anim.SetBool("SlimeChasing", false);
        isJumping = true;

        anim.SetBool("SlimeJump", true);
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        // thoi gian dap xuong
        yield return new WaitForSeconds(jumpCooldown);
        anim.SetBool("SlimeJump", false);

        anim.SetBool("SlimeFall", true);
        GetComponent<Rigidbody2D>().AddForce(Vector2.down * jumpForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1f);
        anim.SetBool("SlimeFall", false);
        isJumping = false;
    }*/

    IEnumerator JumpAndAttack()
    {
        // T?m d?ng m?t th?i gian tr??c khi nh?y
        yield return new WaitForSeconds(1f);

        // Tính toán h??ng và kho?ng cách t? quái ??n nhân v?t
        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        // Tính l?c nh?y d?a trên h??ng và kho?ng cách ??n nhân v?t
        Vector2 jumpForceVector = directionToPlayer * jumpForce;

        // Áp d?ng l?c nh?y cho Rigidbody2D c?a quái
        GetComponent<Rigidbody2D>().AddForce(jumpForceVector, ForceMode2D.Impulse);
    }

}
