using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour
{

    private AudioManager audioManager;
    [SerializeField] private string songName;

    // Start is called before the first frame update
    void Awake()
    {
        audioManager = GetComponent<AudioManager>();
        audioManager.Play(songName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
