using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] private Button muteAllButton;





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


    [SerializeField] private TextMeshProUGUI leftStick1Text;
    [SerializeField] private TextMeshProUGUI leftStick2Text;
    [SerializeField] private TextMeshProUGUI leftStick3Text;
    [SerializeField] private TextMeshProUGUI leftStick4Text;

    private Action onCloseButtonAction;
    private void Awake()
    {
        Instance = this;
        soundEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        muteAllButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.Mute();
            MusicManager.Instance.Mute();
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

                leftStick1Text.text = "LEWY DRĄŻEK";
                leftStick2Text.text = "LEWY DRĄŻEK";
                leftStick3Text.text = "LEWY DRĄŻEK";
                leftStick4Text.text = "LEWY DRĄŻEK";

                break;
            case LanguageChoose.Language.ENG:
                optionsText.text = "OPTIONS";
                soundEffectsText.text = "SOUND EFFECTS: 5";
                musicText.text = "MUSIC: 5";
                moveUpText.text = "MOVE FORWARD";
                moveDownText.text = "MOVE BACKWARDS";
                moveLeftText.text = "MOVE LEFT";
                moveRightText.text = "MOVE RIGHT";
                InteractText.text = "INTERACT";
                pauseText.text = "PAUSE";
                closeButtonText.text = "CLOSE";

                leftStick1Text.text = "LEFT STICK";
                leftStick2Text.text = "LEFT STICK";
                leftStick3Text.text = "LEFT STICK";
                leftStick4Text.text = "LEFT STICK";
                break;
            case LanguageChoose.Language.DK:
                optionsText.text = "OPTIONER";
                soundEffectsText.text = "LYDEFFEKTER: 5";
                musicText.text = "MUSIK: 5";
                moveUpText.text = "FLYT FREMAD";
                moveDownText.text = "FLYT BAGLÆNS";
                moveLeftText.text = "FLYT TIL VENSTRE";
                moveRightText.text = "FLYT TIL HØJRE";
                InteractText.text = "INTERAGERE";
                pauseText.text = "PAUSE";
                closeButtonText.text = "LUK";

                leftStick1Text.text = "VENSTRE PIND";
                leftStick2Text.text = "VENSTRE PIND";
                leftStick3Text.text = "VENSTRE PIND";
                leftStick4Text.text = "VENSTRE PIND";
                break;
            case LanguageChoose.Language.FIN:
                optionsText.text = "ASETUKSET";
                soundEffectsText.text = "ÄÄNIEFEKTIT: 5";
                musicText.text = "MUSIIKKI: 5";
                moveUpText.text = "LIIKU ETEENPÄIN";
                moveDownText.text = "LIIKU TAAKSEPÄIN";
                moveLeftText.text = "LIIKU VASEMMALLE";
                moveRightText.text = "LIIKU OIKEALLE";
                InteractText.text = "POIMI/PUDOTA";
                pauseText.text = "PYSÄYTÄ";
                closeButtonText.text = "SULJE";

                leftStick1Text.text = "VASEN TIKKU";
                leftStick2Text.text = "VASEN TIKKU";
                leftStick3Text.text = "VASEN TIKKU";
                leftStick4Text.text = "VASEN TIKKU";
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

        closeButton.Select();
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
