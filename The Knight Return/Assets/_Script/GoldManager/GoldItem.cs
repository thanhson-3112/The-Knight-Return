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
    public AudioClip goldCollect;

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
        // chon huong bay ngau nhien
        Vector3 moveDirection = new Vector3(Random.Range(-1f, 1f), 1f, 0f).normalized;

        while (true)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, initialPosition) >= 1.5f)
            {
                break;
            }
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            LootManager.Instance.AddGold(goldAmount);
            SoundFxManager.instance.PlaySoundFXClip(goldCollect, transform, 1f);
            Debug.Log("So vang nhan duoc: " + goldAmount);
            Destroy(gameObject);
        }
    }
}
