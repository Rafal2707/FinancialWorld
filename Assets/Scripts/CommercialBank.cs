using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.VFX;

public class CommercialBank : NetworkBehaviour
{
    private ActivityScroll lastActivityScrollInside;

    public event EventHandler OnScrollCorrect;
    public event EventHandler OnScrollIncorrect;


    [SerializeField] private VisualEffect fireworksLeft;
    [SerializeField] private VisualEffect fireworksRight;

    [SerializeField] private Transform dropArea;



    [SerializeField] private GameObject commercialBankLabelENG;
    [SerializeField] private GameObject commercialBankLabelENGBBlack;

    [SerializeField] private GameObject commercialBankLabelPL;
    [SerializeField] private GameObject commercialBankLabelPLBBlack;

    [SerializeField] private GameObject commercialBankLabelDK;
    [SerializeField] private GameObject commercialBankLabelDKBBlack;

    [SerializeField] private GameObject commercialBankLabelFIN;
    [SerializeField] private GameObject commercialBankLabelFINBBlack;


    private void Start()
    {
        fireworksLeft.Stop();
        fireworksRight.Stop();

        ActivityScroll.OnCommercialBankCorrectScroll += ActivityScroll_OnCorrectScroll;
        ActivityScroll.OnCommercialBankWrongScroll += ActivityScroll_OnWrongScroll;


        switch (LanguageChoose.Instance.GetCurrentLanguage())
        {
            case LanguageChoose.Language.ENG:
                commercialBankLabelENG.SetActive(true);
                commercialBankLabelENGBBlack.SetActive(true);
                break;
            case LanguageChoose.Language.PL:
                commercialBankLabelPL.SetActive(true);
                commercialBankLabelPLBBlack.SetActive(true);
                break;
            case LanguageChoose.Language.DK:
                commercialBankLabelDK.SetActive(true);
                commercialBankLabelDKBBlack.SetActive(true);
                break;
            case LanguageChoose.Language.FIN:
                commercialBankLabelFIN.SetActive(true);
                commercialBankLabelFINBBlack.SetActive(true);
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
        if (other.gameObject.TryGetComponent(out Player player))
        {
            player.SetIsInDroppingArea(true);
            player.SetIsInCommercialBankDroppingArea(true);
            player.isInCommercialBankDroppingArea = true;
        }
    }

    [ServerRpc(RequireOwnership = false)]
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
            player.SetIsInCommercialBankDroppingArea(false);
            fireworksLeft.Stop();
            fireworksRight.Stop();
        }
    }

}