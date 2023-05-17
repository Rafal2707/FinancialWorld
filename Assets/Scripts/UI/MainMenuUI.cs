using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playMultiplayerButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button playSingleplayerButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button languagePLButton;
    [SerializeField] private Button languageENGButton;
    [SerializeField] private Button languageDKButton;
    [SerializeField] private Button languageFINButton;


    [SerializeField] private TextMeshProUGUI playSingleplayerButtonText;
    [SerializeField] private TextMeshProUGUI playMultiplayerButtonText;
    [SerializeField] private TextMeshProUGUI quitButtonText;
    [SerializeField] private TextMeshProUGUI optionsButtonText;

    private void Awake()
    {
        optionsButton.onClick.AddListener(() =>
        {
            OptionsMainMenuUI.Instance.Show();
            OptionsMainMenuUI.Instance.UpdateVisual();
        });

        playMultiplayerButton.onClick.AddListener(() => {
            GameManagerMultiplayer.playMultiplayer = true;
            Loader.Load(Loader.Scene.LobbyScene);
        });
        playSingleplayerButton.onClick.AddListener(() => {
            GameManagerMultiplayer.playMultiplayer = false;
            Loader.Load(Loader.Scene.LobbyScene);
        });
        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });

        languagePLButton.onClick.AddListener(() =>
        {
            LanguageChoose.Instance.ChangeLanguage(LanguageChoose.Language.PL);
            ChangeMenuLanguageText("JEDEN GRACZ", "WIELU GRACZY", "WYJDŹ", "OPCJE");
        });
        languageENGButton.onClick.AddListener(() =>
        {
            LanguageChoose.Instance.ChangeLanguage(LanguageChoose.Language.ENG);
            ChangeMenuLanguageText("SINGLEPLAYER", "MULTIPLAYER", "QUIT", "OPTIONS");
        });
        languageDKButton.onClick.AddListener(() =>
        {
            LanguageChoose.Instance.ChangeLanguage(LanguageChoose.Language.DK);
            ChangeMenuLanguageText("SINGLEPLAYER", "MULTIPLAYER", "AFSLUT", "OPTIONER");
        });
        languageFINButton.onClick.AddListener(() =>
        {
            LanguageChoose.Instance.ChangeLanguage(LanguageChoose.Language.FIN);
            ChangeMenuLanguageText("YKSINPELI", "MONINPELI", "LOPETA", "ASETUKSET");
        });

        Time.timeScale = 1f;
    }

    private void Start()
    {
        switch (LanguageChoose.Instance.GetCurrentLanguage())
        {
            case LanguageChoose.Language.PL:
                ChangeMenuLanguageText("JEDEN GRACZ", "WIELU GRACZY", "WYJDŹ", "OPCJE");
                break;
            case LanguageChoose.Language.ENG:
                ChangeMenuLanguageText("SINGLEPLAYER", "MULTIPLAYER", "QUIT", "OPTIONS");
                break;
            case LanguageChoose.Language.DK:
                ChangeMenuLanguageText("SINGLEPLAYER", "MULTIPLAYER", "AFSLUT", "OPTIONER");
                break;
            case LanguageChoose.Language.FIN:
                ChangeMenuLanguageText("YKSINPELI", "MONINPELI", "LOPETA", "ASETUKSET");
                break;
        }
    }
    private void ChangeMenuLanguageText(string singleplayerText, string multiplayerText,string quitText, string optionsText)
    {
        playSingleplayerButtonText.text = singleplayerText;
        playMultiplayerButtonText.text = multiplayerText;
        quitButtonText.text = quitText;
        optionsButtonText.text = optionsText;
    }
}
