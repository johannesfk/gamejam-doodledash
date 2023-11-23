using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Animator transition;
    private GameObject levelLoader;

    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI timerText;
    public float timer;
    [SerializeField]
    public GameObject pauseScreen;

    [SerializeField]
    public GameObject levelComplete;

    private void Awake()
    {
        /* if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        } */
        Instance = this;
    }
    private void Start()
    {
        levelLoader = GameObject.Find("LevelLoader");
        transition = levelLoader.GetComponentInChildren<Animator>();
        highScoreText = GameObject.Find("HighScoreText").GetComponent<TextMeshProUGUI>();
        timerText = GameObject.Find("CurrentTimer").GetComponent<TextMeshProUGUI>();

        pauseScreen = GameObject.Find("PauseMenu");
        levelComplete = GameObject.Find("LevelCompleteMenu");

        if (transition == null)
        {
            Debug.LogError("No GameObject named 'EraserWipe' found in the scene or it doesn't have an Animator component");
        }
        if (pauseScreen == null)
        {
            Debug.LogError("No GameObject named 'PauseMenu' found in the scene");
        }
        if (levelComplete == null)
        {
            Debug.LogError("No GameObject named 'LevelCompleteMenu' found in the scene");
        }

        pauseScreen.SetActive(false);
        levelComplete.SetActive(false);
        levelLoader.SetActive(false);
    }


    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

    }



    // Update is called once per frame
    void Update()
    {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        Debug.Log("Game is paused");
        pauseScreen.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        Debug.Log("Game is resumed");
        pauseScreen.SetActive(false);
    }

    public void OnPauseMenu(InputValue button)
    {
        Pause();
        Debug.Log("hotkey: pause");
    }

    public void EndLevel()
    {
        Pause();
        //EndScreen.setactive(true);

        ;
        CheckHighScore();
        //highScoreText.text = $"HighScore: {PlayerPrefs.GetFloat("HighScore", 0)}";
    }
    public void LevelComplete()
    {
        Debug.Log("Level Complete");
        levelLoader.SetActive(true);
        transition.SetTrigger("Start");
        // Time.timeScale = 0;

        if (transition.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && transition.GetCurrentAnimatorStateInfo(0).IsName("Erase Transition"))
        {
            Debug.Log("Transition is done");
            levelComplete.SetActive(true);
        }

        CheckHighScore();
        if (highScoreText == null)
        {
            Debug.LogError("highScoreText is null");
        }
        else
        {
            highScoreText.text = $"HighScore: {PlayerPrefs.GetFloat("HighScore", 0)}";
        }
    }

    void CheckHighScore()
    {
        if (timer < PlayerPrefs.GetFloat("HighScore" + SceneManager.GetActiveScene().name, 0))
        {
            PlayerPrefs.SetFloat("HighScore" + SceneManager.GetActiveScene().name, timer);
        }

    }

    bool GameHasEnded = false;
    public GameObject GameOverBackground;
    public void RestartGame()
    {
        if (GameHasEnded == true)
        {
            GameHasEnded = false;
            GameOverBackground.SetActive(false);
            Debug.Log("New Game");
        }
    }
}
