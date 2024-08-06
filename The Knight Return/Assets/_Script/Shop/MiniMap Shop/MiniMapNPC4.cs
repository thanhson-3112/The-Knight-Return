using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MiniMapNPC4 : MonoBehaviour
{
    public GameObject miniMapShop4;

    public TMP_Text skillText;
    private bool isPlayerInside = false;
    private bool isShopOpen = false;

    [Header("Sound")]
    public AudioClip shopBell;

    private MiniMapShop4 miniMapShop4Script;

    void Start()
    {
        miniMapShop4 = DontDestroy.instance.miniMapShop4;
        miniMapShop4.SetActive(false);
        miniMapShop4Script = miniMapShop4.GetComponent<MiniMapShop4>();
    }

    private void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.UpArrow) && !isShopOpen)
        {
            if (!miniMapShop4Script.IsMiniMapBought()) // Kiem tra da mua hang
            {
                Debug.Log("Da an uparrow");
                miniMapShop4.SetActive(true);
                SoundFxManager.instance.PlaySoundFXClip(shopBell, transform, 1);
                isShopOpen = true;
            }
        }

        if (isPlayerInside && Input.GetKeyDown(KeyCode.Escape))
        {
            miniMapShop4.SetActive(false);
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
            miniMapShop4.SetActive(false);
            isShopOpen = false;
        }
    }
}
