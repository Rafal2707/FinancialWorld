using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    private int score = 0;



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
