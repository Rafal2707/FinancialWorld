using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScrollUI : MonoBehaviour
{
    [SerializeField] private ActivityScroll thisActivityScroll;
    [SerializeField] private GameObject eKeyUI;
    [SerializeField] private GameObject descriptionUI;
    [SerializeField] private TextMeshProUGUI descriptionText;


    private void Start()
    {
        descriptionText.text = thisActivityScroll.GetDescription();
        HideEKeyUI();
        HideDescriptionUI();
    }
    public void ShowEKeyUI()
    {
        eKeyUI.SetActive(true);
    }

    public void HideEKeyUI()
    {
        eKeyUI.SetActive(false); 
    }

    public void HideDescriptionUI()
    {
        descriptionUI.SetActive(false);
    }

    public void ShowDescriptionUI()
    {
        descriptionUI.SetActive(true);
    }
}
