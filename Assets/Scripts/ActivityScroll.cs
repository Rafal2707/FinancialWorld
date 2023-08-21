using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Rendering;

public class ActivityScroll : NetworkBehaviour
{

    public static event EventHandler OnAnyPickUpScroll;
    public static event EventHandler OnAnyDropScroll;


    public static event EventHandler OnCentralBankCorrectScroll;
    public static event EventHandler OnCentralBankWrongScroll;

    public static event EventHandler OnCommercialBankCorrectScroll;
    public static event EventHandler OnCommercialBankWrongScroll;

    private int number;
    public ActivityScrollSO GetActivityScrollSOFromActivityScroll(ActivityScroll activityScroll)
    {
        foreach(ActivityScrollSO activityScrollSO in Spawner.Instance.allScrollsList)
        {
            if(number == activityScrollSO.number)
            {
                return activityScrollSO;
            }
        }
        return default;
    }


    public FollowTransform followTransform;

    public ScoreUI scoreUI;

    private void Awake()
    {
        followTransform = GetComponent<FollowTransform>();
    }
    public static void ResetStaticData()
    {
        OnAnyDropScroll = null;
        OnAnyPickUpScroll = null;
        OnCentralBankCorrectScroll = null;
        OnCentralBankWrongScroll = null;
        OnCommercialBankCorrectScroll = null;
        OnCommercialBankWrongScroll = null;
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


    private void Start()
    {
        fliesParticles.gameObject.SetActive(true);
        scoreUI = FindAnyObjectByType<ScoreUI>();


        

    }


    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        //switch (LanguageChoose.Instance.GetCurrentLanguage())
        //{
        //    case LanguageChoose.Language.DK:
        //        description = GetActivityScrollSOFromActivityScroll(this).descriptionDK;
        //        break;
        //    case LanguageChoose.Language.PL:
        //        description = GetActivityScrollSOFromActivityScroll(this).descriptionPL;
        //        break;
        //    case LanguageChoose.Language.ENG:
        //        description = GetActivityScrollSOFromActivityScroll(this).descriptionENG;
        //        break;
        //    case LanguageChoose.Language.FIN:
        //        description = GetActivityScrollSOFromActivityScroll(this).descriptionFIN;
        //        break;
        //}
    }

    public void Interact(IScrollParent activityScrollParent)
    {
        if (activityScrollParent.GetNetworkObject().IsOwner && !activityScrollParent.HasActivityScroll() && isDropped) 
        {
            PickUpScroll(activityScrollParent);
        }
        else if (activityScrollParent.GetNetworkObject().IsOwner && activityScrollParent.HasActivityScroll() && !isDropped)
        {
            DropScroll(activityScrollParent);
        }
    }


    private void PickUpScroll(IScrollParent activityScrollParent)
    {

        OnAnyPickUpScroll?.Invoke(this, EventArgs.Empty);
        SetScrollParentServerRpc(activityScrollParent.GetNetworkObject());
        followTransform.SetTargetTransform(activityScrollParent.GetScrollFollowTransform());
        scrollUI.HideEKeyUI();
        fliesParticles.Stop();
    }


