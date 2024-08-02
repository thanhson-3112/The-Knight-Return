using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyRandomEnemy : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Vector2 moveDirection = new Vector2(1f, 0.25f);
    [SerializeField] GameObject rightCheck, topCheck, groundCheck;
    [SerializeField] Vector2 rightCheckSize, topCheckSize, groundCheckSize;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] bool goingUp = true;

    private bool touchedGround, touchedTop, touchedRight;
    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HitLogic();
    }

    void FixedUpdate()
    {
        rb.velocity = moveDirection * moveSpeed;
    }

    void HitLogic()
    {
        touchedRight = HitDetector(rightCheck, rightCheckSize, groundLayer);
        touchedTop = HitDetector(topCheck, topCheckSize, groundLayer);
        touchedGround = HitDetector(groundCheck, groundCheckSize, groundLayer);

        if (touchedRight)
        {
            Flip();
        }
        if (touchedTop && goingUp)
        {
            ChangeYDirection();
        }
        if (touchedGround && !goingUp)
        {
            ChangeYDirection();
        }
    }

    bool HitDetector(GameObject gameObject, Vector2 size, LayerMask layer)
    {
        return Physics2D.OverlapBox(gameObject.transform.position, size, 0f, layer);
    }

    void ChangeYDirection()
    {
        moveDirection.y = -moveDirection.y;
        goingUp = !goingUp;
    }

    void Flip()
    {
        transform.Rotate(new Vector2(0, 180));
        moveDirection.x = -moveDirection.x;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(groundCheck.transform.position, groundCheckSize);
        Gizmos.DrawWireCube(topCheck.transform.position, topCheckSize);
        Gizmos.DrawWireCube(rightCheck.transform.position, rightCheckSize);
    }
}
