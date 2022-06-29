using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary> Manages the state of the whole application </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public MainCharacter player;
    [SerializeField] private string gameScene;

    public Action OnScore;
    

    [Header("Game Over UI")]
    public GameObject GameOverUI;
    public Text currentScoreText;
    public Text highScoreText;

    [Header("Pause UI")]
    public GameObject PauseUI;


    [Header("Home UI")]
    public GameObject HomeUI;
    public Text highscoreMain;
    private int points;

    [Header("Gameplay UI")]
    public GameObject GamePlayUI;
    public Text score;

    private int highscore;
    private int tempHighScore;
    private int currentscore;
    private string saveFile;

    private HighScore highScoreData = new HighScore();

    private void OnEnable()
    {
        OnScore += IncrementScore;
    }

    private void OnDisable()
    {
        OnScore -= IncrementScore;
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        DontDestroyOnLoad(this);
        saveFile = Application.persistentDataPath + "/highscore.json";
    }

    private void Start()
    {
        SceneManager.LoadScene("Home");
        LoadHighScore();
        SetUIsState(false, false, true, false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && LevelManager.isPlaying)
        {
            Pause();
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(gameScene);
        points = 0;
        score.text = "Score: " + points.ToString();

        SetUIsState(false, false, false, true);
    }

    public void OnGameOver()
    {
        SetUIsState(true, false, false, false);
        LevelManager.isPlaying = false;
        Time.timeScale = 0f;

        LoadHighScore();

        if (tempHighScore < highscore)
        {
            highScoreData.Points = highscore;
            SaveHighScore();
        }
        currentScoreText.text = "Current score: " + currentscore.ToString();
        highScoreText.text = "High score: " + highscore.ToString();
        points = 0;
        currentscore = 0;
    }

    /// <summary>
    /// Executes everytime player makes a score in game
    /// </summary>
    private void IncrementScore()
    {
        points++;
        currentscore = points;
        score.text = "Score: " + points.ToString();

        if(CheckForHighScore())
            highscore = currentscore;
        //TODO: check if this is new high score, if is then set it and add some effect.
    }

    public void Pause()
    {
        SetUIsState(false, true, false, true);
        LevelManager.isPlaying = false;
        Time.timeScale = 0f;
    }

    public void OnContinue()
    {
        SetUIsState(false, false, false, true);
        LevelManager.isPlaying = true;
        Time.timeScale = 1f;
    }

    public void Reset()
    {
        // reset levelManager logic
        LevelManager.Instance.ResetLevel();
        
        // reset player position
        player.gameObject.transform.position = new Vector3(0,4,0);

        // reset UI
        SetUIsState(false, false, false, true);
        points = 0;
        score.text = "Score: " + points.ToString();
        Time.timeScale = 1f;
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;


        SceneManager.LoadScene("Home");
        LoadHighScore();
        GameOverUI.SetActive(false);
        HomeUI.SetActive(true);

    }

    /// <summary>
    /// Load high score from json if file exists
    /// </summary>
    private void LoadHighScore()
    {
        // Does the file exist?
        if (File.Exists(saveFile))
        {
            // Read the entire file and save its contents.
            string fileContents = File.ReadAllText(saveFile);

            // Deserialize the JSON data 
            //  into a pattern matching the GameData class.
            highScoreData = JsonUtility.FromJson<HighScore>(fileContents);
            tempHighScore = highScoreData.Points;
            highscoreMain.text = "High Score: " + highScoreData.Points.ToString();
        }
        else
        {
            tempHighScore = 0;
            highscoreMain.text = "High Score: " + highscore.ToString();
        }
    }

    /// <summary>
    /// Save high score instance to json file
    /// </summary>
    private void SaveHighScore()
    {
        // Serialize the object into JSON and save string.
        string jsonString = JsonUtility.ToJson(highScoreData);

        // Write JSON to file.
        File.WriteAllText(saveFile, jsonString);
    }

    private bool CheckForHighScore()
    {
        if (currentscore > highscore)
        {
            return true;
        }
        else
            return false;
    }


    public void SetUIsState(bool gameOverUI, bool pauseUI, bool homeUI, bool gamePlayUI)
    {
        GameOverUI.SetActive(gameOverUI);
        PauseUI.SetActive(pauseUI);
        HomeUI.SetActive(homeUI);
        GamePlayUI.gameObject.SetActive(gamePlayUI);
    }
}