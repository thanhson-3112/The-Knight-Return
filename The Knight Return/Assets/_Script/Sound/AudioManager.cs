using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource backgounAudio;

    [Header("Map1")]
    public AudioClip map1Audio;

    [Header("Map2")]
    public AudioClip map2Audio;
    public AudioClip bossBBA;
    public AudioClip bossMH;

    [Header("Map3")]
    public AudioClip map3Audio;
    public AudioClip bossFP;
    public AudioClip bossTusk;

    [Header("Map4")]
    public AudioClip map4Audio;
    public AudioClip bossRM;
    public AudioClip bossNM;

    [Header("Map5")]
    public AudioClip map5Audio;
    public AudioClip bossPM;
    public AudioClip bossBoD;

    public void PlayAudio(AudioClip audioClip)
    {
        backgounAudio.clip = audioClip;
        backgounAudio.Play();
    }
}
