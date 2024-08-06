using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnlockDoubleJumpSkill : MonoBehaviour
{
    public GameObject skillGuide;
    public TMP_Text skillText;

    private bool isPlayerInside = false;
    private bool isSkillGuideActive = false;

    public PlayerMovement playerSkill;
    public PlayerLife playerPray;
    public PlayerMovement playerMovement;

    public void Start()
    {
        skillGuide = DontDestroy.instance.doubleJumpGuide;
        skillText = DontDestroy.instance.skillText;
        playerPray = DontDestroy.instance.playerPray;
        playerMovement = DontDestroy.instance.playerMovement;
        playerSkill = DontDestroy.instance.playerSkill;
        skillText.gameObject.SetActive(false);
        skillGuide.SetActive(false);
    }

    private void Update()
    {
        skillGuide = DontDestroy.instance.doubleJumpGuide;
        skillText = DontDestroy.instance.skillText;
        playerPray = DontDestroy.instance.playerPray;
        playerMovement = DontDestroy.instance.playerMovement;
        playerSkill = DontDestroy.instance.playerSkill;

        if (isPlayerInside && Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Da an F");
            StartCoroutine(LockPlayerMove());
            playerPray.PlayerPray();
            playerSkill.UnlockDoubleJump();
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
