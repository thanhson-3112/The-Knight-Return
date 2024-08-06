using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopNPC : MonoBehaviour
{
    [SerializeField] ShopManager shopManager;

    public TMP_Text text;
    private bool isPlayerInside = false;
    private bool isShopOpen = false;

    public Transform spawnPos;
    public GameObject[] shopItem;

    [Header("Sound")]
    public AudioClip shopBell;

    private void Start()
    {
        text = DontDestroy.instance.skillText;
        text.gameObject.SetActive(false);
        shopManager = GameObject.FindGameObjectWithTag("ShopManager").GetComponent<ShopManager>();
    }

    private void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.UpArrow) && !isShopOpen)
        {
            Debug.Log("Da an uparrow");
            shopManager.SetCurrentShopNPC(this); // vi tri NPC hien tai
            shopManager.ShopOpen();
            SoundFxManager.instance.PlaySoundFXClip(shopBell, transform, 1);
            isShopOpen = true;
        }

        if (isPlayerInside && Input.GetKeyDown(KeyCode.Escape))
        {
            shopManager.ShopClose();
            isShopOpen = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = true;
            text.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = false;
            text.gameObject.SetActive(false);
            shopManager.ShopClose();
            isShopOpen = false;
        }
    }

    public void BodyArmor()
    {
        Instantiate(shopItem[0], spawnPos.position, Quaternion.identity);
    }

    public void Pants()
    {
        Instantiate(shopItem[1], spawnPos.position, Quaternion.identity);
    }

    public void Boots()
    {
        Instantiate(shopItem[2], spawnPos.position, Quaternion.identity);
    }

    public void Belt()
    {
        Instantiate(shopItem[3], spawnPos.position, Quaternion.identity);
    }

    public void Necklace()
    {
        Instantiate(shopItem[4], spawnPos.position, Quaternion.identity);
    }

    public void Ring()
    {
        Instantiate(shopItem[5], spawnPos.position, Quaternion.identity);
    }
}