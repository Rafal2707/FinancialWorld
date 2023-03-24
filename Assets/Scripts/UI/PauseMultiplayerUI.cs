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
                pauseText.text = "WAITING FOR OTHER PLAYERS TO RESUME";
                break;
            case LanguageChoose.Language.DK:
                pauseText.text = "VENTER PÅ, AT ANDRE SPILLERE GENOPTAGES";
                break;
            case LanguageChoose.Language.FIN:
                pauseText.text = "ODOTTAAN MUIDEN PELAAJIEN JATKAMISTA";
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
