using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AudioClipRefsSO : ScriptableObject
{
    public AudioClip[] footstep;
    public AudioClip[] ScrollIncorrect;
    public AudioClip[] ScrollCorrect;
    public AudioClip[] ScrollDrop;
    public AudioClip[] ScrollPickup;

    public AudioClip spawnedScroll;



}
