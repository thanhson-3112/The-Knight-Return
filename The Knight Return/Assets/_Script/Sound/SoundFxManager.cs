using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFxManager : MonoBehaviour
{
    public static SoundFxManager instance;
    public AudioSource audioFxObject;
    private Dictionary<AudioClip, List<AudioSource>> playingAudioSources = new Dictionary<AudioClip, List<AudioSource>>();
    private float vfxVolume = 1f; 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SetVFXVolume(float volume)
    {
        vfxVolume = volume;

        // volume
        foreach (var audioSources in playingAudioSources.Values)
        {
            foreach (var audioSource in audioSources)
            {
                audioSource.volume = vfxVolume;
            }
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(audioFxObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume * vfxVolume; 
        audioSource.Play();

        if (!playingAudioSources.ContainsKey(audioClip))
        {
            playingAudioSources[audioClip] = new List<AudioSource>();
        }

        playingAudioSources[audioClip].Add(audioSource);

        float clipLength = audioSource.clip.length;
        StartCoroutine(DestroyAfter(audioSource.gameObject, audioClip, clipLength));
    }

    public void StopAudio(AudioClip audioClip)
    {
        if (playingAudioSources.ContainsKey(audioClip))
        {
            List<AudioSource> audioSources = playingAudioSources[audioClip];
            foreach (AudioSource audioSource in audioSources)
            {
                if (audioSource != null)
                {
                    audioSource.Stop();
                    Destroy(audioSource.gameObject);
                }
            }

            playingAudioSources.Remove(audioClip);
        }
    }

    IEnumerator DestroyAfter(GameObject gameObject, AudioClip audioClip, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);

        if (playingAudioSources.ContainsKey(audioClip))
        {
            playingAudioSources[audioClip].RemoveAll(source => source == null);
            if (playingAudioSources[audioClip].Count == 0)
            {
                playingAudioSources.Remove(audioClip);
            }
        }
    }
}
