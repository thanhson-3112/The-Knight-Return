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

    public string bossName; 
    public BossNameText bossNameText;

    public AudioManager audioManager;
    private bool mapAudioRun = false;

    public void Start()
    {
        boss = GameObject.FindGameObjectWithTag("MiniBoss");
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
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

            if (mapAudioRun == false)
            {
                audioManager.PlayAudio(audioManager.map2Audio);
                mapAudioRun = true;
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

            //Sound
            audioManager.PlayAudio(audioManager.bossBBA);
            mapAudioRun = false;

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
                audioManager.PlayAudio(audioManager.map2Audio);
            }
        }
    }
}
