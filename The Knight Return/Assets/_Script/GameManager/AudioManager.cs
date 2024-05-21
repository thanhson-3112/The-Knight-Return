using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource backGroundAudioSource;
    public AudioSource vfxAudioSource;

    [Header("PlayerLife")]
    public AudioClip DamageSoundEffect;
    public AudioClip DeathSoundEffect;
    public AudioClip CheckpointSoundEffect;

    [Header("PlayerAttack")]
    public AudioClip AttackSoundEffect;
    public AudioClip HitSoundEffect;

    [Header("PlayerMovement")]
    public AudioClip RunSoundEffect;
    public AudioClip JumpSoundEffect;
    public AudioClip DashSoundEffect;


    void Start()
    {
            
    }

    public void PlayVFX(AudioClip sfxClip)
    {
        vfxAudioSource.clip = sfxClip;
        vfxAudioSource.PlayOneShot(sfxClip);
    }

    public void StopVFX(AudioClip sfxClip)
    {
        if (vfxAudioSource.clip == sfxClip && vfxAudioSource.isPlaying)
        {
            vfxAudioSource.Stop();
        }
    }
}
