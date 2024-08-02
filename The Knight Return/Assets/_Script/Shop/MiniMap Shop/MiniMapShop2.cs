using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiniMapShop2 : MonoBehaviour
{
    public GameObject miniMapShop2;
    public GameObject yesButton;  

    public MiniMapManager miniMapManager;
    public PlayerGold playerGold;

    private int gold;

    private bool isMiniMapBought = false; 

    void Start()
    {
        miniMapShop2.SetActive(false);
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
                miniMapShop2.SetActive(false);
                playerGold.GoldMinus(50);
                miniMapManager.UnLockMiniMap3();
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
        miniMapShop2.SetActive(false);
    }

    // Ph??ng th?c ki?m tra minimap ?ã ???c mua ch?a
    public bool IsMiniMapBought()
    {
        return isMiniMapBought;
    }
}
