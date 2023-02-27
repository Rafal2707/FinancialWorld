using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class CentralBank : MonoBehaviour
{
    [SerializeField] ScoreUI scoreUI;
    private ActivityScroll lastActivityScrollInside;
    [SerializeField] private VisualEffect fireworksLeft;
    [SerializeField] private VisualEffect fireworksRight;


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

            if (lastActivityScrollInside != null && lastActivityScrollInside.IsDropped())
            {
                if (lastActivityScrollInside.IsCentralBankActivity())
                {
                    Debug.Log("Aktywnosc z banku centralnego");
                    scoreUI.IncreaseScore();
                    fireworksLeft.Play();
                    fireworksRight.Play();
                
                    
                }
                else if (!lastActivityScrollInside.IsCentralBankActivity())
                {
                    Debug.Log("Aktywnosc z banku komercyjnego");
                    scoreUI.DecreaseScore();
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
}
