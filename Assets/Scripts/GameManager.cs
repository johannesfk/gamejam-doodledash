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

    private TextMeshProUGUI highScoreText;
    private TextMeshProUGUI timerText;
    private TextMeshProUGUI sessionTimeText;
    public float timer;
    [SerializeField]
    public GameObject pauseScreen;

    [SerializeField]
    public GameObject levelComplete;

    private float score = 0f;

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
        timerText = GameObject.Find("CurrentTime").GetComponent<TextMeshProUGUI>();
        sessionTimeText = GameObject.Find("SessionTime").GetComponent<TextMeshProUGUI>();


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

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    void Update()
    {
        /* int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60); */
        timerText.text = FormatTime(timer);
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
        pauseScreen.SetActive(false);
        Debug.Log("Game is resumed");
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

    /// <summary>
    /// Waits for the animation to finish before setting the level complete screen to active.
    /// This is done so that the animation can finish before the level complete screen is shown.
    /// </summary>
    IEnumerator WaitForLoadingAnimation()
    {
        // Wait until the animation state is "Erase Transition" and the animation has finished
        while (!(transition.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && transition.GetCurrentAnimatorStateInfo(0).IsName("Erase Transition")))
        {
            yield return null;
        }

        Debug.Log("Transition is done");
        levelComplete.SetActive(true);
    }
    void StartLoadingAnimation()
    {
        StartCoroutine(WaitForLoadingAnimation());
    }
    public void LevelComplete()
    {
        Debug.Log("Level Complete");
        score = timer;
        levelLoader.SetActive(true);
        transition.SetTrigger("Start");

        StartLoadingAnimation();

        StartCoroutine(CheckHighScore());


        if (highScoreText == null)
        {
            Debug.LogError("highScoreText is null");
        }
        else
        {
            highScoreText.text = $"HighScore: {PlayerPrefs.GetFloat("HighScore-" + SceneManager.GetActiveScene().name, 0)}";
            sessionTimeText.text = FormatTime(score);
        }

        PlayerPrefs.SetFloat("float 1", score);
        Debug.Log(PlayerPrefs.GetFloat("float 1", 0));
    }

    IEnumerator CheckHighScore()
    {
        Debug.Log("Checking High Score");
        Debug.Log("Score " + score);
        if (!PlayerPrefs.HasKey("HighScore-" + SceneManager.GetActiveScene().name))
        {
            Debug.Log("Creating key");
            PlayerPrefs.SetFloat("HighScore-" + SceneManager.GetActiveScene().name, 999999999);
            Debug.Log("Key set: " + PlayerPrefs.GetFloat("HighScore-" + SceneManager.GetActiveScene().name, 999999999));
        }
        else
        {
            if (score < PlayerPrefs.GetFloat("HighScore-" + SceneManager.GetActiveScene().name, 999999999))
            {
                Debug.Log("New high score: " + score + " < " + PlayerPrefs.GetFloat("HighScore-" + SceneManager.GetActiveScene().name, 999999999));
                PlayerPrefs.SetFloat("HighScore-" + SceneManager.GetActiveScene().name, score);
                Debug.Log("Key set: " + PlayerPrefs.GetFloat("HighScore-" + SceneManager.GetActiveScene().name, 999999999));
            }
            else
            {
                Debug.Log("No new high score " + score);
                Debug.Log("High score " + PlayerPrefs.GetFloat("HighScore-" + SceneManager.GetActiveScene().name, 999999999));
            }
        }

        PlayerPrefs.Save();
        yield return null;
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
