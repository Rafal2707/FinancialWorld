using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private Image timerImage;


    private void Update()
    {
        if (GameManager.Instance.IsGamePlaying())
        {
            timerImage.fillAmount = 1 - GameManager.Instance.GetGamePlayingTimerNormalized();

        }
    }
}
