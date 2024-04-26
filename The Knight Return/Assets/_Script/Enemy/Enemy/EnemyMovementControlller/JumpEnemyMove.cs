using System.Collections;
using System.Collections.Generic;
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
                anim.SetBool("SlimeChasing", false);
                transform.localScale = new Vector3(-enemyScaleX, enemyScaleY, enemyScaleZ);
                transform.position += Vector3.left * moveSpeed  * Time.deltaTime;
                if (isChasing && !isJumping)
                {
                    StartCoroutine(JumpAndAttack());
                }
            }
            if (transform.position.x < playerTransform.position.x)
            {
                anim.SetBool("SlimeChasing", false);
                transform.localScale = new Vector3(enemyScaleX, enemyScaleY, enemyScaleZ);
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
                if (isChasing && !isJumping)
                {
                    StartCoroutine(JumpAndAttack());
                }
            }
        }
    }

    IEnumerator JumpAndAttack()
    {
        isJumping = true;

        anim.SetTrigger("SlimeJump");
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        // thoi gian dap xuong
        yield return new WaitForSeconds(jumpCooldown);

        anim.SetTrigger("SlimeFall");
        GetComponent<Rigidbody2D>().AddForce(Vector2.down * jumpForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1f);

        isJumping = false;
    }
}
