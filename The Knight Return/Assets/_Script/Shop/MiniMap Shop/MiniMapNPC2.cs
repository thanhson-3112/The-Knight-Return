using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniMapNPC2 : MonoBehaviour
{
    public GameObject miniMapShop2;

    public TMP_Text skillText;
    private bool isPlayerInside = false;
    private bool isShopOpen = false;

    [Header("Sound")]
    public AudioClip shopBell;

    private MiniMapShop2 miniMapShop2Script;

    void Start()
    {
        miniMapShop2.SetActive(false);
        miniMapShop2Script = miniMapShop2.GetComponent<MiniMapShop2>();
    }

    private void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.UpArrow) && !isShopOpen)
        {
            if (!miniMapShop2Script.IsMiniMapBought()) // Kiem tra da mua hang
            {
                Debug.Log("Da an uparrow");
                miniMapShop2.SetActive(true);
                SoundFxManager.instance.PlaySoundFXClip(shopBell, transform, 1);
                isShopOpen = true;
            }
        }

        if (isPlayerInside && Input.GetKeyDown(KeyCode.Escape))
        {
            miniMapShop2.SetActive(false);
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
            miniMapShop2.SetActive(false);
            isShopOpen = false;
        }
    }
}
