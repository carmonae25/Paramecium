using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundOptions : MonoBehaviour
{
    public AudioMixer myAudioMixer;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start() {

        if(PlayerPrefs.GetInt("set first time volume") == 0){
            PlayerPrefs.SetInt("set first time volume", 1);
            masterSlider.value = .25f;
            musicSlider.value = .5f;
            sfxSlider.value = .5f;
        }
        else{
            masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        }
    }

    public void SetMasterVolume(){
        SetVolume("MasterVolume", masterSlider.value);
    }
    public void SetMusicVolume(){
        SetVolume("MusicVolume", musicSlider.value);
    }    
    public void SetSFXVolume(){
        SetVolume("SFXVolume", sfxSlider.value);
    }

    void SetVolume(string name, float value){
        float volume = Mathf.Log10(value) * 20;

        if(value == 0) volume = -80;

        myAudioMixer.SetFloat(name, volume);

        PlayerPrefs.SetFloat(name, value);
    }
}
