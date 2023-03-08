using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] CentralBank centralBank;
    [SerializeField] CommercialBank commercialBank;
    private int score = 0;

    private void Start()
    {
        centralBank.OnScrollCorrect += CentralBank_OnScrollCorrect;
        centralBank.OnScrollIncorrect += CentralBank_OnScrollIncorrect;
        commercialBank.OnScrollCorrect += CommercialBank_OnScrollCorrect;
        commercialBank.OnScrollIncorrect += CommercialBank_OnScrollIncorrect;
    }

    private void CommercialBank_OnScrollIncorrect(object sender, System.EventArgs e)
    {
        DecreaseScore();
    }

    private void CommercialBank_OnScrollCorrect(object sender, System.EventArgs e)
    {
        IncreaseScore();
    }

    private void CentralBank_OnScrollIncorrect(object sender, System.EventArgs e)
    {
        DecreaseScore();
    }

    private void CentralBank_OnScrollCorrect(object sender, System.EventArgs e)
    {
        IncreaseScore();
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = "SCORE: " + score;
    }

    public void DecreaseScore()
    {
        if(score > 0)
        {
            score--;
            scoreText.text = "SCORE: " + score;
        }
    }

    public int GetScore()
    {
        return score;
    }
}
