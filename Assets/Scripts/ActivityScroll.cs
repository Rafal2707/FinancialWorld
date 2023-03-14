using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ActivityScroll : NetworkBehaviour
{

    public static event EventHandler OnAnyPickUpScroll;
    public static event EventHandler OnAnyDropScroll;

    public FollowTransform followTransform;

    private void Awake()
    {
        followTransform = GetComponent<FollowTransform>();
    }
    public static void ResetStaticData()
    {
        OnAnyDropScroll = null;
        OnAnyPickUpScroll = null;
    }


    private bool isCentralBankActivity;
    private string description;
    private bool isDropped = true;
    bool isQuitting;
    [SerializeField] private Transform scrollHoldPoint;
    [SerializeField] private LayerMask scrollLayerMask;
    [SerializeField] ScrollUI scrollUI;
    [SerializeField] private ParticleSystem fliesParticles;
    [SerializeField] private ParticleSystem dropParticles;
    [SerializeField] private GameObject explosionParticles;

    private IScrollParent activityScrollParent;





    public void Interact(IScrollParent activityScrollParent)
    {
        if (!activityScrollParent.HasActivityScroll()) 
        {
            PickUpScroll(activityScrollParent);
        }
        else if (activityScrollParent.HasActivityScroll())
        {
            DropScroll(activityScrollParent);
        }
    }
    public void MoveTo(Vector3 position)
    {
        transform.position = new Vector3(position.x, 0.5f, position.z);
    }

    private void PickUpScroll(IScrollParent activityScrollParent)
    {
        OnAnyPickUpScroll?.Invoke(this, EventArgs.Empty);
        SetScrollParentServerRpc(activityScrollParent.GetNetworkObject());
        followTransform.SetTargetTransform(activityScrollParent.GetScrollFollowTransform());
        activityScrollParent.SetActivityScroll(this);
        scrollUI.HideEKeyUI();
        fliesParticles.Stop();
        isDropped = false;
    }

    [ServerRpc(RequireOwnership = false)]
    private void SetScrollParentServerRpc(NetworkObjectReference activityScrollParentNetworkObjectReference)
    {
        SetScrollObjectParentClientRpc(activityScrollParentNetworkObjectReference);
    }

    [ClientRpc]
    private void SetScrollObjectParentClientRpc(NetworkObjectReference activityScrollParentNetworkObjectReference)
    {
        activityScrollParentNetworkObjectReference.TryGet(out NetworkObject activityScrollParentNetworkObject);
        IScrollParent activityScrollParent = activityScrollParentNetworkObject.GetComponent<IScrollParent>();

        if(this.activityScrollParent != null)
        {
            this.activityScrollParent.ClearActivityScroll();
        }

        this.activityScrollParent = activityScrollParent;


        if (activityScrollParent.HasActivityScroll())
        {
            Debug.Log("IScrollParent already has a Scroll activity");
        }

        activityScrollParent.SetActivityScroll(this);

        followTransform.SetTargetTransform(activityScrollParent.GetScrollFollowTransform());
    }





    public void DropScroll(IScrollParent activityScrollParent)
    {
        Debug.Log("Drop");
        
        if (activityScrollParent.GetActivityScroll() == this)
        {
            OnAnyDropScroll?.Invoke(this, EventArgs.Empty);
            activityScrollParent.ClearActivityScroll();

            

            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
            followTransform.SetTargetTransform(transform);

            if (!activityScrollParent.IsInDroppingArea())
            {
                fliesParticles.Play();
                dropParticles.Play();
            }

            isDropped = true;

        }
    }
    public bool IsDropped()
    {
        return isDropped;
    }


    public ActivityScroll GetScroll()
    {
        return this;
    }

    public bool IsCentralBankActivity()
    {
        return isCentralBankActivity;
    }


    
    public void AssignCentralBankActivity(bool isCentralBankActivity)
    {
        this.isCentralBankActivity = isCentralBankActivity;
    }


    public void SetDescription(string description)
    {
        this.description = description;
    }

    public string GetDescription() 
    {
        return description;
    }

    [ClientRpc]
    public void SetDescriptionClientRpc(string description)
    {
        SetDescription(description);
    }

    [ClientRpc]
    public void AssignCentralBankActivityClientRpc(bool isCentralBankActivity)
    {
        AssignCentralBankActivity(isCentralBankActivity);
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (!player.IsHoldingScroll())
            {
                scrollUI.ShowEKeyUI();
                scrollUI.ShowDescriptionUI();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            scrollUI.HideEKeyUI();
            scrollUI.HideDescriptionUI();
        }
    }


    //bool "IsQuitting" is set to overcome the issue with uncleared gameObjects (Puffs) after destroy is called
    ////private void OnDestroy()
    ////{
    ////    if (!isQuitting)
    ////    {
    ////        GameObject dropPuffVFX = Instantiate(explosionParticles, transform.position, Quaternion.identity);
    ////        Destroy(dropPuffVFX, 0.75f);
    ////    }

    //}

   private void OnApplicationQuit()
    {
        isQuitting = true;
    }
}
