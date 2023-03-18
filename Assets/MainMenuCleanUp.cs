using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MainMenuCleanUp : MonoBehaviour
{
    private void Awake()
    {
        if(NetworkManager.Singleton.gameObject != null)
        {
            Destroy(NetworkManager.Singleton.gameObject);
        }

        if(GameManagerMultiplayer.Instance != null)
        {
            Destroy(GameManagerMultiplayer.Instance.gameObject);
        }
    }
}
