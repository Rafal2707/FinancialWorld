using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaitingForOtherPlayersUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI waitingForOtherPlayersText;    
    

    private void Start()
    {
        GameManager.Instance.OnLocalPlayerReadyChanged += GameManager_OnLocalPlayerReadyChanged;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        switch (LanguageChoose.Instance.currentLanguage)
        {
            case LanguageChoose.Language.PL:
                waitingForOtherPlayersText.text = "OCZEKIWANIE NA INNYCH GRACZY...";
                break;
            case LanguageChoose.Language.ENG:
                waitingForOtherPlayersText.text = "WAITING FOR PLAYERS...";
                break;
            case LanguageChoose.Language.DK:
                waitingForOtherPlayersText.text = "VENTER PÅ SPILLERE...";
                break;
            case LanguageChoose.Language.FIN:
                waitingForOtherPlayersText.text = "ODOTAAN PELAAJIA...";
                break;
        }
        Hide();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsLocalPlayerReady())
        {
            Hide();
        }
    }

    private void GameManager_OnLocalPlayerReadyChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsLocalPlayerReady())
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
