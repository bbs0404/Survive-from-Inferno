﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : SingletonBehaviour<SoundManager> {

    [SerializeField]
    private float BGM_Volume = 0.5f;
    [SerializeField]
    private float SFX_Volume = 0.5f;
    [SerializeField]
    private AudioSource[] SFX_List;
    [SerializeField]
    private AudioSource BGM;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += Initialize;
    }
    private void Initialize(Scene arg0, LoadSceneMode arg1)
    {
        SFX_List = (AudioSource [])GameObject.FindObjectsOfType(typeof(AudioSource));
        foreach (var item in SFX_List)
        {
            if (item.transform.gameObject.name.Substring(0, 3) == "BGM")
            {
                item.volume = BGM_Volume;
                BGM = item;
                BGM.loop = true;
            }
            else
                item.volume = SFX_Volume;
        }
        if (BGM)
        {
            BGM.Play();
        }
    }

    public void playAudio(AudioSource source)
    {
        source.Play();
    }

    public void stopAudio(AudioSource source)
    {
        source.Stop();
    }

    public void changeBGM(AudioClip newBGM)
    {
        BGM.clip = newBGM;
    }

    public void setSFX_Volume(Scrollbar slider)
    {
        SFX_Volume = slider.value;
        SFX_List = (AudioSource[])GameObject.FindObjectsOfType(typeof(AudioSource));
        foreach (var item in SFX_List)
        {
            if (item.transform.gameObject.name.Substring(0, 3) == "BGM")
            {
                continue;
            }
            else
                item.volume = SFX_Volume;
        }
    }

    public void setBGM_Volume(Scrollbar slider)
    {
        BGM_Volume = slider.value;
        BGM.volume = BGM_Volume;
    }
    
    public AudioSource getBGM()
    {
        return BGM;
    }
}
