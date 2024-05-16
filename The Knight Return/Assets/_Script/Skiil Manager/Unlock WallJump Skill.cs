using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnlockWallJumpSkill : MonoBehaviour
{
    public TMP_Text skillText;

    private bool isPlayerInside = false;

    public PlayerMovement playerSkill;

    public void Start()
    {
        skillText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Da an F");
            playerSkill.UnlockSlideWall();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = true;
            skillText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = false;
            skillText.gameObject.SetActive(false);
        }
    }
}
