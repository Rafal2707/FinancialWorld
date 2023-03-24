using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class LobbyMessageUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private TextMeshProUGUI closeButtonText;
    [SerializeField] private Button closeButton;


    private void Awake()
    {
        closeButton.onClick.AddListener(() => { 
            Hide();
        });
    }

    private void Start()
    {
        GameManagerMultiplayer.Instance.OnFailedToJoinGame += GameManager_OnFailedToJoinGame;
        GameLobby.Instance.OnCreateLobbyFailed += GameLobby_OnCreateLobbyFailed;
        GameLobby.Instance.OnCreateLobbyStarted += GameLobby_OnCreateLobbyStarted;
        GameLobby.Instance.OnJoinStarted += GameLobby_OnJoinStarted;
        GameLobby.Instance.OnJoinFailed += GameLobby_OnJoinFailed;
        GameLobby.Instance.OnQuickJoinFailed += GameLobby_OnQuickJoinFailed;


        switch (LanguageChoose.Instance.currentLanguage)
        {
            case LanguageChoose.Language.PL:
                closeButtonText.text = "ZAMKNIJ";
                break;
            case LanguageChoose.Language.ENG:
                closeButtonText.text = "CLOSE";
                break;
            case LanguageChoose.Language.DK:
                closeButtonText.text = "TÆT";
                break;
            case LanguageChoose.Language.FIN:
                closeButtonText.text = "KIINNI";
                break;
        }

        Hide();
    }

    private void GameLobby_OnQuickJoinFailed(object sender, System.EventArgs e)
    {
        switch (LanguageChoose.Instance.currentLanguage)
        {
            case LanguageChoose.Language.PL:
                ShowMessage("NIE ZNALEZIONO LOBBY DO LOSOWEGO ŁĄCZENIA");
                break;
            case LanguageChoose.Language.ENG:
                ShowMessage("NO LOBBIES FOUND FOR RANDOM JOIN");
                break;
            case LanguageChoose.Language.DK:
                ShowMessage("LOBBY IKKE FUNDET TIL TILFÆLDIG KAMP");
                break;
            case LanguageChoose.Language.FIN:
                ShowMessage("AULAA EI LÖYDYNYT SATUNNUSOMAISILLE");
                break;
        }
    }

    private void GameLobby_OnJoinFailed(object sender, System.EventArgs e)
    {
        switch (LanguageChoose.Instance.currentLanguage)
        {
            case LanguageChoose.Language.PL:
                ShowMessage("NIEUDANE DOŁĄCZANIE DO LOBBY");
                break;
            case LanguageChoose.Language.ENG:
                ShowMessage("FAILED TO JOIN LOBBY");
                break;
            case LanguageChoose.Language.DK:
                ShowMessage("KUNNE IKKE DELTAGE I LOBBYEN");
                break;
            case LanguageChoose.Language.FIN:
                ShowMessage("AULAN LIITTYMINEN Epäonnistunut");
                break;
        }
    }

    private void GameLobby_OnJoinStarted(object sender, System.EventArgs e)
    {
        switch (LanguageChoose.Instance.currentLanguage)
        {
            case LanguageChoose.Language.PL:
                ShowMessage("DOŁĄCZANIE DO LOBBY...");
                break;
            case LanguageChoose.Language.ENG:
                ShowMessage("Joining Lobby...");
                break;
            case LanguageChoose.Language.DK:
                ShowMessage("TILSLUTTER LOBBEN...");
                break;
            case LanguageChoose.Language.FIN:
                ShowMessage("LIITTYY LOBBIN...");
                break;
        }
    }

    private void GameLobby_OnCreateLobbyFailed(object sender, System.EventArgs e)
    {
        switch (LanguageChoose.Instance.currentLanguage)
        {
            case LanguageChoose.Language.PL:
                ShowMessage("NIEUDANE TWORZENIE LOBBY");
                break;
            case LanguageChoose.Language.ENG:
                ShowMessage("FAILED TO CREATE LOBBY");
                break;
            case LanguageChoose.Language.DK:
                ShowMessage("Lobbyen kunne ikke oprettes");
                break;
            case LanguageChoose.Language.FIN:
                ShowMessage("Aulan luominen epäonnistui");
                break;
        }
    }

    private void GameLobby_OnCreateLobbyStarted(object sender, System.EventArgs e)
    {
        switch (LanguageChoose.Instance.currentLanguage)
        {
            case LanguageChoose.Language.PL:
                ShowMessage("Tworzenie lobby...");
                break;
            case LanguageChoose.Language.ENG:
                ShowMessage("Creating Lobby...");
                break;
            case LanguageChoose.Language.DK:
                ShowMessage("Opretter lobby...");
                break;
            case LanguageChoose.Language.FIN:
                ShowMessage("Luodaan aulaa...");
                break;
        }
    }

    private void GameManager_OnFailedToJoinGame(object sender, System.EventArgs e)
    {
        if(NetworkManager.Singleton.DisconnectReason == "")
        {
            switch (LanguageChoose.Instance.currentLanguage)
            {
                case LanguageChoose.Language.PL:
                    ShowMessage("Nieudane polączenie");
                    break;
                case LanguageChoose.Language.ENG:
                    ShowMessage("Failed to connect");
                    break;
                case LanguageChoose.Language.DK:
                    ShowMessage("Kunnede ikke oprette forbindelse");
                    break;
                case LanguageChoose.Language.FIN:
                    ShowMessage("Yhdistäminen epäonnistui");
                    break;
            }
        }
        else
        {
            ShowMessage(NetworkManager.Singleton.DisconnectReason);
        }
    }

    private void ShowMessage(string message)
    {
        Show();
        messageText.text = message;
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
        GameManagerMultiplayer.Instance.OnFailedToJoinGame -= GameManager_OnFailedToJoinGame;
        GameLobby.Instance.OnCreateLobbyFailed -= GameLobby_OnCreateLobbyFailed;
        GameLobby.Instance.OnCreateLobbyStarted -= GameLobby_OnCreateLobbyStarted;
        GameLobby.Instance.OnJoinStarted -= GameLobby_OnJoinStarted;
        GameLobby.Instance.OnJoinFailed -= GameLobby_OnJoinFailed;
        GameLobby.Instance.OnQuickJoinFailed -= GameLobby_OnQuickJoinFailed;

    }
}
