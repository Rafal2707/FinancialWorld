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


    //From lobbyUI
    [SerializeField] private Button createLobbyButton;

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
                closeButtonText.text = "LUK";
                break;
            case LanguageChoose.Language.FIN:
                closeButtonText.text = "SULJE";
                break;
        }

        Hide();
    }

    private void GameLobby_OnQuickJoinFailed(object sender, System.EventArgs e)
    {
        switch (LanguageChoose.Instance.currentLanguage)
        {
            case LanguageChoose.Language.PL:
                ShowMessage("NIE ZNALEZIONO LOBBY DLA LOSOWEGO ŁĄCZENIA");
                break;
            case LanguageChoose.Language.ENG:
                ShowMessage("NO LOBBIES FOUND FOR RANDOM JOIN");
                break;
            case LanguageChoose.Language.DK:
                ShowMessage("INGEN LOBBYER FUNDET FOR TILFÆLDIG TILMELDING");
                break;
            case LanguageChoose.Language.FIN:
                ShowMessage("ETSIMÄSI KALTAISTA AULAA EI LÖYTÄNYT");
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
                ShowMessage("UNDLOD AT DELTAGE I LOBBYEN");
                break;
            case LanguageChoose.Language.FIN:
                ShowMessage("AULAAN LIITTYMINEN EPÄONNISTUI");
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
                ShowMessage("JOINING LOBBY...");
                break;
            case LanguageChoose.Language.DK:
                ShowMessage("DELTAGELSE I LOBBY...");
                break;
            case LanguageChoose.Language.FIN:
                ShowMessage("LIITTYY AULAAN...");
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
                ShowMessage("KUNNE IKKE OPRETTE LOBBY");
                break;
            case LanguageChoose.Language.FIN:
                ShowMessage("AULAN LUOMINEN EPÄONNISTUI");
                break;
        }
    }

    private void GameLobby_OnCreateLobbyStarted(object sender, System.EventArgs e)
    {
        switch (LanguageChoose.Instance.currentLanguage)
        {
            case LanguageChoose.Language.PL:
                ShowMessage("TWORZENIE LOBBY...");
                break;
            case LanguageChoose.Language.ENG:
                ShowMessage("CREATING LOBBY...");
                break;
            case LanguageChoose.Language.DK:
                ShowMessage("OPRETTELSE AF LOBBY...");
                break;
            case LanguageChoose.Language.FIN:
                ShowMessage("LUODAAN AULAA...");
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
                    ShowMessage("NIEUDANE POŁĄCZENIE");
                    break;
                case LanguageChoose.Language.ENG:
                    ShowMessage("FAILED TO CONNECT");
                    break;
                case LanguageChoose.Language.DK:
                    ShowMessage("FORBINDELSE MISLYKKEDES");
                    break;
                case LanguageChoose.Language.FIN:
                    ShowMessage("YHDISTÄMINEN EPÄONNISTUI");
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
        closeButton.Select();
    }

    private void Hide()
    {
        createLobbyButton.Select();
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
