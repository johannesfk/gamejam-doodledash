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
    private GameObject baseUI;
    private TextMeshProUGUI highScoreText;
    private TextMeshProUGUI timerText;
    private TextMeshProUGUI sessionTimeText;
    public float timer;
    [SerializeField]
    public GameObject pauseScreen;

    [SerializeField]
    public GameObject levelComplete;

    private float score = 0f;

    private Dictionary<string, string> levelMap;

    TextMeshProUGUI userNameText;

    Leaderboards leaderboards;


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
        levelMap = new Dictionary<string, string>
        {
            { "Level 1", "lv1" },
            { "Level 2", "lv2" },
            { "Level 3", "lv3" },
            { "Level 4", "lv4" },
            { "Level 5", "lv5" },
            { "Level 6", "lv6" },
            { "Level 7", "lv7" },
            { "Level 8", "lv8" }
        };
        levelLoader = GameObject.Find("LevelLoader");
        baseUI = GameObject.Find("Base Level UI");
        transition = levelLoader.GetComponentInChildren<Animator>();
        highScoreText = GameObject.Find("HighScoreText").GetComponent<TextMeshProUGUI>();
        timerText = GameObject.Find("CurrentTime").GetComponent<TextMeshProUGUI>();
        sessionTimeText = GameObject.Find("SessionTime").GetComponent<TextMeshProUGUI>();

        userNameText = GameObject.Find("UserName").GetComponent<TextMeshProUGUI>();

        userNameText.text = PlayerPrefs.GetString("User_name", "");

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

        leaderboards = GameObject.Find("Leaderboards").GetComponent<Leaderboards>();
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
    string FormatTimeMillisec(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt(time % 1 * 1000);
        return string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);
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
        FindObjectOfType<AudioManager>().Play("WinSound");
        levelLoader.SetActive(true);
        baseUI.SetActive(false);
        transition.SetTrigger("Start");
        FindObjectOfType<AudioManager>().Play("Eraser");

        StartLoadingAnimation();

        StartCoroutine(CheckHighScore());


        if (highScoreText == null)
        {
            Debug.LogError("highScoreText is null");
        }
        else
        {
            highScoreText.text = $"Highscore: {FormatTimeMillisec(PlayerPrefs.GetFloat("HighScore-" + SceneManager.GetActiveScene().name, 0))}";
            sessionTimeText.text = $"Your time: {FormatTimeMillisec(score)}";
        }
    }

    IEnumerator CheckHighScore()
    {
        Debug.Log("Checking High Score");
        Debug.Log("Score " + score);
        Debug.Log("Adding score: " + score + " to leaderboard: " + levelMap[SceneManager.GetActiveScene().name]);
        leaderboards.AddScore(levelMap[SceneManager.GetActiveScene().name], score);
        if (!PlayerPrefs.HasKey("HighScore-" + SceneManager.GetActiveScene().name))
        {
            Debug.Log("Creating key");
            PlayerPrefs.SetFloat("HighScore-" + SceneManager.GetActiveScene().name, 999999999);
            Debug.Log("Key set: " + PlayerPrefs.GetFloat("HighScore-" + SceneManager.GetActiveScene().name, 999999999));
            // leaderboards.instance.AddScore(levelMap[SceneManager.GetActiveScene().name], score);

            Debug.Log("Adding score: " + score + " to leaderboard: " + levelMap[SceneManager.GetActiveScene().name]);
            leaderboards.AddScore(levelMap[SceneManager.GetActiveScene().name], score);
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
