using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerGold : MonoBehaviour
{
    /*public static PlayerGold instance;*/

    [SerializeField] public int goldTotal;
    public TextMeshProUGUI goldText;

    private void Awake()
    {
        /*if (PlayerGold.instance != null) Debug.LogError("Only 1 ScoreManager allow");
        PlayerGold.instance = this;*/
    }

    protected virtual void Start()
    {
        goldText.text = "Gold: " + goldTotal.ToString();
    }

    protected virtual void OnEnable()
    {
        LootManager.Instance.OnGoldChange += HandleGold;
    }

    protected virtual void OnDisable()
    {
        LootManager.Instance.OnGoldChange -= HandleGold;
    }

    protected virtual void HandleGold(int newGold)
    {
        goldTotal += newGold;
        goldText.text = "Gold: " + goldTotal.ToString();
    }

    // save game
   /* public virtual void FromJson(string jsonString)
    {
        GoldData obj = JsonUtility.FromJson<GoldData>(jsonString);
        if (obj == null) return;
        this.goldTotal = obj.goldTotal;
    }*/
}

/*public class GoldData
{
    public int goldTotal;
}*/