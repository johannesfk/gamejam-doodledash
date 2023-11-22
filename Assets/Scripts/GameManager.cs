using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float timer;

    public GameObject pauseScreen;


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
       EndLevel();

    }

    public void EndLevel()
    {
        Pause();
        //EndScreen.setactive(true);
        
;
        CheckHighScore();
        highScoreText.text = $"HighScore: {PlayerPrefs.GetFloat("HighScore", 0)}";

    }

    void CheckHighScore()
    {
        if (timer < PlayerPrefs.GetFloat("HighScore" + MenuManager.Instance.levelName , 0))
        {
            PlayerPrefs.SetFloat("HighScore" + MenuManager.Instance.levelName, timer);
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