    public void DropScroll(IScrollParent activityScrollParent)
    {
        if (activityScrollParent.GetActivityScroll() == this)
        {
            OnAnyDropScroll?.Invoke(this, EventArgs.Empty);
            SetScrollOrphanServerRpc(activityScrollParent.GetNetworkObject());
            followTransform.SetTargetTransform(transform);

            if (!activityScrollParent.IsInDroppingArea())
            {
                fliesParticles.Play();
                dropParticles.Play();
            }


            // is In central Bank area
            if (activityScrollParent.IsInCentralBankDroppingArea() && isCentralBankActivity)
            {
                scoreUI.IncreaseServerRpc();
                scoreUI.IncreaseTriesServerRpc();
                DestroyActivityScroll(this);
                OnCentralBankCorrectScroll?.Invoke(this, EventArgs.Empty);
            }
            else if (activityScrollParent.IsInCentralBankDroppingArea() && !isCentralBankActivity)
            {
                //scoreUI.DecreaseServerRpc();
                scoreUI.IncreaseTriesServerRpc();
                DestroyActivityScroll(this);
                OnCentralBankWrongScroll?.Invoke(this, EventArgs.Empty);

            }

            // is In Commercial Bank area
            else if (activityScrollParent.IsInCommercialBankDroppingArea() && !isCentralBankActivity)
            {
                scoreUI.IncreaseServerRpc();
                scoreUI.IncreaseTriesServerRpc();
                DestroyActivityScroll(this);
                OnCommercialBankCorrectScroll?.Invoke(this, EventArgs.Empty);

            }
            else if (activityScrollParent.IsInCommercialBankDroppingArea() && isCentralBankActivity)
            {
                //scoreUI.DecreaseServerRpc();
                scoreUI.IncreaseTriesServerRpc();
                DestroyActivityScroll(this);
                OnCommercialBankWrongScroll?.Invoke(this, EventArgs.Empty);
            }
        }
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
        isDropped = false;
        activityScrollParent.SetActivityScroll(this);
        Debug.Log("Set Up the activityscroll");

        followTransform.SetTargetTransform(activityScrollParent.GetScrollFollowTransform());
    }


    [ServerRpc(RequireOwnership = false)]
    private void SetScrollOrphanServerRpc(NetworkObjectReference activityScrollParentNetworkObjectReference)
    {
        SetScrollObjectOrphanClientRpc(activityScrollParentNetworkObjectReference);
    }

    [ClientRpc]
    private void SetScrollObjectOrphanClientRpc(NetworkObjectReference activityScrollParentNetworkObjectReference)
    {
        activityScrollParentNetworkObjectReference.TryGet(out NetworkObject activityScrollParentNetworkObject);
        IScrollParent activityScrollParent = activityScrollParentNetworkObject.GetComponent<IScrollParent>();

        if(this.activityScrollParent != null)
        {
            this.activityScrollParent.ClearActivityScroll();
        }

        activityScrollParent.SetActivityScroll(null);
        Debug.Log("dropped and set actiivty scroll to null");
        followTransform.SetTargetTransform(null);
        isDropped = true;
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);

    }



    public void DestroyActivityScroll(ActivityScroll activityScroll)
    {
        if (activityScroll.NetworkObject.IsSpawned)
        {
            Debug.Log("Spawned");
            DestroyActivityScrollServerRpc(activityScroll.NetworkObject);
        }
        else
        {
            Debug.Log("NOT Spawned");

            // handle the case where the object is not spawned
        }
    }
    [ServerRpc(RequireOwnership = false)]
    private void DestroyActivityScrollServerRpc(NetworkObjectReference activityScrollObjectReference)
    {
        activityScrollObjectReference.TryGet(out NetworkObject activityScrollNetworkObject);


        if (activityScrollNetworkObject == null)
        {
            //this object is already destroyed
            return;
        }
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



    public void AssignNumber(int number)
    {
        this.number = number;
    }


    [ClientRpc]
    public void AssignNumberClientRpc(int number)
    {
        AssignNumber(number);
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


    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (isDropped)
            {
                scrollUI.ShowDescriptionUI();
                scrollUI.ShowEKeyUI();
            }
            else if(player.HasActivityScroll() && !isDropped)
            {
                scrollUI.ShowDescriptionUI();
                scrollUI.HideEKeyUI();
            }
            else
            {
                scrollUI.HideDescriptionUI();
                scrollUI.HideEKeyUI();
            }
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player) && isDropped)
        {
            scrollUI.HideEKeyUI();
            scrollUI.HideDescriptionUI();
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }


    public void ClearActivityScrollOnParent()
    {
        activityScrollParent.ClearActivityScroll();
    }

    public ScrollUI GetScrollUI()
    {
        return scrollUI;
    }

}
