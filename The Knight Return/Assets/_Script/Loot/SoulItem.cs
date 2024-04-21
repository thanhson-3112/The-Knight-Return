using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoulItem : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 30f;
    private Transform playerTransform;
    private bool isMoving = false;
    public int soulAmount;
    public float autoMoveDistance = 5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 40f);
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected void Update()
    {
        if (!isMoving && Vector3.Distance(transform.position, playerTransform.position) < autoMoveDistance)
        {
            isMoving = true;
        }

        if (isMoving)
        {
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = playerTransform.position - transform.position;
        direction.Normalize();

        transform.Translate(direction * moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, playerTransform.position) < 0.1f)
        {
            LootManager.Instance.AddSoul(soulAmount);
            Debug.Log("Nhan dc soul" + soulAmount);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ki?m tra n?u va ch?m v?i v?t th? có tag "Ground"
        if (other.CompareTag("Ground"))
        {
            isMoving = false;
            rb.gravityScale = 0f;
        }
    }
}