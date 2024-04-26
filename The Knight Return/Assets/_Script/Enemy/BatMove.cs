using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMove : EnemyMovementBase
{
    // Các bi?n ?? xác ??nh vi?c nh?y
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

        if (isChasing && !isJumping)
        {
            StartCoroutine(JumpAndAttack());
        }
    }

    protected override void EnemyChasing()
    {
        if (Vector2.Distance(transform.position, playerTransform.position) > chaseDistance)
        {
            isChasing = false;
        }
        else
        {
            if (transform.position.x > playerTransform.position.x)
            {
                transform.localScale = new Vector3(-enemyScaleX, enemyScaleY, enemyScaleZ);
                transform.position += Vector3.left * moveSpeed * 2f * Time.deltaTime;
            }
            if (transform.position.x < playerTransform.position.x)
            {
                transform.localScale = new Vector3(enemyScaleX, enemyScaleY, enemyScaleZ);
                transform.position += Vector3.right * moveSpeed * 2f * Time.deltaTime;
            }
        }
    }

    IEnumerator JumpAndAttack()
    {
        isJumping = true;

        GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        // thoi gian dap xuong
        yield return new WaitForSeconds(jumpCooldown);

        // ?áp xu?ng
        GetComponent<Rigidbody2D>().AddForce(Vector2.down * jumpForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1f);

        isJumping = false;
    }
}
