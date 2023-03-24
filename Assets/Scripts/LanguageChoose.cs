using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageChoose : MonoBehaviour
{
    public static LanguageChoose Instance { get; private set; }
    public const string PLAYER_PREFS_LANGUAGE = "ENG";


    public Language currentLanguage;
    public enum Language
    {
        PL,
        ENG,
        DK,
        FIN,
    }
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
        LoadLanguageSelection();

    }

    public void ChangeLanguage(Language newLanguage)
    {
        currentLanguage = newLanguage;
        SaveLanguageSelection(newLanguage);
    } 

    public Language GetCurrentLanguage()
    {
        return currentLanguage;
    }



    public Language LoadLanguageSelection()
    {
        if (PlayerPrefs.HasKey("LanguageSelection"))
        {
            string languageSelection = PlayerPrefs.GetString("LanguageSelection");
            currentLanguage = (Language)Enum.Parse(typeof(Language), languageSelection);
        }
        else
        {
            currentLanguage = Language.ENG; // Set a default language if no selection was saved
        }
        return currentLanguage;
    }


    public void SaveLanguageSelection(Language selectedLanguage)
    {
        PlayerPrefs.SetString("LanguageSelection", selectedLanguage.ToString());
    }
}
