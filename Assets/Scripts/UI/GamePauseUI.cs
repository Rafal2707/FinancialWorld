using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;

    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private TextMeshProUGUI resumeButtonText;
    [SerializeField] private TextMeshProUGUI optionsButtonText;
    [SerializeField] private TextMeshProUGUI mainMenuButtonText;



    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.TogglePauseGame();
        });
        mainMenuButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.Shutdown();
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        optionsButton.onClick.AddListener(() =>
        {
            Hide();
            OptionsUI.Instance.Show(Show);
        });
    }
    private void Start()
    {
        GameManager.Instance.OnLocalGamePaused += GameManager_OnLocalGamePaused;
        GameManager.Instance.OnLocalGameUnpaused += GameManager_OnLocalGameUnpaused;


        switch (LanguageChoose.Instance.GetCurrentLanguage())
        {
            case LanguageChoose.Language.PL:
                pauseText.text = "PAUZA";
                resumeButtonText.text = "WZNÓW";
                optionsButtonText.text = "OPCJE";
                mainMenuButtonText.text = "MENU GŁÓWNE";
                break;
            case LanguageChoose.Language.ENG:
                pauseText.text = "PAUSE";
                resumeButtonText.text = "RESUME";
                optionsButtonText.text = "OPTIONS";
                mainMenuButtonText.text = "MAIN MENU";
                break;
            case LanguageChoose.Language.DK:
                pauseText.text = "PAUSE";
                resumeButtonText.text = "RESUME";
                optionsButtonText.text = "OPTIONER";
                mainMenuButtonText.text = "HOVEDMENU";
                break;
            case LanguageChoose.Language.FIN:
                pauseText.text = "TAUKO";
                resumeButtonText.text = "JATKA";
                optionsButtonText.text = "ASETUKSET";
                mainMenuButtonText.text = "PÄÄVALIKKO";
                break;
        }

        Hide();
    }

    private void GameManager_OnLocalGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void GameManager_OnLocalGamePaused(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);

        resumeButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
