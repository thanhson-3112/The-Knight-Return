using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMap1 : MonoBehaviour
{
    public AudioManager audioManager;
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("da trigger");
            audioManager.PlayAudio(audioManager.map1Audio);
        }
    }
}
