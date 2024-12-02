using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealingSkill : MonoBehaviour
{
    public GameObject skillGuide;
    public GameObject skillText;

    private bool isPlayerInside = false;
    private bool isSkillGuideActive = false;

    public PlayerLife playerPray;
    public PlayerMovement playerMovement;

    public void Start()
    {
        playerPray = DontDestroy.instance.playerPray;
        playerMovement = DontDestroy.instance.playerMovement;
        skillText = DontDestroy.instance.skillText;
        skillText.gameObject.SetActive(false);
        skillGuide.SetActive(false);
    }

    private void Update()
    {
        playerPray = DontDestroy.instance.playerPray;
        playerMovement = DontDestroy.instance.playerMovement;

        if (isPlayerInside && Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Da an F");
            StartCoroutine(LockPlayerMove());
            playerPray.PlayerPray();
            skillGuide.SetActive(true);
            isSkillGuideActive = true;
        }

        if (isSkillGuideActive && Input.anyKeyDown)
        {
            if (!Input.GetKey(KeyCode.UpArrow))
            {
                skillGuide.SetActive(false);
                isSkillGuideActive = false;
            }
        }
    }

    IEnumerator LockPlayerMove()
    {
        playerMovement.enabled = false;
        yield return new WaitForSeconds(1.5f);
        playerMovement.enabled = true;

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
