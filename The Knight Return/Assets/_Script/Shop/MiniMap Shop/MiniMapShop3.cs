using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiniMapShop3 : MonoBehaviour
{
    public GameObject miniMapShop3;
    public GameObject yesButton;  

    public MiniMapManager miniMapManager;
    public PlayerGold playerGold;
    private int gold;

    private bool isMiniMapBought = false; 

    void Start()
    {
        miniMapShop3.SetActive(false);
        playerGold = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerGold>();
    }

    public void Update()
    {
        gold = playerGold.goldTotal;
    }

    public void BuyMiniMap()
    {
        if (gold >= 50)
        {
            if (!isMiniMapBought)
            {
                EventSystem.current.SetSelectedGameObject(yesButton);
                miniMapShop3.SetActive(false);
                playerGold.GoldMinus(50);
                miniMapManager.UnLockMiniMap4();
                isMiniMapBought = true;
            }
        }
        else
        {
            return;
        }
    }

    public void Ouit()
    {
        miniMapShop3.SetActive(false);
    }

    // Ph??ng th?c ki?m tra minimap ?ã ???c mua ch?a
    public bool IsMiniMapBought()
    {
        return isMiniMapBought;
    }
}
