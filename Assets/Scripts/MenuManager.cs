using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public static MenuManager Instance;
    public string levelName;

    public int levelIndex;

    private void Awake()
    {
        Instance = this;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Next Scene");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
    public void RestartCurrentLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        Debug.Log("Restarting " + currentScene.name);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Debug.Log("Loading Menu");
    }
    public void SelectLevel(int level)
    {
        levelName = "Level " + level;
        SceneManager.LoadScene(levelName);
        Debug.Log("Loading " + levelName);
        levelIndex = level;
    }

    public void NextLevel()
    {
        SelectLevel(levelIndex + 1);
    }
}