using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransMoving : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;
    [SerializeField] private float speed = 2f;
    private bool isPlayerTouching = false;
    private bool isMoving = false;
    private GameObject player;
    private Rigidbody2D playerRb;

    public DarkScene darkScene;

    private void Update()
    {
        if (isPlayerTouching && Input.GetKeyDown(KeyCode.UpArrow))
        {
            isMoving = true;
            StartCoroutine(darkScene.ActivateDarkScene());
        }

        if (isMoving)
        {
            if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < 0.1f)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex >= waypoints.Length)
                {
                    currentWaypointIndex = 0;
                    isMoving = false;
                }

                // N?u ??n waypoint 2, tách nhân v?t kh?i viên ?á
                if (currentWaypointIndex == 2)
                {
                    DetachPlayer();
                    StartCoroutine(darkScene.DeactivateDarkScene());
                }
            }

            Vector3 targetPosition = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
            Vector3 deltaPosition = targetPosition - transform.position;
            transform.position = targetPosition;

            if (player != null)
            {
                player.transform.position += deltaPosition;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerTouching = true;
            player = collision.gameObject;
            playerRb = player.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.gravityScale = 0; // Vô hi?u hóa tr?ng l?c c?a nhân v?t
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerTouching = false;
            if (playerRb != null)
            {
                playerRb.gravityScale = 4; 
                playerRb = null;
            }
            player = null;
        }
    }

    // Ph??ng th?c tách nhân v?t kh?i viên ?á
    private void DetachPlayer()
    {
        if (playerRb != null)
        {
            playerRb.gravityScale = 4; // B?t l?i tr?ng l?c c?a nhân v?t
            playerRb = null;
        }
        player = null;
    }
}
