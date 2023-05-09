using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public AudioSource[] music;
    public RawImage[] musicItems;
    int musicIndex = 0;
    public Slider slider;
    public GameObject musicPlayer;

    public bool loopOption = false;
    public RawImage loopButton;

    public bool mute;
    public RawImage muteButton;

    public bool pauseOrPlay;
    public RawImage pauseOrPlayButton;
    public RawImage pauseOrPlayBackground;
    // Icon textures for pause and play
    public Texture[] buttonTextures;
    // 0. Play 
    // 1. Pause

    public Color32 defaultColor;
    public Color32 defaultPauseOrPlayColor;
    public Color32 highlightColor;

    // Start is called before the first frame update
    void Start()
    {
        defaultColor = loopButton.color;
        defaultPauseOrPlayColor = pauseOrPlayBackground.color;
        StartSong(0);
        SetPauseOrPlay(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(musicPlayer.activeInHierarchy)
        {
            slider.value = music[musicIndex].time;
            if(music[musicIndex].isPlaying)
            {
                for (int i = 0; i < music.Length; i++)
                {
                    if(i == musicIndex)
                    {
                        musicItems[i].color = highlightColor;
                    }else
                    {
                        musicItems[i].color = defaultColor;
                    }
                }
            }
        }
        if(music[musicIndex].time >= music[musicIndex].clip.length && !loopOption)
        {
            StartSong(musicIndex + 1);
        }
        else if (music[musicIndex].time >= music[musicIndex].clip.length && loopOption)
        {
            StartSong(musicIndex);
        }
    }

    public void StartSong(int index)
    {
        if(index >= music.Length)
        {
            index = 0;
        }
        for (int i = 0; i < music.Length; i++)
        {
            if(music[i].isPlaying)
            {
                music[i].Stop();
            }
        }
        musicIndex = index;
        music[musicIndex].Play();
        if(musicPlayer.activeInHierarchy)
        {
            slider.maxValue = music[musicIndex].clip.length;
            slider.minValue = 0;
            slider.value = slider.minValue;
        }
        else
        {
            musicPlayer.SetActive(true);
            slider.maxValue = music[musicIndex].clip.length;
            slider.minValue = 0;
            slider.value = slider.minValue;
            musicPlayer.SetActive(false);
        }
    }

    public void MuteButton()
    {
        if (mute)
        {
            mute = false;
            muteButton.color = defaultColor;
            for (int i = 0; i < music.Length; i++)
            {
                music[i].mute = false;
            }
        }
        else
        {
            mute = true;
            muteButton.color = highlightColor;
            for (int i = 0; i < music.Length; i++)
            {
                music[i].mute = true;
            }
        }
    }

    public void LoopButton()
    {
        if(loopOption)
        {
            loopOption = false;
            loopButton.color = defaultColor;
        }
        else
        {
            loopOption = true;
            loopButton.color = highlightColor;
        }
    }

    public void SongButton(int index)
    {
        if(index != musicIndex)
        {
            StartSong(index);
        }
    }

    public void PauseOrPlay()
    {
        pauseOrPlay = !pauseOrPlay;
        if (pauseOrPlay)
        {
            pauseOrPlayButton.texture = buttonTextures[0];
            music[musicIndex].Pause();
            pauseOrPlayBackground.color = highlightColor;
        }
        else
        {
            pauseOrPlayButton.texture = buttonTextures[1];
            music[musicIndex].UnPause();
            pauseOrPlayBackground.color = defaultPauseOrPlayColor;
        }
    }

    public void SetPauseOrPlay(bool value)
    {
        pauseOrPlay = value;
        if (pauseOrPlay)
        {
            pauseOrPlayButton.texture = buttonTextures[0];
            music[musicIndex].Pause();
            pauseOrPlayBackground.color = highlightColor;
        }
        else
        {
            pauseOrPlayButton.texture = buttonTextures[1];
            music[musicIndex].UnPause();
            pauseOrPlayBackground.color = defaultPauseOrPlayColor;
        }
    }
}
