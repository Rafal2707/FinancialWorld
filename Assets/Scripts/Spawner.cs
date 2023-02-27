using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<Transform> spawningLocationsList;


    [SerializeField] List<ActivityScrollSO> allScrollsList;
    public List<ActivityScrollSO> spawnedScrollsSlist;

    int randomNumber;
    float timeToSpawnMax = 10f;
    float timeToSpawn;

    float originalPlaneSize = 10f;
    float offsetFromPlaneEdges = .5f;

    private void Start()
    {
        spawnedScrollsSlist = new List<ActivityScrollSO>();

        timeToSpawn = 3f;
    }


    private void Update()
    {
        if (!GameManager.Instance.IsGamePlaying()) return;

        randomNumber = Random.Range(0, spawningLocationsList.Count);
        timeToSpawn -= Time.deltaTime;

        if (timeToSpawn < 0)
        {
            Transform randomLocationTransform = spawningLocationsList[randomNumber].transform;
            if(spawnedScrollsSlist.Count == allScrollsList.Count)
            {
                return;
            }
            ActivityScrollSO randomScript = PickRandomScroll();
            if(randomScript != null)
            {
               
                spawnedScrollsSlist.Add(randomScript);
               Transform spawnedScrollTransfrom = Instantiate(randomScript.prefab, randomLocationTransform.position + RandomSpawnOffsetPosition(randomLocationTransform), Quaternion.identity);

                spawnedScrollTransfrom.GetComponent<ActivityScroll>().AssignCentralBankActivity(randomScript.isCentralBankActivity);
                spawnedScrollTransfrom.GetComponent<ActivityScroll>().SetDescription(randomScript.Description);

                timeToSpawn = timeToSpawnMax;
            }
           
        }
    }


    private Vector3 RandomSpawnOffsetPosition(Transform planeTransform)
    {
        Vector3 randomPositionOnPlane = new Vector3(Random.Range(-planeTransform.localScale.x * (originalPlaneSize - offsetFromPlaneEdges) / 2, planeTransform.localScale.x * (originalPlaneSize - offsetFromPlaneEdges) / 2),
                                                    0.5f, Random.Range(-planeTransform.localScale.z * (originalPlaneSize - offsetFromPlaneEdges) / 2, planeTransform.localScale.z * (originalPlaneSize - offsetFromPlaneEdges) / 2));


            return randomPositionOnPlane;
    }


    private ActivityScrollSO PickRandomScroll()
    {
        int attempts = 0;
        ActivityScrollSO randomActivityScroll = allScrollsList[Random.Range(0, allScrollsList.Count)];
        
        //Make sure to spawn different activity each time.
        while (spawnedScrollsSlist.Contains(randomActivityScroll) && attempts < allScrollsList.Count)
        {
            randomActivityScroll = allScrollsList[Random.Range(0, allScrollsList.Count)];
            attempts++;
        }
        if (spawnedScrollsSlist.Contains(randomActivityScroll)) 
        {
            randomActivityScroll = null;
        }
        return randomActivityScroll;
    }

}
