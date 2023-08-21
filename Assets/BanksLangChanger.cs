using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BanksLangChanger : MonoBehaviour
{

    [SerializeField] private GameObject commercialBankLabelPLPrefab;
    [SerializeField] private GameObject centralBankLabelPLPrefab;

    [SerializeField] private GameObject commercialBankLabelDKPrefab;
    [SerializeField] private GameObject centralBankLabelDKPrefab;

    [SerializeField] private GameObject commercialBankLabelENGPrefab;
    [SerializeField] private GameObject centralBankLabelENGPrefab;

    [SerializeField] private GameObject commercialBankLabelFINPrefab;
    [SerializeField] private GameObject centralBankLabelFINPrefab;

    private void Start()
    {
        
    }


    private void UpdateBankLabels()
    {
        if(LanguageChoose.Instance.GetCurrentLanguage() == LanguageChoose.Language.ENG)
        {


            commercialBankLabelENGPrefab.SetActive(true);
            centralBankLabelENGPrefab.SetActive(true);
        }
        else if (LanguageChoose.Instance.GetCurrentLanguage() == LanguageChoose.Language.PL)
        {
            commercialBankLabelPLPrefab.SetActive(true);
            centralBankLabelPLPrefab.SetActive(true);
        }
        else if (LanguageChoose.Instance.GetCurrentLanguage() == LanguageChoose.Language.FIN)
        {
            commercialBankLabelFINPrefab.SetActive(true);
            centralBankLabelFINPrefab.SetActive(true);
        }
        else if (LanguageChoose.Instance.GetCurrentLanguage() == LanguageChoose.Language.DK)
        {
            commercialBankLabelENGPrefab.SetActive(true);
            centralBankLabelENGPrefab.SetActive(true);
        }
    }
}
