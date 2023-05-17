using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI correctScrollsText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI LabelCorrectScrollsText;
    [SerializeField] private TextMeshProUGUI PlayAgainButtonText;
    [SerializeField] private Button playAgainButton;
    [SerializeField] private ScoreUI scoreUI;



    private void Awake()
    {
        playAgainButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.Shutdown();
            Loader.Load(Loader.Scene.MainMenuScene);
        });
    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        switch(LanguageChoose.Instance.currentLanguage)
        {
            case LanguageChoose.Language.PL:
                gameOverText.text = "CZAS SIĘ SKOŃCZYŁ!";
                LabelCorrectScrollsText.text = "POPRAWNIE PRZYPISANE AKTYWNOŚCI";
                PlayAgainButtonText.text = "ZAGRAJ PONOWNIE";
                break;
            case LanguageChoose.Language.ENG:
                gameOverText.text = "TIME IS UP!";
                LabelCorrectScrollsText.text = "CORRECTLY ASSIGNED ACTIVITIES";
                PlayAgainButtonText.text = "PLAY AGAIN";
                break;
            case LanguageChoose.Language.DK:
                gameOverText.text = "TIDEN ER GÅET!";
                LabelCorrectScrollsText.text = "KORREKT TILDELTE AKTIVITETER";
                PlayAgainButtonText.text = "SPIL IGEN";
                break;
            case LanguageChoose.Language.FIN:
                gameOverText.text = "AIKA ON LOPPU!";
                LabelCorrectScrollsText.text = "OIKEAT VALINNAT";
                PlayAgainButtonText.text = "PELAA UUDESTAAN";
                break;
        }

        Hide();

    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            Show();
            correctScrollsText.text = scoreUI.GetScore().ToString() + "/" + scoreUI.GetTries().ToString();
        }
        else Hide();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
        playAgainButton.Select();
    }
}
