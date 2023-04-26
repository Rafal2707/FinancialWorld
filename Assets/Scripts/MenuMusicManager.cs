using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MenuMusicManager : MonoBehaviour
{
    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";

    float musicVolume = .3f;
    private float soundEffectsVolume = .1f;
    private AudioSource audioSource;
    public static MenuMusicManager Instance { get; private set; }


    private void Start()
    {
        DontDestroyOnLoad(this);
        Instance = this;

        audioSource = GetComponent<AudioSource>();

        musicVolume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, .3f);
        soundEffectsVolume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, .1f);
        audioSource.volume = musicVolume;

    }


    public void ChangeSoundEffectsVolume()
    {
        soundEffectsVolume += .1f;
        if (soundEffectsVolume > 1f)
        {
            soundEffectsVolume = 0f;
        }

        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, soundEffectsVolume);
        PlayerPrefs.Save();
        
    }

    public void ChangeMusicVolume()
    {
        musicVolume += .1f;
        if (musicVolume > 1f)
        {
            musicVolume = 0f;
        }
        audioSource.volume = musicVolume;

        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, musicVolume);
        PlayerPrefs.Save();
    }


    public void Mute()
    {
        musicVolume = 0f;
        soundEffectsVolume = 0f;
        audioSource.volume = musicVolume;
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, musicVolume);
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, soundEffectsVolume);

        PlayerPrefs.Save();
    }


    public float GetMusicVolume()
    {
        return musicVolume;
    }

    public float GetSoundEffectsVolume()
    {
        return soundEffectsVolume;
    }



}
