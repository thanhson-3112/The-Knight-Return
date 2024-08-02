using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    Shop[] Shops;

    private ShopNPC currentShopNPC;

    private int shopCount = 6;

    [SerializeField] GameObject shop_prefab;
    [SerializeField] GameObject shopHorizontalLayout;

    [Header("")]
    [SerializeField] PlayerGold playerGold;
    private float goldTotal;

    private PlayerAttack playerAttack;

    private void Start()
    {
        playerGold = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerGold>();
        playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();

        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
    }

    private void Update()
    {
        goldTotal = playerGold.goldTotal;
    }

    public void SetCurrentShopNPC(ShopNPC npc)
    {
        currentShopNPC = npc;
    }

    public void ShopClose()
    {
        playerAttack.enabled = true;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
    }

    public void ShopOpen()
    {
        Shops = new Shop[]
        {
            new Shop{Name = "BodyArmor", Description = "MaxHealth +1", Ratity = "Common", Sprite = Resources.Load<Sprite>("Shop_Img/BodyArmor"), RequiredGold = 100},
            new Shop{Name = "Pants", Description = "MaxHealth +1", Ratity = "Common", Sprite = Resources.Load<Sprite>("Shop_Img/Pants"), RequiredGold = 200},
            new Shop{Name = "Boots", Description = "MaxHealth +1", Ratity = "Rare", Sprite = Resources.Load<Sprite>("Shop_Img/Boots"), RequiredGold = 300},
            new Shop{Name = "Belt", Description = "MaxHealth +1", Ratity = "Rare", Sprite = Resources.Load<Sprite>("Shop_Img/Belt"), RequiredGold = 400},
            new Shop{Name = "Necklace", Description = "MaxSoul +1", Ratity = "Epic", Sprite = Resources.Load<Sprite>("Shop_Img/Necklace"), RequiredGold = 500},
            new Shop{Name = "Ring", Description = "MaxSoul +1", Ratity = "Epic", Sprite = Resources.Load<Sprite>("Shop_Img/Ring"), RequiredGold = 600}
        };
        ButtonsSet();
        ShopSave.instance.UpdateShopActiveStates();
    }

    public void UpdateShopActiveStates(bool shop1Active, bool shop2Active, bool shop3Active, bool shop4Active, bool shop5Active, bool shop6Active)
    {
        shopHorizontalLayout.transform.GetChild(0).gameObject.SetActive(shop1Active);
        shopHorizontalLayout.transform.GetChild(1).gameObject.SetActive(shop2Active);
        shopHorizontalLayout.transform.GetChild(2).gameObject.SetActive(shop3Active);
        shopHorizontalLayout.transform.GetChild(3).gameObject.SetActive(shop4Active);
        shopHorizontalLayout.transform.GetChild(4).gameObject.SetActive(shop5Active);
        shopHorizontalLayout.transform.GetChild(5).gameObject.SetActive(shop6Active);
    }

    private void ButtonsSet()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);

        while (shopHorizontalLayout.transform.childCount > shopCount)
        {
            Destroy(shopHorizontalLayout.transform.GetChild(shopHorizontalLayout.transform.childCount - 1).gameObject);
        }

        while (shopHorizontalLayout.transform.childCount < shopCount)
        {
            Instantiate(shop_prefab, shopHorizontalLayout.transform);
        }

        Dictionary<string, Color> rarityColors = new Dictionary<string, Color>
        {
            {"Common", new Color(1, 1, 1, 1)},
            {"Rare", new Color(0.5f, 1, 0.5f, 1)},
            {"Epic", new Color(0.75f, 0.25f, 0.75f, 1)}
        };

        for (int i = 0; i < shopCount; i++)
        {
            Shop shop = Shops[i];

            GameObject shopObject = shopHorizontalLayout.transform.GetChild(i).gameObject;
            Button shopButton = shopObject.GetComponent<Button>();

            shopButton.onClick.RemoveAllListeners();
            shopButton.onClick.AddListener(() => { ShopChosen(shop); });

            TextMeshProUGUI shopTextName = shopObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            shopTextName.text = shop.Name;

            TextMeshProUGUI shopTextDescription = shopObject.transform.GetChild(4).GetComponent<TextMeshProUGUI>();
            shopTextDescription.text = shop.Description;

            TextMeshProUGUI shopTextGold = shopObject.transform.GetChild(5).GetComponent<TextMeshProUGUI>();
            shopTextGold.text = shop.RequiredGold.ToString();

            shopObject.transform.GetChild(1).GetComponent<Image>().color = rarityColors[shop.Ratity];
            shopObject.transform.GetChild(2).GetComponent<Image>().sprite = shop.Sprite;
        }

        playerAttack.enabled = false;
    }

    private void ShopChosen(Shop shop)
    {
        if (goldTotal < shop.RequiredGold)
        {
            Debug.Log("khong du vang.");
            return;
        }

        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        playerAttack.enabled = true;

        playerGold.GoldMinus(shop.RequiredGold);

        if (currentShopNPC != null)
        {
            switch (shop.Name)
            {
                case "BodyArmor":
                    Debug.Log("BodyArmor");
                    currentShopNPC.BodyArmor();
                    break;
                case "Pants":
                    Debug.Log("Pants");
                    currentShopNPC.Pants();
                    break;
                case "Boots":
                    Debug.Log("Boots");
                    currentShopNPC.Boots();
                    break;
                case "Belt":
                    Debug.Log("Belt");
                    currentShopNPC.Belt();
                    break;
                case "Necklace":
                    Debug.Log("Necklace");
                    currentShopNPC.Necklace();
                    break;
                case "Ring":
                    Debug.Log("Ring");
                    currentShopNPC.Ring();
                    break;
            }
        }

        for (int i = 0; i < shopCount; i++)
        {
            if (Shops[i].Name == shop.Name)
            {
                GameObject selectedShopObject = shopHorizontalLayout.transform.GetChild(i).gameObject;
                selectedShopObject.SetActive(false);

                // Update boolean state
                switch (i)
                {
                    case 0: ShopSave.instance.shop1Active = false; break;
                    case 1: ShopSave.instance.shop2Active = false; break;
                    case 2: ShopSave.instance.shop3Active = false; break;
                    case 3: ShopSave.instance.shop4Active = false; break;
                    case 4: ShopSave.instance.shop5Active = false; break;
                    case 5: ShopSave.instance.shop6Active = false; break;
                }

                break;
            }
        }
    }

    public class Shop
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Ratity { get; set; }
        public Sprite Sprite { get; set; }
        public int RequiredGold { get; set; }
    }
}
