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
    public float soulAmount;
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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            LootManager.Instance.AddSoul(soulAmount);
            Debug.Log("Nhan dc soul" + soulAmount);
            Destroy(gameObject);
        }
    }
}