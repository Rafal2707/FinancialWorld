using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PressToRebindKeyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pressKeyToRebindKeyText;


    private void OnEnable()
    {
        switch (LanguageChoose.Instance.GetCurrentLanguage())
        {
            case LanguageChoose.Language.PL:
                pressKeyToRebindKeyText.text = "NACIŚNIJ NOWY KLAWISZ";
                break;
            case LanguageChoose.Language.ENG:
                pressKeyToRebindKeyText.text = "PRESS A KEY TO REBIND";
                break;
            case LanguageChoose.Language.DK:
                pressKeyToRebindKeyText.text = "TRYK PÅ EN NY TAST";
                break;
            case LanguageChoose.Language.FIN:
                pressKeyToRebindKeyText.text = "PAINA UUTTA NÄPPÄINTÄ";
                break;
        }
    }


}
