using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameLoopUI : MonoBehaviour
{
    public static GameLoopUI Instance { get; private set; }

    [SerializeField] private Button nextSlideButton;
    [SerializeField] private TextMeshProUGUI nextSlideButtonText;

    [SerializeField] private Button playerMovementUINextSlideButton;

    [SerializeField] private TextMeshProUGUI pickUpText;
    [SerializeField] private TextMeshProUGUI bringBackText;
    [SerializeField] private TextMeshProUGUI dropText;


    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        nextSlideButton.onClick.AddListener(() => {
            PlayerMovementUI.Instance.Show();
            playerMovementUINextSlideButton.Select();
            Hide();
        });


        switch (LanguageChoose.Instance.GetCurrentLanguage())
        {
            case LanguageChoose.Language.PL:
                pickUpText.text = "PODNIEŚ ZWÓJ AKTYWNOŚCI I PRZECZYTAJ OPIS";
                bringBackText.text = "ZANIEŚ ZWÓJ AKTYWNOŚCI DO PRAWIDŁOWEGO BANKU";
                dropText.text = "UPUŚĆ ZWÓJ AKTYWNOŚCI W STREFIE BANKU";
                nextSlideButtonText.text = "DALEJ";

                break;
            case LanguageChoose.Language.ENG:
                pickUpText.text = "PICK UP ACTIVITY SCROLL AND READ DESCRIPTION";
                bringBackText.text = "BRING BACK ACTIVITY SCROLL TO CORRECT BANK";
                dropText.text = "DROP ACTIVITY SCROLL IN BANK ZONE";
                nextSlideButtonText.text = "NEXT";

                break;
            case LanguageChoose.Language.DK:
                pickUpText.text = "SAMLE AKTIVITETSRULLEN OP OG LÆSE BESKRIVELSEN";
                bringBackText.text = "BRINGE AKTIVITETSRULLEN TILBAGE TIL DEN KORREKTE BANK";
                dropText.text = "DROP AKTIVITETSRULLE I BANKZONEN";
                nextSlideButtonText.text = "NÆSTE";

                break;
            case LanguageChoose.Language.FIN:
                pickUpText.text = "POIMI KÄÄRÖ JA LUE SEN SISÄLTÖ";
                bringBackText.text = "VIE KÄÄRÖ SEN PANKIN LUO, JOTA TOIMINTO KUVAA";
                dropText.text = "PUDOTA KÄÄRÖ VALITSEMASI PANKIN EDUSTALLE";
                nextSlideButtonText.text = "SEURAAVA";
                break;
        }


        Hide();

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
