using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntoBossRoomTusk : MonoBehaviour
{
    public GameObject bossPrefab;
    public List<BossDoor> doors;
    public Transform bossSpawnPoint;
    public GameObject boss;

    private bool canTrigger = true;

    public string bossName;
    public BossNameText bossNameText;

    public AudioManager audioManager;
    private bool mapAudioRun = false;

    public bool isBossDefeatedTusk;

    public void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        bossNameText = DontDestroy.instance.bossName.GetComponent<BossNameText>();
    }

    public void Update()
    {
        if (boss == null)
        {
            foreach (BossDoor door in doors)
            {
                door.OpenDoor();
            }
            if (mapAudioRun == false)
            {
                audioManager.PlayAudio(audioManager.map3Audio);
                mapAudioRun = true;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && canTrigger && BossSave.instance.bossSaveTusk == false)
        {
            foreach (BossDoor door in doors)
            {
                door.CloseDoor();
            }

            if (bossNameText != null)
            {
                bossNameText.SetText(bossName);
                bossNameText.Show();

                StartCoroutine(HideBossNameAfterDelay(5f));
            }

            //Sound
            audioManager.PlayAudio(audioManager.bossTusk);
            mapAudioRun = false;

            GameObject spawnedBoss = Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
            boss = spawnedBoss;
            canTrigger = false;

            isBossDefeatedTusk = true;
            BossSave.instance.UpdateBossTusk(true);
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

    // Player roi phong boss
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (boss != null)
            {
                Destroy(boss);
                bossNameText.Hide();
                canTrigger = true;
                audioManager.PlayAudio(audioManager.map3Audio);
                isBossDefeatedTusk = false;
                BossSave.instance.UpdateBossTusk(false);
            }
        }
    }
}
