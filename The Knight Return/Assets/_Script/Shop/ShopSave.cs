using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSave : MonoBehaviour
{
    public static ShopSave instance;
    public ShopManager shopManager;

    public bool shop1Active;
    public bool shop2Active;
    public bool shop3Active;
    public bool shop4Active;
    public bool shop5Active;
    public bool shop6Active;

    private void Awake()
    {
        if (ShopSave.instance != null)
        {
            Debug.LogError("Only 1 ShopSave instance allowed");
        }
        ShopSave.instance = this;
    }

    public void UpdateShopActiveStates()
    {
        shopManager.UpdateShopActiveStates(shop1Active, shop2Active, shop3Active, shop4Active, shop5Active, shop6Active);
    }

    public void FromJson(string jsonString)
    {
        GameData obj = JsonUtility.FromJson<GameData>(jsonString);
        if (obj == null) return;

        this.shop1Active = obj.shop1Active;
        this.shop2Active = obj.shop2Active;
        this.shop3Active = obj.shop3Active;
        this.shop4Active = obj.shop4Active;
        this.shop5Active = obj.shop5Active;
        this.shop6Active = obj.shop6Active;
    }
}
