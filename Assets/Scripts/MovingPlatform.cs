using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private static float speed = 0.2f;
    private float currentDifficulty = 1f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * Time.deltaTime * speed * currentDifficulty;
    }
}