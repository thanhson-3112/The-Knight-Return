using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFxManager : MonoBehaviour
{
    public static SoundFxManager instance;
    public AudioSource audioFxObject;
    private AudioSource runningAudioSource; 

    public AudioClip RunSoundEffect;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        if (audioClip == RunSoundEffect)
        {
            if (runningAudioSource != null && runningAudioSource.isPlaying)
            {
                return; 
            }
        }

        AudioSource audioSource = Instantiate(audioFxObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();

        if (audioClip == RunSoundEffect)
        {
            runningAudioSource = audioSource;
        }

        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

    public void StopRunningSound()
    {
        if (runningAudioSource != null && runningAudioSource.isPlaying)
        {
            runningAudioSource.Stop();
            Destroy(runningAudioSource.gameObject);
            runningAudioSource = null;
        }
    }
}
