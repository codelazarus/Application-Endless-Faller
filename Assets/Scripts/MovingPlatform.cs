using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private static float speed = 2f;
    private static float currentDifficulty = 1f;
    void Start()
    {
        currentDifficulty = LevelManager.Instance.currentPlatformDifficulty;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * Time.deltaTime * speed * currentDifficulty;
        if (transform.position.y > 10)
        {
            LevelManager.Instance.OnSpawnNext(transform.position.y);
            Destroy(gameObject);
        }
    }
    
}