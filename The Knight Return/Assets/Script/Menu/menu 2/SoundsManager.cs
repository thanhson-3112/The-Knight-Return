/*using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum SoundType
{
    Music,
    VFX
}

public class SoundsManager : MonoBehaviour
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
        
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume", Mathf.Log10(volumeSlider.value)*20);
        VFXVolumeSlider.value = PlayerPrefs.GetFloat("VFXVolume", Mathf.Log10(volumeSlider.value) * 20);
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
                // ??t �m l??ng c?a VFX
                // Code ?? ?i?u ch?nh �m l??ng c?a hi?u ?ng h�nh ?nh ??ng ? ?�y
                break;
            default:
                break;
        }

        // L?u gi� tr? �m l??ng sau khi ?i?u ch?nh
        Save();
    }
}
*/