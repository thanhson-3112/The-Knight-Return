using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource backGroundAudioSource;
    public AudioSource vfxAudioSource;


    [Header("PlayerHealing")]
    public AudioClip focusHeallingSound;
    public AudioClip healingSound;

    void Start()
    {

    }

    public void PlayVFX(AudioClip sfxClip)
    {
        vfxAudioSource.clip = sfxClip;
        vfxAudioSource.Play();
    }

    public void StopVFX(AudioClip sfxClip)
    {
        if (vfxAudioSource.clip == sfxClip && vfxAudioSource.isPlaying)
        {
            vfxAudioSource.Stop();
        }
    }

   
}
