using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnlockFireBallSkill : MonoBehaviour
{
    public GameObject skillGuide;
    public GameObject skillText;

    private bool isPlayerInside = false;
    private bool isSkillGuideActive = false;

    public PlayerShooting playerFireBall;
    public PlayerLife playerPray;
    public PlayerMovement playerMovement;

    [Header("Sound")]
    public AudioClip dragonRoar;

    public void Start()
    {
        skillText = DontDestroy.instance.skillText;
        playerFireBall = DontDestroy.instance.playerFireBall;
        playerPray = DontDestroy.instance.playerPray;
        playerMovement = DontDestroy.instance.playerMovement;
        skillText.gameObject.SetActive(false);
        skillGuide.SetActive(false);

    }

    private void Update()
    {
        skillText = DontDestroy.instance.skillText;
        playerFireBall = DontDestroy.instance.playerFireBall;
        playerPray = DontDestroy.instance.playerPray;
        playerMovement = DontDestroy.instance.playerMovement;

        if (isPlayerInside && Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Da an F");
            StartCoroutine(LockPlayerMove());
            playerPray.PlayerPray();
            playerFireBall.UnlockFireBall();
            skillGuide.SetActive(true);
            isSkillGuideActive = true;
            SoundFxManager.instance.PlaySoundFXClip(dragonRoar, transform, 1);
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
