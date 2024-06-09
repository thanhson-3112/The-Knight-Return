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

    public string bossName; // Name of the boss
    public BossNameText bossNameText; // Reference to the BossNameText script
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

            if (bossNameText != null)
            {
                // Set the boss name and show it
                bossNameText.SetText(bossName);
                bossNameText.Show();

                // Start the coroutine to hide the boss name after 5 seconds
                StartCoroutine(HideBossNameAfterDelay(5f));
            }

            // Spawn boss ? v? trí bossSpawnPoint
            GameObject spawnedBoss1 = Instantiate(bossPrefab1, bossSpawnPoint1.position, Quaternion.identity);
            GameObject spawnedBoss2 = Instantiate(bossPrefab2, bossSpawnPoint2.position, Quaternion.identity);
            boss = new GameObject[] { spawnedBoss1, spawnedBoss2 };
            canTrigger = false;
        }
    }

    // Coroutine to hide the boss name after a delay
    private IEnumerator HideBossNameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (bossNameText != null)
        {
            bossNameText.Hide();
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
                bossNameText.Hide();
                canTrigger = true;
            }
        }
    }
}
