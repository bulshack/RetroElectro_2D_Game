using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class audioSettings : MonoBehaviour {

    // Use this for initialization
    public AudioMixer audioMixer;
    [Space(10)]
    public Slider musicSlider;
    public AudioMixerGroup SFXMixerGroup;
    public Slider sfxSlider;
    public bool sfxMuted = false;

    public void SetMusicVolume (float volume)
    {
        audioMixer.SetFloat("musicVolume", volume);
    }

    public void SetSfxVolume (float volume)
    {
        SFXMixerGroup.audioMixer.SetFloat("SFXVolume", volume);
    }

    public void MuteSFX()
    {
        sfxMuted = !sfxMuted;
        float sfxVolumeSaved = PlayerPrefs.GetFloat("SFXVolume", 0);
        if (sfxMuted)
            SFXMixerGroup.audioMixer.SetFloat("SFXVolume", -100f);
        else if (!sfxMuted)
            SFXMixerGroup.audioMixer.SetFloat("SFXVolume", sfxVolumeSaved);
        else
            Debug.Log("Check SFX volume settings");
    }

    public void Start()
    {
        // SetMusicVolume(PlayerPrefs.GetFloat("musicVolume", 0));
        // SetSfxVolume(PlayerPrefs.GetFloat("SFXVolume", 0));
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 0);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0);
    }

    private void OnDisable()
    {
        float MusicVolume = 0;
        float sfxVolume = 0;

        audioMixer.GetFloat("musicVolume", out MusicVolume);
        audioMixer.GetFloat("SFXVolume", out sfxVolume);

        PlayerPrefs.SetFloat("musicVolume", MusicVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.Save();
    }
}
