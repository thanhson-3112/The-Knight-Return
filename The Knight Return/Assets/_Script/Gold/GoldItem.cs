using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldItem : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 30f;
    private Transform playerTransform;
    private bool isMoving = false;
    public int goldAmount = 1;
    public float autoMoveDistance = 5f;

    // L?u tr? v? trí ban ??u ?? s? d?ng khi r?i xu?ng
    private Vector2 initialPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 40f);
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // L?u v? trí ban ??u
        initialPosition = transform.position;

        // G?i hàm MoveUp khi b?t ??u
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
            Debug.Log("S? vàng nh?n ???c: " + goldAmount);
            Destroy(gameObject);
        }
    }

    private IEnumerator MoveUp()
    {
        // Bay lên trên m?t kho?ng nh?
        float targetHeight = initialPosition.y + 2f; // Kho?ng 2f
        while (transform.position.y < targetHeight)
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
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
