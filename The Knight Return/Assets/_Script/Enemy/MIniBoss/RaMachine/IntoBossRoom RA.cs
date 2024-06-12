using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntoBossRoomRA : MonoBehaviour
{
    public GameObject bossPrefab1;
    public GameObject bossPrefab2;
    public List<BossDoor> doors;
    public Transform bossSpawnPoint1;
    public Transform bossSpawnPoint2;
    public GameObject[] boss;
    public CameraManager cam;

    private bool canTrigger = true;

    public string bossName; 
    public BossNameText bossNameText;

    public AudioManager audioManager;
    private bool mapAudioRun = false;

    public void Start()
    {
        boss = GameObject.FindGameObjectsWithTag("Boss");
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    public void Update()
    {
        if (AreAllBossesDead())
        {
            cam.BossDie();
            foreach (BossDoor door in doors)
            {
                door.OpenDoor();
            }

            if (mapAudioRun == false)
            {
                audioManager.PlayAudio(audioManager.map4Audio);
                mapAudioRun = true;
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

            //Sound
            audioManager.PlayAudio(audioManager.bossRM);
            mapAudioRun = false;

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
            cam.BossDie();
            if (!AreAllBossesDead())
            {
                foreach (GameObject bossInstance in boss)
                {
                    Destroy(bossInstance);
                }
                bossNameText.Hide();
                canTrigger = true;
                audioManager.PlayAudio(audioManager.map4Audio);

            }
        }
    }
}
