using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralBank : MonoBehaviour
{
    [SerializeField] ScoreUI scoreUI;
    private ActivityScroll lastActivityScrollInside;


    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
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

}
