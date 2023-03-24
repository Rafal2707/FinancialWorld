using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MainMenuCleanUp : MonoBehaviour
{

    private void Awake()
    {
        if (NetworkManager.Singleton != null)
        {
            Destroy(NetworkManager.Singleton.gameObject);
        }

        if (GameManagerMultiplayer.Instance != null)
        {
            Destroy(GameManagerMultiplayer.Instance.gameObject);
        }

        if (GameLobby.Instance != null)
        {
            Destroy(GameLobby.Instance.gameObject);
        }
        if(LanguageChoose.Instance != null)
        {
            Destroy(LanguageChoose.Instance.gameObject);
        }

        if(MenuMusicMenager.Instance != null)
        {
            Destroy(MenuMusicMenager.Instance.gameObject);
        }
    }
}
