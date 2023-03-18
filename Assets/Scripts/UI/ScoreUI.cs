using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class ScoreUI : NetworkBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] CentralBank centralBank;
    [SerializeField] CommercialBank commercialBank;
    

    private NetworkVariable<int> score = new NetworkVariable<int>(0);

    private void Start()
    {
        centralBank.OnScrollCorrect += CentralBank_OnScrollCorrect;
        centralBank.OnScrollIncorrect += CentralBank_OnScrollIncorrect;
        commercialBank.OnScrollCorrect += CommercialBank_OnScrollCorrect;
        commercialBank.OnScrollIncorrect += CommercialBank_OnScrollIncorrect;
    }

    private void CommercialBank_OnScrollIncorrect(object sender, System.EventArgs e)
    {
        DecreaseScoreServerRpc();
    }

    private void CommercialBank_OnScrollCorrect(object sender, System.EventArgs e)
    {
        IncreaseScoreServerRpc();
    }

    private void CentralBank_OnScrollIncorrect(object sender, System.EventArgs e)
    {
        DecreaseScoreServerRpc();
    }

    private void CentralBank_OnScrollCorrect(object sender, System.EventArgs e)
    {
        IncreaseScoreServerRpc();
    }







    [ClientRpc]

    public void DecreaseScoreClientRpc()
    {
        scoreText.text = "SCORE: " + score.Value;
        
    }

    [ServerRpc(RequireOwnership = false)]
    public void DecreaseScoreServerRpc()
    {
        if (score.Value > 0)
        {
            score.Value--;
            scoreText.text = "SCORE: " + score.Value;
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void IncreaseScoreServerRpc()
    {
        score.Value++;
        IncreaseScoreClientRpc();
    }

    [ClientRpc]
    public void IncreaseScoreClientRpc()
    {
        scoreText.text = "SCORE: " + score.Value;
    }
    public int GetScore()
    {
        return score.Value;
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            IncreaseScoreServerRpc();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            IncreaseScoreClientRpc();
        }
    }

}
