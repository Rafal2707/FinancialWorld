using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementUI : MonoBehaviour
{
    public static PlayerMovementUI Instance { get; private set; }

    [SerializeField] private Button nextSlideButton;
    [SerializeField] private TextMeshProUGUI nextSlideButtonText;

    [SerializeField] private TextMeshProUGUI playerMovementText;
    [SerializeField] private TextMeshProUGUI leftStickText;

    private void Awake()
    {
        Instance = this;

    }
    private void Start()
    {

        nextSlideButton.onClick.AddListener(() => {
            OtherControlsUI.Instance.Show();
            Hide();
        });

        switch (LanguageChoose.Instance.GetCurrentLanguage())
        {
            case LanguageChoose.Language.PL:
                playerMovementText.text = "PORUSZANIE GRACZA";
                leftStickText.text = "LEWY DRĄŻEK";
                nextSlideButtonText.text = "DALEJ";
                break;
            case LanguageChoose.Language.ENG:
                playerMovementText.text = "PLAYER MOVEMENT";
                leftStickText.text = "LEFT STICK";
                nextSlideButtonText.text = "NEXT";
                break;
            case LanguageChoose.Language.DK:
                playerMovementText.text = "SPILLERBEVÆGELSE";
                leftStickText.text = "VENSTRE PIND";
                nextSlideButtonText.text = "NÆSTE";
                break;
            case LanguageChoose.Language.FIN:
                playerMovementText.text = "PELAAJAN LIIKKEET";
                leftStickText.text = "VASEN TIKKU";
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
