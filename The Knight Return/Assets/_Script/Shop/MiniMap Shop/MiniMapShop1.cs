using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiniMapShop1 : MonoBehaviour
{
    public GameObject miniMapShop1;
    public GameObject yesButton;  

    public MiniMapManager miniMapManager;
    public PlayerGold playerGold;
    private int gold;

    private bool isMiniMapBought = false; 

    void Start()
    {
        miniMapShop1.SetActive(false);
        playerGold = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerGold>();
        
    }

    public void Update()
    {
        gold = playerGold.goldTotal;
    }


    public void BuyMiniMap()
    {
        if(gold >= 50)
        {
            if (!isMiniMapBought)
            {
                EventSystem.current.SetSelectedGameObject(yesButton);
                miniMapShop1.SetActive(false);
                playerGold.GoldMinus(50);
                miniMapManager.UnLockMiniMap2();
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
        miniMapShop1.SetActive(false);
    }

    // Ph??ng th?c ki?m tra minimap ?ã ???c mua ch?a
    public bool IsMiniMapBought()
    {
        return isMiniMapBought;
    }
}
