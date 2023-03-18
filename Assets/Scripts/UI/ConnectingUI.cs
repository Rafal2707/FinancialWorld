using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectingUI : MonoBehaviour
{

    private void Start()
    {
        GameManagerMultiplayer.Instance.OnTryingToJoinGame += GameManager_OnTryingToJoinGame;
        GameManagerMultiplayer.Instance.OnFailedToJoinGame += GameManager_OnFailedToJoinGame;
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
