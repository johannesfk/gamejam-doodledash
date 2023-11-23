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

    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI timerText;
    public float timer;
    [SerializeField]
    public GameObject pauseScreen;

    [SerializeField]
    public GameObject levelComplete;


    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        pauseScreen.SetActive(false);
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
        //Pause();
        Debug.Log("hotkey: pause");
        transition.SetTrigger("Start");

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
        Time.timeScale = 0;
        levelComplete.SetActive(true);
        CheckHighScore();
        highScoreText.text = $"HighScore: {PlayerPrefs.GetFloat("HighScore", 0)}";
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
