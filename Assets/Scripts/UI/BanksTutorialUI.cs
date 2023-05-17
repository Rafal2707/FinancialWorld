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
                descriptionText.text = "Banki centralne i komercyjne odgrywają różne role w gospodarce państwa i życiu codziennym ludzi. Na co dzień wykonują pewne czynności, które są ważnym elementem sektora finansowego w każdym kraju.\r\n\r\n\r\n Jako mieszkaniec \"Financial City\" masz za zadanie prawidłowo dopasować czynności bankowe do odpowiednich instytucji finansowych.\r\n\r\n Dopasowując \"Zwoje aktywności\" do banków, otrzymasz punkty oraz rozwiniesz swoją wiedzę finansową!";
                centralBankText.text = "Bank Centralny";
                commercialBankText.text = "Bank Komercyjny";
                introText.text = "Istnieją dwa typy banków...";
                nextSlideButtonText.text = "DALEJ";

                break;
            case LanguageChoose.Language.ENG:
                descriptionText.text = "Central and commercial banks play different roles in the state's economy and impact people's daily lives. On a daily basis, they perform certain activities that are an important element of the financial sector in every country.\r\n\r\n\r\n As a resident of \"Financial City\", you have the responsibility of correctly matching bank activities with their corresponding institutions.\r\n\r\n By matching \"Activity Scrolls,\" you can earn points and enhance your financial knowledge!";
                centralBankText.text = "Central Bank";
                commercialBankText.text = "Commercial Bank";
                introText.text = "There are two types of banks...";
                nextSlideButtonText.text = "NEXT";

                break;
            case LanguageChoose.Language.DK:
                descriptionText.text = "Central- og forretningsbanker spiller forskellige roller i statens økonomi og påvirker folks dagligdag. De udfører dagligt visse aktiviteter, som er et vigtigt element i den finansielle sektor i alle lande.\r\n\r\n\r\n Som beboer i \"Financial City\" har du ansvaret for at matche bankaktiviteterne korrekt med deres tilsvarende institutioner.\r\n\r\n Ved at matche \"aktivitetsrullen\" kan du optjene point og forbedre din finansielle viden!";
                centralBankText.text = "Centralbank";
                commercialBankText.text = "Kommerciel bank";
                introText.text = "Der findes to typer banker...";
                nextSlideButtonText.text = "NÆSTE";

                break;
            case LanguageChoose.Language.FIN:
                descriptionText.text = "Keskus- ja liikepankeilla on erilaisia ​​rooleja valtiontaloudessa ja ne vaikuttavat ihmisten jokapäiväiseen elämään.  Pankit suorittavat päivittäin erilaisia toimintoja, jotka ovat tärkeä osa rahoitussektoria kaikissa maissa.\r\n\r\n\r\n \"Financial Cityn\" asukkaana olet vastuussa pankkien toiminnan ja vastaavien laitosten oikeasta yhteensovittamisesta.\r\n\r\n Viemällä pelistä löytyvät \"kääröt\" oikean pankin luo voit ansaita pisteitä ja parantaa taloustietoasi!";
                centralBankText.text = "Keskuspankki";
                commercialBankText.text = "Liikepankki";
                introText.text = "On olemassa kahdenlaisia pankkeja...";
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
