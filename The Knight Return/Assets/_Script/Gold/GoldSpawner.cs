using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSpawner : MonoBehaviour
{
    [SerializeField] private GameObject droppedItemPrefab;
    [SerializeField] private List<GoldSO> lootList = new List<GoldSO>();

    protected virtual void Update()
    {
    }

    public GoldSO GetDroppedItem()
    {
        // random ti le roi ra 1% - 100%
        int randomNumber = Random.Range(1, 101);
        List<GoldSO> PossibleItems = new List<GoldSO>();
        foreach (GoldSO item in lootList)
        {
            if (randomNumber <= item.goldChance)
            {
                PossibleItems.Add(item);
            }
        }
        if (PossibleItems.Count > 0)
        {
            GoldSO droppedItem = PossibleItems[Random.Range(0, PossibleItems.Count)];
            return droppedItem;
        }
        Debug.Log("No loot dropped");
        return null;
    }

    // Roi ra tu quai
    public void InstantiateLoot(Vector3 spawnPosition)
    {
        GoldSO droppedItem = GetDroppedItem();
        if (droppedItem != null)
        {
            GameObject lootGameObject = Instantiate(droppedItemPrefab, spawnPosition, Quaternion.identity);
            //truyen hinh anh cho item
            lootGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.goldSprite;

            float dropForce = 10f;
            Vector2 dropDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            lootGameObject.GetComponent<Rigidbody2D>().AddForce(dropDir * dropForce, ForceMode2D.Impulse);
        }
    }
}
