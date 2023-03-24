using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button readyButton;
    [SerializeField] private TextMeshProUGUI lobbyNameText;
    [SerializeField] private TextMeshProUGUI lobbyCodeText;
    [SerializeField] private TextMeshProUGUI mainMenuButtonText;
    [SerializeField] private TextMeshProUGUI readyButtonText;


    private void Awake()
    {
        mainMenuButton.onClick.AddListener(() =>
        {
            GameLobby.Instance.LeaveLobby();
            NetworkManager.Singleton.Shutdown();
            Loader.Load(Loader.Scene.MainMenuScene);
        });

        readyButton.onClick.AddListener(() =>
        {
            CharacterSelectReady.Instance.SetPlayerReady();
        });
    }


    private void Start()
    {
       Lobby lobby =  GameLobby.Instance.GetLobby();

        lobbyNameText.text = lobby.Name;
        lobbyCodeText.text = lobby.LobbyCode;


        switch (LanguageChoose.Instance.currentLanguage)
        {
            case LanguageChoose.Language.PL:
                mainMenuButtonText.text = "GŁÓWNE MENU";
                readyButtonText.text = "START";
                break;
            case LanguageChoose.Language.ENG:
                mainMenuButtonText.text = "MAIN MENU";
                readyButtonText.text = "START";
                break;
            case LanguageChoose.Language.DK:
                mainMenuButtonText.text = "HOVEDMENU";
                readyButtonText.text = "START";
                break;
            case LanguageChoose.Language.FIN:
                mainMenuButtonText.text = "PÄÄVALIKKO";
                readyButtonText.text = "START";
                break;
        }
    }
}
