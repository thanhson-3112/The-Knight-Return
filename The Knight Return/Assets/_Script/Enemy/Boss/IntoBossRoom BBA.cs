using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntoBossRoomBBA: MonoBehaviour
{
    public CameraManager cam;
    public GameObject bossPrefab;
    public List<BossDoor> doors;
    public Transform bossSpawnPoint;
    public Transform bossCamPoint;
    public GameObject boss;

    private bool canTrigger = true;

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

            // Spawn boss ? v? tr� bossSpawnPoint
            GameObject spawnedBoss = Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
            boss = spawnedBoss;
            canTrigger = false;
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
                canTrigger = true;
            }
        }
    }
}