using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadingLanguageHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI loadingText;


    private void Awake()
    {
        switch (LanguageChoose.Instance.GetCurrentLanguage())
        {
            case LanguageChoose.Language.PL:
                loadingText.text = "ŁADOWANIE...";
                break;
            case LanguageChoose.Language.ENG:
                loadingText.text = "LOADING...";
                break;
            case LanguageChoose.Language.DK:
                loadingText.text = "INDLÆSER...";
                break;
            case LanguageChoose.Language.FIN:
                loadingText.text = "LADATAAN...";
                break;
        }
    }
}
