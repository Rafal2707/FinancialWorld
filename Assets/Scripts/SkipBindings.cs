using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SkipBindings : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI keyGamepadInteractText;
    [SerializeField] private TextMeshProUGUI keyInteractText;
    [SerializeField] private TextMeshProUGUI toSkipText;


    private void Start()
    {
        keyInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        keyGamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);

        switch (LanguageChoose.Instance.GetCurrentLanguage())
        {
            case LanguageChoose.Language.PL:
                toSkipText.text = "BY POMINĄĆ";
                break;
            case LanguageChoose.Language.ENG:
                toSkipText.text = "TO SKIP";
                break;
            case LanguageChoose.Language.DK:
                toSkipText.text = "SPRINGE";
                break;
            case LanguageChoose.Language.FIN:
                toSkipText.text = "OHITA";
                break;
        }
    }
}
