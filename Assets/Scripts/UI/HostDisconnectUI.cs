using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class HostDisconnectUI : MonoBehaviour
{
    [SerializeField] private Button playAgainButton;
    [SerializeField] private TextMeshProUGUI hostDisconnectedText;
    [SerializeField] private TextMeshProUGUI playAgainButtonText;


    private void Awake()
    {
        playAgainButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
    }

    private void Start()
    {
        NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_OnClientDisconnectCallback;

        switch (LanguageChoose.Instance.currentLanguage)
        {
            case LanguageChoose.Language.PL:
                hostDisconnectedText.text = "HOST SIĘ ROZŁĄCZYŁ";
                playAgainButtonText.text = "ZAGRAJ PONOWNIE";
                break;
            case LanguageChoose.Language.ENG:
                hostDisconnectedText.text = "HOST HAS DISCONNECTED";
                playAgainButtonText.text = "PLAY AGAIN";
                break;
            case LanguageChoose.Language.DK:
                hostDisconnectedText.text = "VÆRTEN HAR AFBRYDET";
                playAgainButtonText.text = "SPIL IGEN";
                break;
            case LanguageChoose.Language.FIN:
                hostDisconnectedText.text = "ISÄNTÄ ON YHTEYS KATKUNUT";
                playAgainButtonText.text = "PELAA UUDESTAAN";
                break;
        }


        Hide();
    }

    private void NetworkManager_OnClientDisconnectCallback(ulong clientId)
    {
        if (clientId == NetworkManager.ServerClientId)
        {
            // Server is shutting down
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
