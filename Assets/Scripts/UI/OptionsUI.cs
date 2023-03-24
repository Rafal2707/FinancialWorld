using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }


    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button gamepadInteractButton;
    [SerializeField] private Button gamepadPauseButton;





    [SerializeField] private TextMeshProUGUI moveUpButtonText;
    [SerializeField] private TextMeshProUGUI moveDownButtonText;
    [SerializeField] private TextMeshProUGUI moveLeftButtonText;
    [SerializeField] private TextMeshProUGUI moveRightButtonText;
    [SerializeField] private TextMeshProUGUI interactButtonText;
    [SerializeField] private TextMeshProUGUI pauseButtonText;
    [SerializeField] private TextMeshProUGUI gamepadInteractButtonText;
    [SerializeField] private TextMeshProUGUI gamepadPauseButtonText;

    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI optionsText;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI InteractText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private TextMeshProUGUI closeButtonText;


    [SerializeField] private Transform pressToRebindKeyTransform;


    private Action onCloseButtonAction;
    private void Awake()
    {
        Instance = this;
        soundEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });


        closeButton.onClick.AddListener(() =>
        {
            Hide();
            onCloseButtonAction();
        });
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        moveUpButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Up);
        });
        moveDownButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Down);
        });
        moveLeftButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Left);
        });
        moveRightButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Move_Right);
        });
        interactButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Interact);
        });
        pauseButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Pause);
        });
        gamepadInteractButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Gamepad_Interact);
        });
        gamepadPauseButton.onClick.AddListener(() =>
        {
            RebindBinding(GameInput.Binding.Gamepad_Pause);
        });

    }
    private void Start()
    {
        GameManager.Instance.OnLocalGameUnpaused += GameManager_OnGameUnpaused;

        switch (LanguageChoose.Instance.GetCurrentLanguage())
        {
            case LanguageChoose.Language.PL:
                optionsText.text = "OPCJE";
                soundEffectsText.text = "EFEKTY DZWIĘKOWE: 5";
                musicText.text = "MUZYKA: 5";
                moveUpText.text = "DO PRZODU";
                moveDownText.text = "DO TYŁU";
                moveLeftText.text = "W LEWO";
                moveRightText.text = "W PRAWO";
                InteractText.text = "INTERAKCJA";
                pauseText.text = "PAUZA";
                closeButtonText.text = "ZAMKNIJ";
                break;
            case LanguageChoose.Language.ENG:
                optionsText.text = "OPTIONS";
                soundEffectsText.text = "SOUND EFFECTS: 5";
                musicText.text = "MUSIC: 5";
                moveUpText.text = "MOVE UP";
                moveDownText.text = "MOVE DOWN";
                moveLeftText.text = "MOVE LEFT";
                moveRightText.text = "MOVE RIGHT";
                InteractText.text = "INTERACT";
                pauseText.text = "PAUSE";
                closeButtonText.text = "CLOSE";
                break;
            case LanguageChoose.Language.DK:
                optionsText.text = "MULIGHEDER";
                soundEffectsText.text = "LYDVIRKNINGER: 5";
                musicText.text = "MUSIK: 5";
                moveUpText.text = "FLYT OP";
                moveDownText.text = "FLYT NED";
                moveLeftText.text = "BEVÆG DIG TIL VENSTRE";
                moveRightText.text = "FLYT TIL HØJRE";
                InteractText.text = "INTERAKTERE";
                pauseText.text = "PAUSE";
                closeButtonText.text = "TÆT";
                break;
            case LanguageChoose.Language.FIN:
                optionsText.text = "VAIHTOEHDOT";
                soundEffectsText.text = "ÄÄNIEFEKTIOT: 5";
                musicText.text = "MUSIIKKI: 5";
                moveUpText.text = "LIIKU YLÖS";
                moveDownText.text = "SIIRRÄ ALAS";
                moveLeftText.text = "SIIRRY VASEMMALLE";
                moveRightText.text = "LIIKU OIKEALLE";
                InteractText.text = "VUOROPUHELU";
                pauseText.text = "TAUKO";
                closeButtonText.text = "KIINNI";
                break;
        }
        UpdateVisual();


        HidePressToRebindKey();
        Hide();
    }

    private void GameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        soundEffectsText.text = GetTextUntilNumberAppears(soundEffectsText.text) + ": " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = GetTextUntilNumberAppears(musicText.text) + ": " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);


        moveUpButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        pauseButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);

        gamepadInteractButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        gamepadPauseButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause);

    }

    private string GetTextUntilNumberAppears(string text)
    {
        int charLocation = text.IndexOf(":", StringComparison.Ordinal);
        if (charLocation > 0)
        {
            return text.Substring(0, charLocation);
        }
        return string.Empty;
    }


    public void Show(Action onCloseButtonAction)
    {
        this.onCloseButtonAction = onCloseButtonAction;
        gameObject.SetActive(true);

        soundEffectsButton.Select();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }


    private void ShowPressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }
    private void HidePressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }


    private void RebindBinding(GameInput.Binding binding)
    {
        ShowPressToRebindKey();
        GameInput.Instance.RebindBinding(binding,() => { 
        HidePressToRebindKey();
        UpdateVisual();
        });
    }
}
