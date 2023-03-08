using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScrollResultUICentralBank : MonoBehaviour
{
    private const string POPUP = "Popup";

    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private CentralBank centralBank;
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
        centralBank.OnScrollCorrect += CentralBank_OnScrollCorrect;
        centralBank.OnScrollIncorrect += CentralBank_OnScrollIncorrect;




        gameObject.SetActive(false);
    }

    private void CentralBank_OnScrollIncorrect(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        animator.SetTrigger(POPUP);
        backgroundImage.color = failedColor;
        iconImage.sprite = failedSprite;
        messageText.text = "INCORRECT\nSCROLL";
    }

    private void CentralBank_OnScrollCorrect(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        animator.SetTrigger(POPUP);
        backgroundImage.color = succesColor;
        iconImage.sprite = succesSprite;
        messageText.text = "CORRECT\nSCROLL";
    }
}
