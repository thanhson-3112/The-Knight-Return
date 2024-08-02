using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulSpawner : MonoBehaviour
{
    [SerializeField] private GameObject droppedItemPrefab;
    [SerializeField] private List<SoulSO> lootList = new List<SoulSO>();


    protected virtual void Update()
    {
    }

    public SoulSO GetDroppedItem()
    {
        // random ti le roi ra 1% - 100%
        int randomNumber = Random.Range(1, 101);
        List<SoulSO> PossibleItems = new List<SoulSO>();
        foreach(SoulSO item in lootList)
        {
            if (randomNumber <= item.soulChance)
            {
                PossibleItems.Add(item); 
            }
        }
        if (PossibleItems.Count > 0)
            {
                SoulSO droppedItem = PossibleItems[Random.Range(0, PossibleItems.Count)];
                return droppedItem;
            }
        Debug.Log("No loot dropped");
        return null;
    }

    // Roi ra tu quai
    public void InstantiateLoot(Vector3 spawnPosition)
    {
        SoulSO droppedItem = GetDroppedItem();
        if(droppedItem != null)
        {
            GameObject lootGameObject = Instantiate(droppedItemPrefab, spawnPosition, Quaternion.identity);
            //truyen hinh anh cho item
            lootGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.soulSprite;

            float dropForce = 10f;
            Vector2 dropDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            lootGameObject.GetComponent<Rigidbody2D>().AddForce(dropDir * dropForce, ForceMode2D.Impulse);
        }
    } 
}
