using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulManager : MonoBehaviour
{
    //Soul
    [SerializeField] public int maxSoul = 6;
    [SerializeField] public int currentSoul;

    /*public int MaxSoul { get { return maxSoul; } set { maxSoul = value; } }
    public int CurrentSoul { get { return currentSoul; } set { currentSoul = value; } }*/

    public SoulUI soulUI;

    public void Start()
    {
        currentSoul = maxSoul;
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


    private void HandleSoul(int newSoul)
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
}