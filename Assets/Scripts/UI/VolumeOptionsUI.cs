using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeOptionsUI : MonoBehaviour
{
    public AudioMixer mainAudioMixer;
    public Slider mainVolumeSlider;
    //public AudioMixer SFXVolumeMixer;
    public Slider SFXVolumeSlider;
    //public AudioMixer musicAudioMixer;
    public Slider musicAudioSlider;
    //public AudioMixer ambienceAudioMixer;
    public Slider ambienceAudioSlider;

    // Start is called before the first frame update
    public void Start()
    {
        OnMainVolumeChange();
    }

    public void OnMainVolumeChange()
    {
        float newVolume = mainVolumeSlider.value;
        if (newVolume <= 0)
        {
            newVolume = -80;
        }
        else
        {
            newVolume = Mathf.Log10(newVolume);
            newVolume = newVolume * 20;
        }

        mainAudioMixer.SetFloat("MasterVolumeMixer", newVolume);

    }

    public void OnSFXVolumeChange()
    {
        float newVolume = SFXVolumeSlider.value;
        if (newVolume <= 0)
        {
            newVolume = -80;
        }
        else
        {
            newVolume = Mathf.Log10(newVolume);
            newVolume = newVolume * 20;
        }

        mainAudioMixer.SetFloat("SFXVolumeMixer", newVolume);
    }

    public void OnMusicVolumeChange()
    {
        float newVolume = musicAudioSlider.value;
        if (newVolume <= 0)
        {
            newVolume = -80;
        }
        else
        {
            newVolume = Mathf.Log10(newVolume);
            newVolume = newVolume * 20;
        }

        mainAudioMixer.SetFloat("MusicVolumeMixer", newVolume);
    }

    public void OnAmbienceVolumeChange()
    {
        float newVolume = ambienceAudioSlider.value;
        if (newVolume <= 0)
        {
            newVolume = -80;
        }
        else
        {
            newVolume = Mathf.Log10(newVolume);
            newVolume = newVolume * 20;
        }

        mainAudioMixer.SetFloat("AmbienceVolumeMixer", newVolume);
    }

}
