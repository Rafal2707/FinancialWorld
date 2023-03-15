using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.VFX;

public class CommercialBank : NetworkBehaviour
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
        if (other.TryGetComponent(out IScrollParent activityScrollParent))
        {
            activityScrollParent.SetIsInDroppingArea(true);
            if (activityScrollParent.GetActivityScroll() != null)
            {
                lastActivityScrollInside = activityScrollParent.GetActivityScroll();
            }

            if(lastActivityScrollInside != null && lastActivityScrollInside.IsDropped())
            {
                if (!lastActivityScrollInside.IsCentralBankActivity())
                {
                    ScrollCorrectlyAssignedServerRpc();
                }
                else if (lastActivityScrollInside.IsCentralBankActivity())
                {
                    ScrollIncorrectlyAssignedServerRpc();
                }
                DestroyActivityScroll(lastActivityScrollInside);
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void ScrollCorrectlyAssignedServerRpc()
    {
        ScrollCorrectlyAssignedClientRpc();
    }

    [ClientRpc]
    public void ScrollCorrectlyAssignedClientRpc()
    {
        OnScrollCorrect?.Invoke(this, EventArgs.Empty);

        fireworksLeft.Play();
        fireworksRight.Play();
    }

    [ServerRpc(RequireOwnership = false)]
    public void ScrollIncorrectlyAssignedServerRpc()
    {
        ScrollIncorrectlyAssignedClientRpc();
    }

    [ClientRpc]
    public void ScrollIncorrectlyAssignedClientRpc()
    {
        OnScrollIncorrect?.Invoke(this, EventArgs.Empty);
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




    public void DestroyActivityScroll(ActivityScroll activityScroll)
    {
        DestroyActivityScrollServerRpc(activityScroll.NetworkObject);
    }
    [ServerRpc(RequireOwnership = false)]
    private void DestroyActivityScrollServerRpc(NetworkObjectReference activityScrollObjectReference)
    {
        activityScrollObjectReference.TryGet(out NetworkObject activityScrollNetworkObject);
        ActivityScroll activityScroll = activityScrollNetworkObject.GetComponent<ActivityScroll>();

        ClearActivityScrollOnParentClientRpc(activityScrollObjectReference);
        activityScroll.DestroySelf();
    }

    [ClientRpc]
    public void ClearActivityScrollOnParentClientRpc(NetworkObjectReference activityScrollObjectReference)
    {
        activityScrollObjectReference.TryGet(out NetworkObject activityScrollNetworkObject);
        ActivityScroll activityScroll = activityScrollNetworkObject.GetComponent<ActivityScroll>();

        activityScroll.ClearActivityScrollOnParent();
    }
}