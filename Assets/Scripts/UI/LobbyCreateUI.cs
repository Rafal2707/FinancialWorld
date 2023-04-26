using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyCreateUI : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private Button createPublicButton;
    [SerializeField] private Button createPrivateButton;
    [SerializeField] private TMP_InputField lobbyNameInputField;

    [SerializeField] private TextMeshProUGUI createPublicButtonText;
    [SerializeField] private TextMeshProUGUI createPrivateButtonText;
    [SerializeField] private TextMeshProUGUI createLobbyText;

    //From LobbyUI
    [SerializeField] private Button createLobbyButton;
    [SerializeField] private Button quickJoinButton;
    [SerializeField] private Button joinByCodeButton;
    

    private void Awake()
    {
        createPublicButton.onClick.AddListener(() => {
            GameLobby.Instance.CreateLobby(lobbyNameInputField.text, false);
        });
        createPrivateButton.onClick.AddListener(() => {
            GameLobby.Instance.CreateLobby(lobbyNameInputField.text, true);
        });
        closeButton.onClick.AddListener(() => {
            Hide();
        });
    }

    private void Start()
    {

        switch (LanguageChoose.Instance.currentLanguage)
        {
            case LanguageChoose.Language.PL:
                createPublicButtonText.text = "PUBLICZNE";
                createPrivateButtonText.text = "PRYWATNE";
                createLobbyText.text = "STWÓRZ LOBBY";
                break;
            case LanguageChoose.Language.ENG:
                createPublicButtonText.text = "PUBLIC";
                createPrivateButtonText.text = "PRIVATE";
                createLobbyText.text = "CREATE LOBBY";
                break;
            case LanguageChoose.Language.DK:
                createPublicButtonText.text = "OFFENTLIG";
                createPrivateButtonText.text = "PRIVAT";
                createLobbyText.text = "OPRET LOBBY";
                break;
            case LanguageChoose.Language.FIN:
                createPublicButtonText.text = "JULKINEN";
                createPrivateButtonText.text = "PRIVATE";
                createLobbyText.text = "LUO AULA";
                break;
        }
        Hide();
    }

    private void Hide()
    {
        

        createLobbyButton.gameObject.SetActive(true);
        quickJoinButton.gameObject.SetActive(true);
        joinByCodeButton.gameObject.SetActive(true);

        createLobbyButton.Select();
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        createLobbyButton.gameObject.SetActive(false);
        quickJoinButton.gameObject.SetActive(false);
        joinByCodeButton.gameObject.SetActive(false);
        createPublicButton.Select();
    }
}
