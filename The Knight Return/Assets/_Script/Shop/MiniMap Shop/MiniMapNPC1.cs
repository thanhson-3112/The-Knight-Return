using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniMapNPC1 : MonoBehaviour
{
    public GameObject miniMapShop1;

    public TMP_Text skillText;
    private bool isPlayerInside = false;
    private bool isShopOpen = false;

    [Header("Sound")]
    public AudioClip shopBell;

    private MiniMapShop1 miniMapShop1Script;

    void Start()
    {
        miniMapShop1.SetActive(false);
        miniMapShop1Script = miniMapShop1.GetComponent<MiniMapShop1>();
    }

    private void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.UpArrow) && !isShopOpen)
        {
            if (!miniMapShop1Script.IsMiniMapBought()) // Kiem tra da mua hang
            {
                Debug.Log("Da an uparrow");
                miniMapShop1.SetActive(true);
                SoundFxManager.instance.PlaySoundFXClip(shopBell, transform, 1);
                isShopOpen = true;
            }
        }

        if (isPlayerInside && Input.GetKeyDown(KeyCode.Escape))
        {
            miniMapShop1.SetActive(false);
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
            miniMapShop1.SetActive(false);
            isShopOpen = false;
        }
    }
}
