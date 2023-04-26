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
    [SerializeField] public NetworkVariable<int> tries = new NetworkVariable<int>(0);

    public int GetScore()
    {
        return score.Value;
    }
    

    public int GetTries()
    {
        return tries.Value;
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
        score.Value++;
        scoreText.text = score.Value.ToString();
    }

    [ServerRpc(RequireOwnership = false)]
    public void IncreaseTriesServerRpc()
    {
        IncreaseTriesTextClientRpc();
    }

    [ClientRpc]
    public void IncreaseTriesTextClientRpc()
    {
        tries.Value++;
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
