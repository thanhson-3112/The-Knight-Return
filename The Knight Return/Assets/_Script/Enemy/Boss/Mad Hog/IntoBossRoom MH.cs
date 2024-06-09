using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntoBossRoomMH : MonoBehaviour
{
    public GameObject bossPrefab;
    public List<BossDoor> doors;
    public Transform bossSpawnPoint;
    public GameObject boss;

    private bool canTrigger = true;

    public string bossName; // Name of the boss
    public BossNameText bossNameText; // Reference to the BossNameText script

    public void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
    }

    public void Update()
    {
        if (boss == null)
        {
            foreach (BossDoor door in doors)
            {
                door.OpenDoor();
            }
        }
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

            // Spawn boss at bossSpawnPoint
            GameObject spawnedBoss = Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
            boss = spawnedBoss;
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

    // Player exits the boss room
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (boss != null)
            {
                Destroy(boss);
                bossNameText.Hide();
                canTrigger = true;
            }
        }
    }
}
