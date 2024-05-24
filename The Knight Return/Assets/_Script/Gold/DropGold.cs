using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGold : MonoBehaviour
{
    public PlayerGold playerGold;
    public int goldDrop;

    public float moveSpeed = 10f;
    private Transform playerTransform;

    private bool playerFarEnough = false; 
    private float distanceThreshold = 10f; 

    private Vector2 initialPosition;
    private PlayerLife playerLife;


    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerGold = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerGold>();
        playerLife = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLife>();
        goldDrop = playerGold.goldTotal;

        // vi tri roi xuong
        initialPosition = transform.position;
        StartCoroutine(MoveUp());

        if (playerGold.goldTotal == 0)
        {
            Destroy(gameObject);
        }
    }

    protected void Update()
    {
        // Ki?m tra n?u ng??i ch?i ?ã cách ?? xa
        if (Vector3.Distance(transform.position, playerTransform.position) > distanceThreshold)
        {
            playerFarEnough = true;
        }

        if(playerLife.dieTime >= 2)
        {
            Destroy(gameObject);
            playerLife.ResetDieTime();
        }
    }

    private IEnumerator MoveUp()
    {
        Vector3 moveDirection = new Vector3(Random.Range(-1f, 1f), 1f, 0f).normalized;

        while (true)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, initialPosition) >= 2f)
            {
                break;
            }

            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerFarEnough)
        {
            playerLife.ResetDieTime();
            LootManager.Instance.AddGold(goldDrop);
            Debug.Log("So vang nhan duoc: " + goldDrop);
            Destroy(gameObject);
        }
    }
}
