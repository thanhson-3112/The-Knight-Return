using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnlockFireBallSkill : MonoBehaviour
{
    public TMP_Text skillText;

    private bool isPlayerInside = false;

    public PlayerShooting playerSkill;

    public void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerSkill = playerObject.GetComponent<PlayerShooting>();
        skillText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Da an F");
            playerSkill.UnlockFireBall();
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