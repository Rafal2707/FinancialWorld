using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button createLobbyButton;
    [SerializeField] private Button quickJoinButton;
    [SerializeField] private Button joinCodeButton;
    [SerializeField] private TMP_InputField joinCodeInputField;
    [SerializeField] private TMP_InputField playerNameInputField;
    [SerializeField] private LobbyCreateUI lobbyCreateUI;
    [SerializeField] private Transform lobbyContainer;
    [SerializeField] private Transform lobbyTemplate;


    [SerializeField] private TextMeshProUGUI mainMenuButtonText;
    [SerializeField] private TextMeshProUGUI createLobbyButtonText;
    [SerializeField] private TextMeshProUGUI quickJoinButtonText;
    [SerializeField] private TextMeshProUGUI joinCodeButtonText;

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(() => {
            GameLobby.Instance.LeaveLobby();
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        createLobbyButton.onClick.AddListener(() => {
            lobbyCreateUI.Show();
        });
        quickJoinButton.onClick.AddListener(() => {
            GameLobby.Instance.QuickJoin();
        });
        joinCodeButton.onClick.AddListener(() =>
        {
            GameLobby.Instance.JoinWithCode(joinCodeInputField.text);
        });
        
        lobbyTemplate.gameObject.SetActive(false);

    }


    private void Start()
    {
        playerNameInputField.text = GameManagerMultiplayer.Instance.GetPlayerName();
        playerNameInputField.onValueChanged.AddListener((string newText) =>
        {
            GameManagerMultiplayer.Instance.SetPlayerName(newText);
        });

        GameLobby.Instance.OnLobbyListChanged += GameLobby_OnLobbyListChanged;
        UpdateLobbyList(new List<Lobby>());


        switch (LanguageChoose.Instance.currentLanguage)
        {
            case LanguageChoose.Language.PL:
                mainMenuButtonText.text = "GŁÓWNE MENU";
                createLobbyButtonText.text = "STWÓRZ LOBBY";
                quickJoinButtonText.text = "DOŁĄCZ LOSOWO";
                joinCodeButtonText.text = "DOŁĄCZ PRZEZ KOD";
                break;
            case LanguageChoose.Language.ENG:
                mainMenuButtonText.text = "MAIN MENU";
                createLobbyButtonText.text = "CREATE LOBBY";
                quickJoinButtonText.text = "QUICK JOIN";
                joinCodeButtonText.text = "JOIN BY CODE";
                break;
            case LanguageChoose.Language.DK:
                mainMenuButtonText.text = "HOVEDMENU";
                createLobbyButtonText.text = "OPRETTE LOBBY";
                quickJoinButtonText.text = "HURTIG TILMELDING";
                joinCodeButtonText.text = "TILMELDING VED CODE";
                break;
            case LanguageChoose.Language.FIN:
                mainMenuButtonText.text = "PÄÄVALIKKO";
                createLobbyButtonText.text = "LUO AULA";
                quickJoinButtonText.text = "PIKALIITY";
                joinCodeButtonText.text = "LIITY KOODIN AVULLA";
                break;
        }
    }

    private void GameLobby_OnLobbyListChanged(object sender, GameLobby.OnLobbyListChangedEventArgs e)
    {
        UpdateLobbyList(e.lobbyList);
    }

    private void UpdateLobbyList(List<Lobby> lobbyList)
    {
        foreach(Transform child in lobbyContainer)
        {
            if (child == lobbyTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach(Lobby lobby in lobbyList) 
        {
            Transform lobbyTransform = Instantiate(lobbyTemplate, lobbyContainer);
            lobbyTransform.gameObject.SetActive(true);
            lobbyTransform.GetComponent<LobbyListSingleUI>().SetLobby(lobby);
        }
    }


    private void OnDestroy()
    {
        GameLobby.Instance.OnLobbyListChanged -= GameLobby_OnLobbyListChanged;

    }

}
