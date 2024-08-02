using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulManager : MonoBehaviour
{
    public static SoulManager instance;

    //Soul
    [SerializeField] public float maxSoul = 6;
    [SerializeField] public float currentSoul;

    public SoulUI soulUI;

    private void Awake()
    {
        if (SoulManager.instance != null) Debug.LogError("Only 1 ScoreManager allow");
        SoulManager.instance = this;
    }

    public void Start()
    {
    }

    public void Update()
    {
        soulUI.SetMaxSoul(maxSoul);
        soulUI.SetSoul(currentSoul);
    }

    private void OnEnable()
    {
        LootManager.Instance.OnSoulChange += HandleSoul;
    }

    private void OnDisable()
    {
        LootManager.Instance.OnSoulChange -= HandleSoul;
    }


    private void HandleSoul(float newSoul)
    {
        if(currentSoul < maxSoul)
        {
            currentSoul += newSoul;
            Debug.Log("soul hien tai " + currentSoul);
        }
    }

    public void MinusCurrentSoul()
    {
        currentSoul -= 2;
    }

    public void AddCurrentSoul()
    {
        currentSoul = maxSoul;
    }

    // save game
    public virtual void FromJson(string jsonString1)
    {
        GameData obj = JsonUtility.FromJson<GameData>(jsonString1);
        if (obj == null) return;
        this.currentSoul = obj.currentSoul;
        this.maxSoul = obj.maxSoul;
    }
}
