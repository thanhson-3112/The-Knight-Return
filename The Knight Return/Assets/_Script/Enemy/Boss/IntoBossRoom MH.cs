using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntoBossRoomMH: MonoBehaviour
{
    public GameObject bossPrefab;
    public List<BossDoor> doors;
    public Transform bossSpawnPoint;
    public GameObject boss;

    private bool isTrigger = true;
    private bool bossDead = false;

    public void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");
    }

    public void Update()
    {
        if (bossDead)
        {
            return;
        }

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
        if (!bossDead && collision.gameObject.tag == "Player" && isTrigger)
        {
            foreach (BossDoor door in doors)
            {
                door.CloseDoor();
            }

            // Spawn boss ? v? tr� bossSpawnPoint
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
