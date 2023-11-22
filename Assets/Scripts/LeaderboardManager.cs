using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using Dan.Main;
using UnityEngine.SocialPlatforms.Impl;
namespace LeaderboardCreatorDemo { 

public class LeaderboardManager : MonoBehaviour
{

    [SerializeField] private TMP_Text[] _entryTextObjects;
    [SerializeField] private TMP_InputField _usernameInputField;

    

    // Start is called before the first frame update
    void Start()
    {
        LoadEntries();
    }

    public void LoadEntries()   
    {
        Leaderboards.TestLeaderboard.GetEntries(entries =>
        {
            foreach (var t in _entryTextObjects)
                t.text = "";
            var length = Mathf.Min(_entryTextObjects.Length, entries.Length);
            for (int i = 0; i < length; i++)
                _entryTextObjects[i].text = $"{entries[i].Rank}. {entries[i].Username} - {entries[i].Score}";
        });
    }


    public void UploadEntry()
    {
            int score = Mathf.FloorToInt(PlayerPrefs.GetFloat("HighScore" + MenuManager.Instance.levelName));
        Leaderboards.TestLeaderboard.UploadNewEntry(_usernameInputField.text, score , isSuccessful =>
        {
            if (isSuccessful)
                LoadEntries();
        });
    }



}

}
