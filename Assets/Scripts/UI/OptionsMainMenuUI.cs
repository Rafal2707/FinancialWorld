using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMainMenuUI : MonoBehaviour
{
    public static OptionsMainMenuUI Instance { get; private set; }

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
    [SerializeField] private TextMeshProUGUI muteAllButtonText;
    [SerializeField] private TextMeshProUGUI optionsText;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI InteractText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private TextMeshProUGUI closeButtonText;


    [SerializeField] private TextMeshProUGUI leftStick1Text;
    [SerializeField] private TextMeshProUGUI leftStick2Text;
    [SerializeField] private TextMeshProUGUI leftStick3Text;
    [SerializeField] private TextMeshProUGUI leftStick4Text;


    


    [SerializeField] private Transform pressToRebindKeyTransform;


    //MainMenuUI
    [SerializeField] private Button playMultiplayerButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button playSingleplayerButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button languagePLButton;
    [SerializeField] private Button languageENGButton;
    [SerializeField] private Button languageDKButton;
    [SerializeField] private Button languageFINButton;


    private void Awake()
    {
        Instance = this;
        soundEffectsButton.onClick.AddListener(() =>
        {
            MenuMusicManager.Instance.ChangeSoundEffectsVolume();
            UpdateVisual();
        });

        muteAllButton.onClick.AddListener(() =>
        {
            MenuMusicManager.Instance.Mute();
            UpdateVisual();
        });


        closeButton.onClick.AddListener(() =>
        {
            Hide();
        });
        musicButton.onClick.AddListener(() =>
        {
            MenuMusicManager.Instance.ChangeMusicVolume();
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
        HidePressToRebindKey();
        Hide();
    }

    public void UpdateVisual()
    {

        switch (LanguageChoose.Instance.GetCurrentLanguage())
        {
            case LanguageChoose.Language.PL:
                optionsText.text = "OPCJE";
                soundEffectsText.text = "EFEKTY DZWIĘKOWE: 5";
                muteAllButtonText.text = "WYCISZ";
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
                muteAllButtonText.text = "MUTE ALL";
                musicText.text = "MUSIC: 5";
                moveUpText.text = "MOVE UP";
                moveDownText.text = "MOVE DOWN";
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
                optionsText.text = "MULIGHEDER";
                soundEffectsText.text = "LYDVIRKNINGER: 5";
                muteAllButtonText.text = "STUM";
                musicText.text = "MUSIK: 5";
                moveUpText.text = "FLYT OP";
                moveDownText.text = "FLYT NED";
                moveLeftText.text = "BEVÆG DIG TIL VENSTRE";
                moveRightText.text = "FLYT TIL HØJRE";
                InteractText.text = "INTERAKTERE";
                pauseText.text = "PAUSE";
                closeButtonText.text = "TÆT";

                leftStick1Text.text = "VENSTRE PIND";
                leftStick2Text.text = "VENSTRE PIND";
                leftStick3Text.text = "VENSTRE PIND";
                leftStick4Text.text = "VENSTRE PIND";
                break;
            case LanguageChoose.Language.FIN:
                optionsText.text = "VAIHTOEHDOT";
                soundEffectsText.text = "ÄÄNIEFEKTIOT: 5";
                muteAllButtonText.text = "MYKISTÄÄ";
                musicText.text = "MUSIIKKI: 5";
                moveUpText.text = "LIIKU YLÖS";
                moveDownText.text = "SIIRRÄ ALAS";
                moveLeftText.text = "SIIRRY VASEMMALLE";
                moveRightText.text = "LIIKU OIKEALLE";
                InteractText.text = "VUOROPUHELU";
                pauseText.text = "TAUKO";
                closeButtonText.text = "KIINNI";

                leftStick1Text.text = "VASEN TIKKU";
                leftStick2Text.text = "VASEN TIKKU";
                leftStick3Text.text = "VASEN TIKKU";
                leftStick4Text.text = "VASEN TIKKU";
                break;
        }




        musicText.text = GetTextUntilNumberAppears(musicText.text) + ": " + Mathf.Round(MenuMusicManager.Instance.GetMusicVolume() * 10f);
        soundEffectsText.text = GetTextUntilNumberAppears(soundEffectsText.text) + ": " + Mathf.Round(MenuMusicManager.Instance.GetSoundEffectsVolume() * 10f);


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


    public void Show()
    {
        gameObject.SetActive(true);

        playMultiplayerButton.gameObject.SetActive(false);
        optionsButton.gameObject.SetActive(false);
        playSingleplayerButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        languagePLButton.gameObject.SetActive(false);
        languageDKButton.gameObject.SetActive(false);
        languageENGButton.gameObject.SetActive(false);
        languageFINButton.gameObject.SetActive(false);



        closeButton.Select();
    }

    public void Hide()
    {
        playMultiplayerButton.gameObject.SetActive(true);
        optionsButton.gameObject.SetActive(true);
        playSingleplayerButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        languagePLButton.gameObject.SetActive(true);
        languageDKButton.gameObject.SetActive(true);
        languageENGButton.gameObject.SetActive(true);
        languageFINButton.gameObject.SetActive(true);


        playSingleplayerButton.Select();
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
        GameInput.Instance.RebindBinding(binding, () => {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }
}
