using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicMenager : MonoBehaviour
{

    public static MenuMusicMenager Instance { get; private set; }
    private void Start()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }
}
