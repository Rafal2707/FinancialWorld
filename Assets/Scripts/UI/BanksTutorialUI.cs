using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BanksTutorialUI : MonoBehaviour
{
    public static BanksTutorialUI Instance { get; private set; }

    [SerializeField] private Button nextSlideButton;
    [SerializeField] private Button gameLoopUINextSlideButton;
    [SerializeField] private TextMeshProUGUI nextSlideButtonText;


    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI centralBankText;
    [SerializeField] private TextMeshProUGUI commercialBankText;
    [SerializeField] private TextMeshProUGUI introText;

    private void Awake()
    {
        Instance = this;

    }

    private void Start()
    {
        nextSlideButton.Select();


        nextSlideButton.onClick.AddListener(() => {
            GameLoopUI.Instance.Show();
            gameLoopUINextSlideButton.Select();
            Hide();
        });

        switch (LanguageChoose.Instance.GetCurrentLanguage())
        {
            case LanguageChoose.Language.PL:
                descriptionText.text = "Banki centralne i komercyjne odgrywają różne role w gospodarce państwa i życiu codziennym ludzi. Na co dzień wykonują pewne czynności, które są ważnym elementem sektora finansowego w każdym kraju.\r\n\r\n\r\nW Financial City odpowiadasz za dopasowanie działań do właściwego banku!\r\n\r\nOtrzymasz punkty za poprawne dopasowanie!";
                centralBankText.text = "Bank Centralny";
                commercialBankText.text = "Bank Komercyjny";
                introText.text = "Istnieją dwa typy banków...";
                nextSlideButtonText.text = "DALEJ";

                break;
            case LanguageChoose.Language.ENG:
                descriptionText.text = "Central and commercial banks play different roles in the state's economy and people's daily lives. On a daily basis, they perform certain activities that are an important element of the financial sector in every country.\r\n\r\n\r\nIn Financial City, you are responsible for matching activities with the correct bank!\r\n\r\nYou will receive points for a correct matches!";
                centralBankText.text = "Central Bank";
                commercialBankText.text = "Commercial Bank";
                introText.text = "There are two types of banks...";
                nextSlideButtonText.text = "NEXT";

                break;
            case LanguageChoose.Language.DK:
                descriptionText.text = "Central- og forretningsbanker spiller forskellige roller i statens økonomi og folks dagligdag. På daglig basis udfører de visse aktiviteter, som er et vigtigt element i den finansielle sektor i alle lande.\r\n\r\n\r\nI Financial City er du ansvarlig for at matche aktiviteter med den rigtige bank!\r\n\r\nDu vil modtage point for en korrekt kamp!";
                centralBankText.text = "Centralbank";
                commercialBankText.text = "Forretnings Bank";
                introText.text = "Der er to typer banker...";
                nextSlideButtonText.text = "NÆSTE";

                break;
            case LanguageChoose.Language.FIN:
                descriptionText.text = "";
                centralBankText.text = "";
                commercialBankText.text = "";
                introText.text = "";
                nextSlideButtonText.text = "SEURAAVA";

                break;
        }

    }
    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
