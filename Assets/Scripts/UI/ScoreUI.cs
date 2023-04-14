using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.CullingGroup;

public class ScoreUI : NetworkBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] CentralBank centralBank;
    [SerializeField] CommercialBank commercialBank;


    [SerializeField] public NetworkVariable<int> score = new NetworkVariable<int>(0);

    public int GetScore()
    {
        return score.Value;
    }

 

    public override void OnNetworkSpawn()
    {
        score.OnValueChanged += OnStateChanged;
    }

    public override void OnNetworkDespawn()
    {
        score.OnValueChanged -= OnStateChanged;
    }

    public void OnStateChanged(int previous, int current)
    {
        UpdateScoreTextClientRpc(current);        
    }

    [ServerRpc(RequireOwnership = false)]
    public void IncreaseServerRpc()
    {
        // this will cause a replication over the network
        // and ultimately invoke `OnValueChanged` on receivers

        Debug.Log("Increased from server");
        score.Value++;
        scoreText.text = score.Value.ToString();
    }

    [ClientRpc]
    public void UpdateScoreTextClientRpc(int currentValue)
    {
        scoreText.text = currentValue.ToString();
    }


    [ServerRpc(RequireOwnership = false)]
    public void DecreaseServerRpc()
    {
        if(score.Value > 0)
        {
            score.Value--;
        }
        scoreText.text = score.Value.ToString();

    }


}
