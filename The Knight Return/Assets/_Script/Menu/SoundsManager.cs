using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum SoundType
{
    Music,
    VFX
}

public class SoundsVolume : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider VFXVolumeSlider;

    void Start()
    {
        Load();
    }

    public void ChangeVolume()
    {
        SetVolume(SoundType.Music, volumeSlider.value);
    }

    public void ChangeVFXVolume()
    {
        SetVolume(SoundType.VFX, VFXVolumeSlider.value);
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume", volumeSlider.value);
        VFXVolumeSlider.value = PlayerPrefs.GetFloat("VFXVolume", VFXVolumeSlider.value);
    }


    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
        PlayerPrefs.SetFloat("VFXVolume", VFXVolumeSlider.value);
    }

    private void SetVolume(SoundType soundType, float volume)
    {
        switch (soundType)
        {
            case SoundType.Music:
                AudioListener.volume = volume;
                break;
            case SoundType.VFX:
                SoundFxManager.instance.SetVFXVolume(volume);
                break;
            default:
                break;
        }

        Save();
    }
}
