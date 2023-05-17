using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : NetworkBehaviour
{



    public static Spawner Instance { get; private set; }

    public event EventHandler OnSpawnedScroll;

    [SerializeField] List<Transform> spawningLocationsList;


    [SerializeField] public List<ActivityScrollSO> allScrollsList;


    public List<ActivityScrollSO> spawnedScrollsList;

    float timeToSpawnMax = 4f;
    float timeToSpawn = 4f;

    float originalPlaneSize = 10f;
    float offsetFromPlaneEdges = .5f;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {

        

        spawnedScrollsList = new List<ActivityScrollSO>();


        timeToSpawn = 2.5f;
    }


    private void Update()
    {
        if (!IsServer)
        {
            return;
        }
        if (!GameManager.Instance.IsGamePlaying())
        {
            return;
        };

        if (spawnedScrollsList.Count == allScrollsList.Count)
        {
            return;
        }

        timeToSpawn -= Time.deltaTime;
        if (timeToSpawn < 0)
        {
            timeToSpawn = timeToSpawnMax;
            ActivityScrollSO newRandomScript = PickRandomScroll();

            if (newRandomScript != null)
            {
                int newRandomScriptIndex = GetActivityScrollSOIndex(newRandomScript);



                // Don't spawn if there are no available scrolls anymore.
                if (newRandomScriptIndex != -1)
                {                    
                    SpawnNewScrollServerRpc(newRandomScriptIndex);
                }
            }

        }
    }



    [ServerRpc]
    private void SpawnNewScrollServerRpc(int newActivityScrollSOIndex)
    {

        Transform randomLocationTransform = spawningLocationsList[0].transform;
        ActivityScrollSO randomActivityScrollSO = GetActivityScrollSOFromIndex(newActivityScrollSOIndex);


        Transform spawnedScrollTransform = Instantiate(randomActivityScrollSO.prefab, RandomSpawnOffsetPosition(randomLocationTransform), Quaternion.identity);

        NetworkObject spawnedNetworkScroll = spawnedScrollTransform.GetComponent<NetworkObject>();

        // Spawn is like instantiate for network, we need to do this first and after that we can assign parameters to it !
        spawnedNetworkScroll.Spawn(true);

        spawnedNetworkScroll.GetComponent<ActivityScroll>().AssignCentralBankActivityClientRpc(randomActivityScrollSO.isCentralBankActivity);
        spawnedNetworkScroll.GetComponent<ActivityScroll>().AssignNumberClientRpc(randomActivityScrollSO.number);
        AssignHostDescriptionLanguageClientRpc(newActivityScrollSOIndex, spawnedNetworkScroll);
        
        SpawnNewScrollClientRpc(newActivityScrollSOIndex);


    }

    [ClientRpc]
    private void SpawnNewScrollClientRpc(int index)
    {
        spawnedScrollsList.Add(GetActivityScrollSOFromIndex(index));

        OnSpawnedScroll?.Invoke(this, EventArgs.Empty);
    }



    [ClientRpc]
    private void AssignHostDescriptionLanguageClientRpc(int newActivityScrollSOIndex, NetworkObjectReference spawnedNetworkScrollReference)
    {
        ActivityScrollSO randomActivityScrollSO = GetActivityScrollSOFromIndex(newActivityScrollSOIndex);

        spawnedNetworkScrollReference.TryGet(out NetworkObject spawnedNetworkScroll);


        switch (LanguageChoose.Instance.GetCurrentLanguage())
        {
            case LanguageChoose.Language.DK:
                spawnedNetworkScroll.GetComponent<ActivityScroll>().SetDescription(randomActivityScrollSO.descriptionDK);
                break;
            case LanguageChoose.Language.PL:
                spawnedNetworkScroll.GetComponent<ActivityScroll>().SetDescription(randomActivityScrollSO.descriptionPL);
                break;
            case LanguageChoose.Language.ENG:
                spawnedNetworkScroll.GetComponent<ActivityScroll>().SetDescription(randomActivityScrollSO.descriptionENG);
                break;
            case LanguageChoose.Language.FIN:
                spawnedNetworkScroll.GetComponent<ActivityScroll>().SetDescription(randomActivityScrollSO.descriptionFIN);
                break;
        }
    }


    private Vector3 RandomSpawnOffsetPosition(Transform planeTransform)
    {
        Vector3 planePosition = transform.position;
        Vector3 planeScale = transform.localScale;

        // Calculate the left, right, top, and bottom edges
        float leftEdge = -10f;
        float rightEdge = 35f;
        float topEdge = 2.5f;
        float bottomEdge = -30f;


        Vector3 tempPos = new Vector3(Random.Range(leftEdge, rightEdge), 0.5f, Random.Range(bottomEdge, topEdge));


        Vector3 randomPositionOnPlane = new Vector3(Random.Range(-planeTransform.localScale.x * (originalPlaneSize - offsetFromPlaneEdges) / 2, planeTransform.localScale.x * (originalPlaneSize - offsetFromPlaneEdges) / 2),
                                                    0.5f, Random.Range(-planeTransform.localScale.z * (originalPlaneSize - offsetFromPlaneEdges) / 2, planeTransform.localScale.z * (originalPlaneSize - offsetFromPlaneEdges) / 2));
            return tempPos;
    }



    private ActivityScrollSO PickRandomScroll2()
    {
        ActivityScrollSO randomActivityScroll = allScrollsList[Random.Range(0, allScrollsList.Count)];
        return randomActivityScroll;
    }

    private ActivityScrollSO PickRandomScroll()
    {
        int attempts = 0;
        ActivityScrollSO randomActivityScroll = allScrollsList[Random.Range(0, allScrollsList.Count)];
        
        //Make sure to spawn different activity each time.
        while (spawnedScrollsList.Contains(randomActivityScroll) && attempts < allScrollsList.Count)
        {
            randomActivityScroll = allScrollsList[Random.Range(0, allScrollsList.Count)];
            attempts++;
        }
        if (spawnedScrollsList.Contains(randomActivityScroll)) 
        {
            randomActivityScroll = null;
        }
        return randomActivityScroll;
    }


    private int GetActivityScrollSOIndex(ActivityScrollSO activityScrollSO)
    {
        return allScrollsList.IndexOf(activityScrollSO);
    }

    private ActivityScrollSO GetActivityScrollSOFromIndex(int index)
    {
        return allScrollsList[index];
    }

}
