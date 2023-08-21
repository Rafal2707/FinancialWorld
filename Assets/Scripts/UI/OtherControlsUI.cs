using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OtherControlsUI : MonoBehaviour
{
    public static OtherControlsUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI controlsText;
    [SerializeField] private TextMeshProUGUI keyboardText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private TextMeshProUGUI pressInteractToStartText;

    private void Awake()
    {
        Instance = this;

    }

    private void Start()
    {
        switch (LanguageChoose.Instance.GetCurrentLanguage())
        {
            case LanguageChoose.Language.PL:
                controlsText.text = "KLAWISZE FUNKCYJNE";
                keyboardText.text = "KLAWIATURA";
                interactText.text = "INTERAKCJA";
                pauseText.text = "PAUZA";
                pressInteractToStartText.text = "NACIŚNIJ 'INTERAKCJA' BY ROZPOCZĄĆ";
                break;
            case LanguageChoose.Language.ENG:
                controlsText.text = "FUNCTION KEYS";
                keyboardText.text = "KEYBOARD";
                interactText.text = "INTERACT";
                pauseText.text = "PAUSE";
                pressInteractToStartText.text = "PRESS 'INTERACT' TO START";
                break;
            case LanguageChoose.Language.DK:
                controlsText.text = "FUNKTIONSTASTER";
                keyboardText.text = "TASTATUR";
                interactText.text = "INTERAGERE";
                pauseText.text = "PAUSE";
                pressInteractToStartText.text = "TRYK PÅ 'INTERAGERE' FOR AT STARTE";
                break;
            case LanguageChoose.Language.FIN:
                controlsText.text = "NÄPPÄINTOIMINNOT";
                keyboardText.text = "NÄPPÄIMISTÖ";
                interactText.text = "POIMI/PUDOTA";
                pauseText.text = "TAUKO";
                pressInteractToStartText.text = "PAINA 'POIMI/PUDOTA' JATKAAKSESI";
                break;
        }



        Hide();
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
