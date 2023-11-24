using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using UnityEngine;

public class Leaderboards : MonoBehaviour
{
  // string LeaderboardId = "lv1";
  string VersionId { get; set; }
  int Offset { get; set; }
  int Limit { get; set; }
  int RangeLimit { get; set; }
  List<string> FriendIds { get; set; }

  public static Leaderboards instance;

  public float highScore;

  async void Awake()
  {
    if (instance != null)
    {
      Destroy(gameObject);
    }
    else
    {
      instance = this;

      DontDestroyOnLoad(gameObject);
    }
    await UnityServices.InitializeAsync();

    await SignInAnonymously();
  }

  async Task SignInAnonymously()
  {
    AuthenticationService.Instance.SignedIn += () =>
    {
      Debug.Log("Signed in as: " + AuthenticationService.Instance.PlayerId + " with display name: " + AuthenticationService.Instance.PlayerName);
    };
    AuthenticationService.Instance.SignInFailed += s =>
    {
      // Take some action here...
      Debug.Log(s);
    };

    await AuthenticationService.Instance.SignInAnonymouslyAsync();

    string PlayerName = PlayerPrefs.GetString("User_name", "");
    if (PlayerName != "")
    {
      await AuthenticationService.Instance.UpdatePlayerNameAsync(PlayerName);
    }
  }

  public async void AddScore(string LeaderboardId, float score)
  {
    Debug.Log("Adding score to cloud");
    var scoreResponse = await LeaderboardsService.Instance.AddPlayerScoreAsync(LeaderboardId, score);
    Debug.Log(JsonConvert.SerializeObject(scoreResponse));
  }

  public async void GetScores(string LeaderboardId)
  {
    var scoresResponse =
        await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId);
    Debug.Log(JsonConvert.SerializeObject(scoresResponse));
  }

  public async void GetPaginatedScores(string LeaderboardId, int Offset, int Limit)
  {
    /* Offset = 10;
    Limit = 10; */
    var scoresResponse =
        await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId, new GetScoresOptions { Offset = Offset, Limit = Limit });
    Debug.Log(JsonConvert.SerializeObject(scoresResponse));
  }

  public async void GetTopScore(string LeaderboardId)
  {
    var scoresResponse =
        await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId, new GetScoresOptions { Limit = 1 });
    highScore = (float)scoresResponse.Results[0].Score;
    Debug.Log(JsonConvert.SerializeObject(scoresResponse));
  }

  public async void GetPlayerScore(string LeaderboardId)
  {
    var scoreResponse =
        await LeaderboardsService.Instance.GetPlayerScoreAsync(LeaderboardId);
    Debug.Log(JsonConvert.SerializeObject(scoreResponse));
  }

  public async void GetPlayerRange(string LeaderboardId)
  {
    var scoresResponse =
        await LeaderboardsService.Instance.GetPlayerRangeAsync(LeaderboardId, new GetPlayerRangeOptions { RangeLimit = RangeLimit });
    Debug.Log(JsonConvert.SerializeObject(scoresResponse));
  }

  public async void GetScoresByPlayerIds(string LeaderboardId)
  {
    var scoresResponse =
        await LeaderboardsService.Instance.GetScoresByPlayerIdsAsync(LeaderboardId, FriendIds);
    Debug.Log(JsonConvert.SerializeObject(scoresResponse));
  }

  // If the Leaderboard has been reset and the existing scores were archived,
  // this call will return the list of archived versions available to read from,
  // in reverse chronological order (so e.g. the first entry is the archived version
  // containing the most recent scores)
  public async void GetVersions(string LeaderboardId)
  {
    var versionResponse =
        await LeaderboardsService.Instance.GetVersionsAsync(LeaderboardId);

    // As an example, get the ID of the most recently archived Leaderboard version
    VersionId = versionResponse.Results[0].Id;
    Debug.Log(JsonConvert.SerializeObject(versionResponse));
  }

  public async void GetVersionScores(string LeaderboardId)
  {
    var scoresResponse =
        await LeaderboardsService.Instance.GetVersionScoresAsync(LeaderboardId, VersionId);
    Debug.Log(JsonConvert.SerializeObject(scoresResponse));
  }

  public async void GetPaginatedVersionScores(string LeaderboardId)
  {
    Offset = 10;
    Limit = 10;
    var scoresResponse =
        await LeaderboardsService.Instance.GetVersionScoresAsync(LeaderboardId, VersionId, new GetVersionScoresOptions { Offset = Offset, Limit = Limit });
    Debug.Log(JsonConvert.SerializeObject(scoresResponse));
  }

  public async void GetPlayerVersionScore(string LeaderboardId)
  {
    var scoreResponse =
        await LeaderboardsService.Instance.GetVersionPlayerScoreAsync(LeaderboardId, VersionId);
    Debug.Log(JsonConvert.SerializeObject(scoreResponse));
  }
}
