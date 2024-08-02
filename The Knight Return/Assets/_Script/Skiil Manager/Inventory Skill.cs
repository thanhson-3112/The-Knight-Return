using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventorySkill : MonoBehaviour
{
    public GameObject skillGuide;
    public TMP_Text skillText;

    private bool isPlayerInside = false;
    private bool isSkillGuideActive = false;

    public PlayerLife playerPray;
    public PlayerMovement playerMovement;

    public void Start()
    {
        skillText.gameObject.SetActive(false);
        playerPray = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLife>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        skillGuide.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Da an F");
            StartCoroutine(LockPlayerMove());
            playerPray.PlayerPray();
            skillGuide.SetActive(true);
            isSkillGuideActive = true;
        }

        // Check for any key press to hide the skill guide
        if (isSkillGuideActive && Input.anyKeyDown)
        {
            // Ignore the Up Arrow key press to avoid immediate hiding
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
