using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntoBossRoomFP : MonoBehaviour
{
    public GameObject bossPrefab1;
    public GameObject bossPrefab2;
    public List<BossDoor> doors;
    public Transform bossSpawnPoint1;
    public Transform bossSpawnPoint2;
    public GameObject[] boss;

    private bool canTrigger = true;

    public void Start()
    {
        boss = GameObject.FindGameObjectsWithTag("Boss");
    }

    public void Update()
    {
        if (AreAllBossesDead())
        {
            foreach (BossDoor door in doors)
            {
                door.OpenDoor();
            }
        }
    }

    private bool AreAllBossesDead()
    {
        foreach (GameObject bossInstance in boss)
        {
            if (bossInstance != null)
            {
                return false;
            }
        }
        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && canTrigger)
        {
            foreach (BossDoor door in doors)
            {
                door.CloseDoor();
            }

            // Spawn boss ? v? tr� bossSpawnPoint
            GameObject spawnedBoss1 = Instantiate(bossPrefab1, bossSpawnPoint1.position, Quaternion.identity);
            GameObject spawnedBoss2 = Instantiate(bossPrefab2, bossSpawnPoint2.position, Quaternion.identity);
            boss = new GameObject[] { spawnedBoss1, spawnedBoss2 };
            canTrigger = false;
        }
    }

    // Nguoi choi chet
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!AreAllBossesDead())
            {
                foreach (GameObject bossInstance in boss)
                {
                    Destroy(bossInstance);
                }
                canTrigger = true;
            }
        }
    }
}
