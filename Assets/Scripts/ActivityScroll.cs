using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityScroll : MonoBehaviour
{

    public static event EventHandler OnAnyPickUpScroll;
    public static event EventHandler OnAnyDropScroll;

    public static void ResetStaticData()
    {
        OnAnyDropScroll= null;
        OnAnyPickUpScroll= null;
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


    public void Interact(Player player)
    {
        
        if(player.GetCurrentActivityScroll() == null) 
        {
            PickUpScroll(player);
        }
        else if (player.GetCurrentActivityScroll() != null)
        {
            DropScroll(player);
        }
    }
    public void MoveTo(Vector3 position)
    {
        transform.position = new Vector3(position.x, 0.5f, position.z);
    }

    private void PickUpScroll(Player player)
    {
        OnAnyPickUpScroll?.Invoke(this, EventArgs.Empty);
        transform.parent = player.GetScrollHoldPoint();
        transform.position = player.GetScrollHoldPoint().position;
        transform.rotation = player.GetScrollHoldPoint().rotation;
        player.SetCurrentActivityScroll(this);
        player.SetIsHoldingScroll(true);
        scrollUI.HideEKeyUI();
        fliesParticles.Stop();
        isDropped = false;
    }

    public void DropScroll(Player player)
    {
        if(player.GetCurrentActivityScroll() == this)
        {
            OnAnyDropScroll?.Invoke(this, EventArgs.Empty);
            player.SetCurrentActivityScroll(null);
            player.SetIsHoldingScroll(false);

            transform.parent = null;
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);

            if (!player.IsInDroppingArea())
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
