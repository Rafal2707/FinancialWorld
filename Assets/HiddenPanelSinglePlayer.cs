using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenPanelSinglePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!GameManagerMultiplayer.playMultiplayer)
        {
            gameObject.SetActive(true);
        }
        else gameObject.SetActive(false);
    }
}
