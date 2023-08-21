using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseMultiplayerUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI pauseText;
    private void Start()
    {
        GameManager.Instance.OnMultiplayerGamePaused += GameManager_OnMultiplayerGamePaused;
        GameManager.Instance.OnMultiplayerGameUnpaused += GameManager_OnMultiplayerGameUnaused;


        switch (LanguageChoose.Instance.GetCurrentLanguage())
        {
            case LanguageChoose.Language.PL:
                pauseText.text = "OCZEKAWIANIE NA WZNOWIENIE GRY PRZEZ INNYCH GRACZY";
                break;
            case LanguageChoose.Language.ENG:
                pauseText.text = "WAITING FOR ALL PLAYERS TO UNPAUSE";
                break;
            case LanguageChoose.Language.DK:
                pauseText.text = "VENTER PÅ, AT ALLE SPILLERNE AFBRYDER DERES PAUSE";
                break;
            case LanguageChoose.Language.FIN:
                pauseText.text = "ODOTTAA ETTÄ MUUT PELAAJAT OVAT VALMIITA";
                break;
        }

        Hide();
    }

    private void GameManager_OnMultiplayerGameUnaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void GameManager_OnMultiplayerGamePaused(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
