using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.VFX;

public class CentralBank : NetworkBehaviour
{
    public event EventHandler OnScrollCorrect;
    public event EventHandler OnScrollIncorrect;

    [SerializeField] ScoreUI scoreUI;
    private ActivityScroll lastActivityScrollInside;
    [SerializeField] private VisualEffect fireworksLeft;
    [SerializeField] private VisualEffect fireworksRight;

    [SerializeField] private Transform dropArea;
    private bool executed = false;

    private void Start()
    {
        fireworksLeft.Stop();
        fireworksRight.Stop();
    }


    private void OnTriggerStay(Collider other)
    {
        
        if (!executed && other.TryGetComponent(out IScrollParent activityScrollParent))
        {
            Debug.Log("PLayer" + other.GetComponent<NetworkObject>().GetInstanceID());
            activityScrollParent.SetIsInDroppingArea(true);

            if (activityScrollParent.GetActivityScroll() != null)
            {
                lastActivityScrollInside = activityScrollParent.GetActivityScroll();
                Debug.Log(lastActivityScrollInside.GetComponent<ActivityScroll>().GetDescription());

            }


            if (lastActivityScrollInside != null && lastActivityScrollInside.IsDropped())
            {
                executed = true;

                if (lastActivityScrollInside.IsCentralBankActivity())
                {
                                 
                    CorrectActivityScrollServerRpc();

                }
                else if (!lastActivityScrollInside.IsCentralBankActivity())
                {
                    OnScrollIncorrect?.Invoke(this, EventArgs.Empty);
                }

                DestroyActivityScroll(lastActivityScrollInside);
            }
        }
    }

    [ServerRpc(RequireOwnership =false)]
    public void CorrectActivityScrollServerRpc()
    {
        CorrectActivityScrollClientRpc();
    }

    [ClientRpc]
    public void CorrectActivityScrollClientRpc()
    {
        OnScrollCorrect?.Invoke(this, EventArgs.Empty);
        fireworksLeft.Play();
        fireworksRight.Play();
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




    private void OnTriggerExit(Collider other)
    {
        executed = false;

        if (other.TryGetComponent(out Player player))
        {
            lastActivityScrollInside = null;
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
