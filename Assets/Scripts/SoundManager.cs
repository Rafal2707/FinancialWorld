using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{

    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;


    [SerializeField] private CentralBank centralBank;
    [SerializeField] private CommercialBank commercialBank;

    private float volume = .1f;


    private void Awake()
    {
        Instance = this;

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, .1f);

    }
    private void Start()
    {
        centralBank.OnScrollCorrect += CentralBank_OnScrollCorrect;
        centralBank.OnScrollIncorrect += CentralBank_OnScrollIncorrect;
        commercialBank.OnScrollCorrect += CommercialBank_OnScrollCorrect;
        commercialBank.OnScrollIncorrect += CommercialBank_OnScrollIncorrect;

        Spawner.Instance.OnSpawnedScroll += Spawner_OnSpawnedScroll;


        ActivityScroll.OnAnyPickUpScroll += ActivityScroll_OnAnyPickUpScroll;
        ActivityScroll.OnAnyDropScroll += ActivityScroll_OnAnyDropScroll;
    }

    private void ActivityScroll_OnAnyDropScroll(object sender, EventArgs e)
    {
        PlaySound(audioClipRefsSO.ScrollDrop, Camera.main.transform.position);
    }

    private void ActivityScroll_OnAnyPickUpScroll(object sender, EventArgs e)
    {
        PlaySound(audioClipRefsSO.ScrollPickup, Camera.main.transform.position);
    }

    private void Spawner_OnSpawnedScroll(object sender, EventArgs e)
    {
        PlaySound(audioClipRefsSO.spawnedScroll, Camera.main.transform.position);
    }


    private void CommercialBank_OnScrollIncorrect(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.ScrollIncorrect, Camera.main.transform.position);
    }

    private void CommercialBank_OnScrollCorrect(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.ScrollCorrect, Camera.main.transform.position);

    }

    private void CentralBank_OnScrollIncorrect(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.ScrollIncorrect, Camera.main.transform.position);
    }

    private void CentralBank_OnScrollCorrect(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.ScrollCorrect, Camera.main.transform.position);

    }






    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 0.5f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);

    }
    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }

    public void PlayFootstepsSound(Vector3 position, float volume)
    {
        PlaySound(audioClipRefsSO.footstep, position, volume);
    }

    public void PlayCountdownSound()
    {
        PlaySound(audioClipRefsSO.spawnedScroll, Camera.main.transform.position);
    }


    public void ChangeVolume()
    {
        volume += .1f;
        if (volume > 1f)
        {
            volume = 0f;
        }

        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }


    public void Mute()
    {
        volume = 0f;
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }


    public float GetVolume()
    {
        return volume;
    }
}
