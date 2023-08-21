using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScrollResultUICommercialBank : MonoBehaviour
{
    private const string POPUP = "Popup";

    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private CommercialBank commercialBank;
    [SerializeField] private Color succesColor;
    [SerializeField] private Color failedColor;
    [SerializeField] private Sprite succesSprite;
    [SerializeField] private Sprite failedSprite;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        commercialBank.OnScrollCorrect += CommercialBank_OnScrollCorrect;
        commercialBank.OnScrollIncorrect += CommercialBank_OnScrollIncorrect;




        gameObject.SetActive(false);
    }

    private void CommercialBank_OnScrollIncorrect(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        animator.SetTrigger(POPUP);
        backgroundImage.color = failedColor;
        iconImage.sprite = failedSprite;

        switch (LanguageChoose.Instance.GetCurrentLanguage())
        {
            case LanguageChoose.Language.ENG:
                messageText.text = "INCORRECT\nSCROLL";
                break;
            case LanguageChoose.Language.PL:
                messageText.text = "ZWÓJ\nNIEPRAWIDŁOWY";
                break;
            case LanguageChoose.Language.DK:
                messageText.text = "FORKERT\nAKTIVITETSRULLE";
                break;
            case LanguageChoose.Language.FIN:
                messageText.text = "VÄÄRÄ\nKÄÄRÖT";
                break;
        }

    }

    private void CommercialBank_OnScrollCorrect(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        animator.SetTrigger(POPUP);
        backgroundImage.color = succesColor;
        iconImage.sprite = succesSprite;

        switch (LanguageChoose.Instance.GetCurrentLanguage())
        {
            case LanguageChoose.Language.ENG:
                messageText.text = "CORRECT\nSCROLL";
                break;
            case LanguageChoose.Language.PL:
                messageText.text = "ZWÓJ\nPOPRAWNY";
                break;
            case LanguageChoose.Language.DK:
                messageText.text = "KORREKT\nAKTIVITETSRULLE";
                break;
            case LanguageChoose.Language.FIN:
                messageText.text = "OIKEA\nKÄÄRÖT";
                break;
        }
    }
}
