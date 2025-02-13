using UnityEngine;

public class HighScoreDisplay : MonoBehaviour
{
    public HighScoreManager highScoreManager;

    void Start()
    {
        ShowHighScores();
    }

    void ShowHighScores()
    {
        foreach (var entry in highScoreManager.GetHighScores())
        {
            Debug.Log(entry.playerName + " - " + entry.score);
        }
    }
}
