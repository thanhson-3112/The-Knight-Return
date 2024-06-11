using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldItem : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 10f;
    private Transform playerTransform;
    private bool isMoving = false;
    public int goldAmount = 1;
    public float autoMoveDistance = 2f;

    private Vector2 initialPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 40f);
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // vi tri roi xuong
        initialPosition = transform.position;
        StartCoroutine(MoveUp());
    }

    protected void Update()
    {
        if (!isMoving && Vector3.Distance(transform.position, playerTransform.position) < autoMoveDistance)
        {
            isMoving = true;
        }

        if (isMoving)
        {
            GoldMove();
        }
    }

    private void GoldMove()
    {
        Vector3 direction = playerTransform.position - transform.position;
        direction.Normalize();

        rb.velocity = direction * moveSpeed;

        if (Vector3.Distance(transform.position, playerTransform.position) < 0.1f)
        {
            LootManager.Instance.AddGold(goldAmount);
            Debug.Log("So vang nhan duoc: " + goldAmount);
            Destroy(gameObject);
        }
    }

    private IEnumerator MoveUp()
    {
        // Ch?n m?t h??ng di chuy?n ng?u nhiên
        Vector3 moveDirection = new Vector3(Random.Range(-1f, 1f), 1f, 0f).normalized;

        // Bay theo h??ng ng?u nhiên ???c ch?n
        while (true)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

            // Ki?m tra n?u ??i t??ng ?ã di chuy?n ?? xa
            if (Vector3.Distance(transform.position, initialPosition) >= 1.5f)
            {
                break; // Thoát vòng l?p n?u ??i t??ng ?ã di chuy?n ?? xa
            }

            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            LootManager.Instance.AddGold(goldAmount);
            Debug.Log("So vang nhan duoc: " + goldAmount);
            Destroy(gameObject);
        }
    }
}
