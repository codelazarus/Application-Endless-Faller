using System;
using UnityEngine;

/// <summary> Manages the state of the level </summary>
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private GameObject[] movingPlatforms;

    public float floorHeight = 5f;
    public int spawnedPlatformsOnStart = 5;
    public float currentPlatformDifficulty = 1f;
    public static bool isPlaying = false;

    private float time = 0f;
    private float difficultyIncreament = 5f;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        SpawnLevel();
        isPlaying = true;
    }

    private void Update()
    {

        time += Time.deltaTime;
        if (time > difficultyIncreament)
        {
            difficultyIncreament += 5f;
            currentPlatformDifficulty += 0.5f;
        }
    }

    public void SpawnLevel()
    {
        // Spawn platforms
        for (int i = 0; i < spawnedPlatformsOnStart; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, movingPlatforms.Length);
            GameObject platform = Instantiate(movingPlatforms[randomIndex], transform);
            Vector3 platformPosition = platform.transform.position;
            platformPosition.y -= floorHeight * i;
            platform.transform.position = platformPosition;
        }
    }

    public void OnSpawnNext(float lastPosition)
    {
        int randomIndex = UnityEngine.Random.Range(0, movingPlatforms.Length);
        GameObject platform = Instantiate(movingPlatforms[randomIndex], transform);
        Vector3 platformPosition = platform.transform.position;
        platformPosition.y -= (floorHeight * spawnedPlatformsOnStart) - lastPosition;
        platform.transform.position = platformPosition;
    }

    public void ResetLevel()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
        currentPlatformDifficulty = 1f;
        SpawnLevel();
        isPlaying = true;
    }


}
