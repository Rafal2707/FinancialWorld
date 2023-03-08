using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class CommercialBank : MonoBehaviour
{
    [SerializeField] ScoreUI scoreUI;
    private ActivityScroll lastActivityScrollInside;

    public event EventHandler OnScrollCorrect;
    public event EventHandler OnScrollIncorrect;


    [SerializeField] private VisualEffect fireworksLeft;
    [SerializeField] private VisualEffect fireworksRight;

    [SerializeField] private Transform dropArea;


    private void Start()
    {
        fireworksLeft.Stop();
        fireworksRight.Stop();
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.SetIsInDroppingArea(true);
            if (player.GetCurrentActivityScroll() != null)
            {
                lastActivityScrollInside = player.GetCurrentActivityScroll();
            }

            if(lastActivityScrollInside != null && lastActivityScrollInside.IsDropped())
            {
                if (!lastActivityScrollInside.IsCentralBankActivity())
                {
                    Debug.Log("Aktywnosc z banku komercyjnego");
                    OnScrollCorrect?.Invoke(this, EventArgs.Empty);
                    fireworksLeft.Play();
                    fireworksRight.Play();
                }
                else if (lastActivityScrollInside.IsCentralBankActivity())
                {
                    Debug.Log("Aktywnosc z banku centralnego");
                    OnScrollIncorrect?.Invoke(this, EventArgs.Empty);
                }
                Destroy(lastActivityScrollInside.gameObject);
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.SetIsInDroppingArea(false);
            fireworksLeft.Stop();
            fireworksRight.Stop();
        }
    }

    public Vector3 GetDropAreaPosition()
    {
        return dropArea.position;
    }
}