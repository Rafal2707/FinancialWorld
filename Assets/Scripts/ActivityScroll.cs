using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityScroll : MonoBehaviour
{

    private bool isCentralBankActivity;
    private string description;
    private bool isDropped = true;
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

    private void OnDestroy()
    {
      GameObject dropPuffVFX =  Instantiate(explosionParticles, transform.position, Quaternion.identity);
        Destroy(dropPuffVFX, 0.75f);
    }
}
