using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float timer;




    // Start is called before the first frame update
    void Start()
    {
        
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
}
