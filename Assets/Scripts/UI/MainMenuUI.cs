using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playMultiplayerButton;
    [SerializeField] private Button playSingleplayerButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button languagePLButton;
    [SerializeField] private Button languageENGButton;
    [SerializeField] private Button languageDKButton;
    [SerializeField] private Button languageFINButton;


    [SerializeField] private TextMeshProUGUI playSingleplayerButtonText;
    [SerializeField] private TextMeshProUGUI playMultiplayerButtonText;
    [SerializeField] private TextMeshProUGUI quitButtonText;


    private void Awake()
    {


        playMultiplayerButton.onClick.AddListener(() => {
            GameManagerMultiplayer.playMultiplayer = true;
            Loader.Load(Loader.Scene.LobbyScene);
        });
        playSingleplayerButton.onClick.AddListener(() => {
            GameManagerMultiplayer  .playMultiplayer = false;
            Loader.Load(Loader.Scene.LobbyScene);
        });
        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });

        languagePLButton.onClick.AddListener(() =>
        {
            LanguageChoose.Instance.ChangeLanguage(LanguageChoose.Language.PL);
            ChangeMenuLanguageText("JEDEN GRACZ", "WIELU GRACZY", "WYJDZ");
        });
        languageENGButton.onClick.AddListener(() =>
        {
            LanguageChoose.Instance.ChangeLanguage(LanguageChoose.Language.ENG);
            ChangeMenuLanguageText("SINGLEPLAYER", "MULTIPLAYER", "QUIT");
        });
        languageDKButton.onClick.AddListener(() =>
        {
            LanguageChoose.Instance.ChangeLanguage(LanguageChoose.Language.DK);
            ChangeMenuLanguageText("SINGLEPLAYER", "MULTIPLAYER", "AFSLUT");
        });
        languageFINButton.onClick.AddListener(() =>
        {
            LanguageChoose.Instance.ChangeLanguage(LanguageChoose.Language.FIN);
            ChangeMenuLanguageText("YKSINPELI", "MONINPELI", "LOPETTAA");
        });

        Time.timeScale = 1f;
    }

    private void Start()
    {
        switch (LanguageChoose.Instance.GetCurrentLanguage())
        {
            case LanguageChoose.Language.PL:
                ChangeMenuLanguageText("JEDEN GRACZ", "WIELU GRACZY", "WYJDZ");
                break;
            case LanguageChoose.Language.ENG:
                ChangeMenuLanguageText("SINGLEPLAYER", "MULTIPLAYER", "QUIT");
                break;
            case LanguageChoose.Language.DK:
                ChangeMenuLanguageText("SINGLEPLAYER", "MULTIPLAYER", "AFSLUT");
                break;
            case LanguageChoose.Language.FIN:
                ChangeMenuLanguageText("YKSINPELI", "MONINPELI", "LOPETTAA");
                break;
        }
    }
    private void ChangeMenuLanguageText(string singleplayerText, string multiplayerText,string quitText)
    {
        playSingleplayerButtonText.text = singleplayerText;
        playMultiplayerButtonText.text = multiplayerText;
        quitButtonText.text = quitText;
    }

}
