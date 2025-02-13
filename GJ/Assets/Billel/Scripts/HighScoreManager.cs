using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class HighScoreEntry
{
    public string playerName;
    public int score;

    public HighScoreEntry(string name, int score)
    {
        this.playerName = name;
        this.score = score;
    }
}

public class HighScoreManager : MonoBehaviour
{
    private const string HighScoreKey = "HighScores";
    public int maxEntries = 5; // Change this to store more scores

    private List<HighScoreEntry> highScores = new List<HighScoreEntry>();

    void Start()
    {
        LoadHighScores();
    }

    public void SaveNewScore(string playerName, int score)
    {
        highScores.Add(new HighScoreEntry(playerName, score));

        // Sort scores in descending order & keep only top entries
        highScores = highScores.OrderByDescending(s => s.score).Take(maxEntries).ToList();

        SaveHighScores();
    }

    private void SaveHighScores()
    {
        string json = JsonUtility.ToJson(new HighScoreList(highScores));
        PlayerPrefs.SetString(HighScoreKey, json);
        PlayerPrefs.Save();
    }

    private void LoadHighScores()
    {
        if (PlayerPrefs.HasKey(HighScoreKey))
        {
            string json = PlayerPrefs.GetString(HighScoreKey);
            highScores = JsonUtility.FromJson<HighScoreList>(json).scores;
        }
    }

    public List<HighScoreEntry> GetHighScores()
    {
        return highScores;
    }
}

[System.Serializable]
public class HighScoreList
{
    public List<HighScoreEntry> scores;

    public HighScoreList(List<HighScoreEntry> scores)
    {
        this.scores = scores;
    }
}
