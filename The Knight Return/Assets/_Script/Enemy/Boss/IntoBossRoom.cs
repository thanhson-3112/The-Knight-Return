using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntoBossRoom : MonoBehaviour
{
    public CameraManager cam;
    public GameObject boss;
    public BossDoor door;

    public void Update()
    {
        if(boss == null)
        {
            cam.BossDie();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            cam.CamBossRoom();
            door.OpenDoor();
        }
    }
}
