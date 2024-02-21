using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : Singleton<AudioController>
{
    [Header("Main Settings: ")]
    [Range(0f, 1f)]
    public float musicVolume;
    [Range(0f, 1f)]
    public float sfxVolume;

    public AudioSource musicaus;
    public AudioSource sfxaus;


    [Header("Game Sound And Musics: ")]
    public AudioClip shooting;
    public AudioClip win;
    public AudioClip lose;
    public AudioClip[] bgmusics;



    public override void Start()
    {
        PlayMusic(bgmusics);
    }


    public void PlaySound(AudioClip sound , AudioSource aus = null)
    {
        if (!aus) 
        {
            aus = sfxaus;
        }
        if (aus)
        {
            aus.PlayOneShot(sound, sfxVolume);
        }    
    }    

    public void PlaySound(AudioClip[] sound, AudioSource aus = null)
    {
        if (!aus)
        {
            aus = sfxaus;
        }

        if (aus)
        {
            int randIdx = Random.Range(0,sound.Length);

            if (sound[randIdx] != null)
            {
                aus.PlayOneShot(sound[randIdx], sfxVolume);
            }    
        }
    }

    public void PlayMusic(AudioClip music, bool loop = true)
    {
        if (musicaus)
        {
            musicaus.clip = music;
            musicaus.loop = loop;
            musicaus.volume = musicVolume;
            musicaus.Play();
        }
    }

    public void PlayMusic(AudioClip[] musics, bool loop = true)
    {
        if (musicaus)
        {
            int randIdx = Random.Range(0, musics.Length);

            if (musics[randIdx] != null)
            {
                musicaus.clip = musics[randIdx];
                musicaus.loop = loop;
                musicaus.volume = musicVolume;
                musicaus.Play();
            }    
        }
    }
}
