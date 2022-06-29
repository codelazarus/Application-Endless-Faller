using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] private GameObject[] movingPlatforms;
    private Vector3 _startSpawnPosition;

    private void Start()
    {
        _startSpawnPosition = transform.position;
    }

    public void SpawnLevel(int spawnedPlatformsOnStart, float floorHeight)
    {

        for (int i = 0; i < spawnedPlatformsOnStart; i++)
        {
            int randomIndex = Random.Range(0, movingPlatforms.Length);
            GameObject platform = Instantiate(movingPlatforms[randomIndex], transform);
            Vector3 platformPosition = platform.transform.position;
            platformPosition.y -= floorHeight * i;
            platform.transform.position = platformPosition;
        }
    }

    public void OnSpawnNext(int spawnedPlatformsOnStart, float floorHeight)
    {
        int randomIndex = Random.Range(0, movingPlatforms.Length);
        GameObject platform = Instantiate(movingPlatforms[randomIndex], transform);
        Vector3 platformPosition = platform.transform.position;
        platformPosition.y -= floorHeight * (spawnedPlatformsOnStart-1);
        platform.transform.position = platformPosition;
    }
}
