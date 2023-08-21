using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.VFX;

public class CentralBank : NetworkBehaviour
{
    public event EventHandler OnScrollCorrect;
    public event EventHandler OnScrollIncorrect;

    
    private ActivityScroll lastActivityScrollInside;
    [SerializeField] private VisualEffect fireworksLeft;
    [SerializeField] private VisualEffect fireworksRight;

    [SerializeField] private Transform dropArea;

    [SerializeField] private GameObject centralBankLabelENG;
    [SerializeField] private GameObject centralBankLabelENGBBlack;

    [SerializeField] private GameObject centralBankLabelPL;
    [SerializeField] private GameObject centralBankLabelPLBBlack;

    [SerializeField] private GameObject centralBankLabelDK;
    [SerializeField] private GameObject centralBankLabelDKBBlack;

    [SerializeField] private GameObject centralBankLabelFIN;
    [SerializeField] private GameObject centralBankLabelFINBBlack;

    private void Start()
    {
        fireworksLeft.Stop();
        fireworksRight.Stop();

        ActivityScroll.OnCentralBankCorrectScroll += ActivityScroll_OnCorrectScroll;
        ActivityScroll.OnCentralBankWrongScroll += ActivityScroll_OnWrongScroll;



        switch (LanguageChoose.Instance.GetCurrentLanguage())
        {
            case LanguageChoose.Language.ENG:
                centralBankLabelENG.SetActive(true);
                centralBankLabelENGBBlack.SetActive(true);
                break;
            case LanguageChoose.Language.PL:
                centralBankLabelPL.SetActive(true);
                centralBankLabelPLBBlack.SetActive(true);
                break;
            case LanguageChoose.Language.DK:
                centralBankLabelDK.SetActive(true);
                centralBankLabelDKBBlack.SetActive(true);
                break;
            case LanguageChoose.Language.FIN:
                centralBankLabelFIN.SetActive(true);
                centralBankLabelFINBBlack.SetActive(true);
                break;
        }

    }

    private void ActivityScroll_OnWrongScroll(object sender, EventArgs e)
    {
        ShowVisualWrongServerRpc();
    }

    private void ActivityScroll_OnCorrectScroll(object sender, EventArgs e)
    {
        ShowVisualCorrectServerRpc();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Player player))
        {
            player.SetIsInDroppingArea(true);
            player.SetIsInCentralBankDroppingArea(true);
            player.isInCentralBankDroppingArea= true;
        }
    }


    [ServerRpc(RequireOwnership =false)]
    public void ShowVisualCorrectServerRpc()
    {
        ShowVisualCorrectClientRpc();
    }

    [ClientRpc]
    public void ShowVisualCorrectClientRpc()
    {
        fireworksLeft.Play();
        fireworksRight.Play();
        OnScrollCorrect?.Invoke(this, EventArgs.Empty);
    }

    [ServerRpc(RequireOwnership = false)]
    public void ShowVisualWrongServerRpc()
    {
        ShowVisualWrongClientRpc();
    }

    [ClientRpc]
    public void ShowVisualWrongClientRpc()
    {
        OnScrollIncorrect?.Invoke(this, EventArgs.Empty);
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            lastActivityScrollInside = null;
            player.SetIsInDroppingArea(false);
            player.SetIsInCentralBankDroppingArea(false);
            fireworksLeft.Stop();
            fireworksRight.Stop();
        }
    }







 



}
