using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniMapNPC3 : MonoBehaviour
{
    public GameObject miniMapShop3;

    public TMP_Text skillText;
    private bool isPlayerInside = false;
    private bool isShopOpen = false;

    [Header("Sound")]
    public AudioClip shopBell;

    private MiniMapShop3 miniMapShop3Script;

    void Start()
    {
        miniMapShop3.SetActive(false);
        miniMapShop3Script = miniMapShop3.GetComponent<MiniMapShop3>();
    }

    private void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.UpArrow) && !isShopOpen)
        {
            if (!miniMapShop3Script.IsMiniMapBought()) // Kiem tra da mua hang
            {
                Debug.Log("Da an uparrow");
                miniMapShop3.SetActive(true);
                SoundFxManager.instance.PlaySoundFXClip(shopBell, transform, 1);
                isShopOpen = true;
            }
        }

        if (isPlayerInside && Input.GetKeyDown(KeyCode.Escape))
        {
            miniMapShop3.SetActive(false);
            isShopOpen = false;
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
            miniMapShop3.SetActive(false);
            isShopOpen = false;
        }
    }
}
