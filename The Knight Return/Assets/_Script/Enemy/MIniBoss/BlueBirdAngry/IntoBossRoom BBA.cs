using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntoBossRoomBBA: MonoBehaviour
{
    public CameraManager cam;
    public GameObject bossPrefab;
    public List<BossDoor> doors;
    public Transform bossSpawnPoint;

    public GameObject boss;

    private bool canTrigger = true;

    public string bossName; // Name of the boss
    public BossNameText bossNameText; // Reference to the BossNameText script

    public void Start()
    {
        boss = GameObject.FindGameObjectWithTag("MiniBoss");
    }

    public void Update()
    {
        if (boss == null)
        {
            cam.BossDie();
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
            cam.CamBossRoom();
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
            GameObject spawnedBoss = Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
            boss = spawnedBoss;
            canTrigger = false;
        }
    }

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
            cam.BossDie();
            if (boss != null)
            {
                Destroy(boss);
                bossNameText.Hide();
                canTrigger = true;
            }
        }
    }
}
