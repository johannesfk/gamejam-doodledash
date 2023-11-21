using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
