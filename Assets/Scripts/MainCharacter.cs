using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    private Rigidbody _rigidBody;
    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        GameManager.Instance.player = this;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        IsGameOver();
    }

    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.name.Equals("PointTrigger"))
            GameManager.Instance.OnScore();
    }

    private void IsGameOver()
    {
        if (LevelManager.isPlaying)
        {
            if (transform.position.y > 6)
            {
                GameManager.Instance.OnGameOver();
            }
            else if (transform.position.y < -5)
            {
                GameManager.Instance.OnGameOver();


            }
        }
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            _rigidBody.AddForce(Vector2.left * Time.deltaTime * movementSpeed, ForceMode.Impulse);
        else if (Input.GetKey(KeyCode.RightArrow))
            _rigidBody.AddForce(Vector2.right * Time.deltaTime * movementSpeed, ForceMode.Impulse);
    }
}
