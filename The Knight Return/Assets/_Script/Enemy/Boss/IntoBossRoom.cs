using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntoBossRoom : MonoBehaviour
{
    public CameraManager cam;
    public GameObject bossPrefab;
    public List<BossDoor> doors;
    public Transform bossSpawnPoint;
    public Transform bossCamPoint;
    public GameObject boss;

    private bool isTrigger = true;
    private bool bossDead = false;

    public void Start()
    {
        boss = GameObject.FindGameObjectWithTag("MiniBoss");
    }

    public void Update()
    {
        if (bossDead)
        {
            // N?u boss ?ã ch?t, không c?n ph?n ?ng v?i s? ki?n trong vùng kích ho?t
            return;
        }

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
        if (!bossDead && collision.gameObject.tag == "Player" && isTrigger)
        {
            cam.CamBossRoom();
            foreach (BossDoor door in doors)
            {
                door.CloseDoor();
            }

            // Spawn boss ? v? trí bossSpawnPoint
            GameObject spawnedBoss = Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
            boss = spawnedBoss;
            isTrigger = false;

            if(boss == null)
            {
                bossDead = true; // nguoi choi tieu diet boss khi dang trigger
            }
        }
    }

    // Nguoi choi chet
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            cam.BossDie();
            foreach (BossDoor door in doors)
            {
                door.OpenDoor();
            }
            if (boss != null)
            {
                Destroy(boss);
                bossDead = false; //khi boss bien mat khi nguoi choi chet
                isTrigger = true;
            }
        }
    }
}
