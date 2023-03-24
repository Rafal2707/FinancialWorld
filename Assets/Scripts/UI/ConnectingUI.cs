using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConnectingUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI connectingText;
    private void Start()
    {
        GameManagerMultiplayer.Instance.OnTryingToJoinGame += GameManager_OnTryingToJoinGame;
        GameManagerMultiplayer.Instance.OnFailedToJoinGame += GameManager_OnFailedToJoinGame;

        switch (LanguageChoose.Instance.currentLanguage)
        {
            case LanguageChoose.Language.PL:
                connectingText.text = "ŁĄCZENIE...";
                break;
            case LanguageChoose.Language.ENG:
                connectingText.text = "CONNECTING...";
                break;
            case LanguageChoose.Language.DK:
                connectingText.text = "TILSLUTNING...";
                break;
            case LanguageChoose.Language.FIN:
                connectingText.text = "YHTEYTTÄ...";
                break;
        }


        Hide();
    }

    private void GameManager_OnFailedToJoinGame(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void GameManager_OnTryingToJoinGame(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameManagerMultiplayer.Instance.OnTryingToJoinGame -= GameManager_OnTryingToJoinGame;
        GameManagerMultiplayer.Instance.OnFailedToJoinGame -= GameManager_OnFailedToJoinGame;
    }
}
