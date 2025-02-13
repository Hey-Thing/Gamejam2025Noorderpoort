using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private int playerScore = 0;
    private string playerName = "";
    public HighScoreManager highScoreManager;

    public void SetPlayerName(string name)
    {
        playerName = name;
    }

    public void StartGame()
    {
        Debug.Log("Game Started for " + playerName);
    }

    public void AddScore(int points)
    {
        playerScore += points;
    }
        public void RemoveScore(int points)
    {
        playerScore -= points;
    }

    public void EndGame()
    {
        if (!string.IsNullOrEmpty(playerName))
        {
            highScoreManager.SaveNewScore(playerName, playerScore);
            Debug.Log("Score saved for " + playerName);
        }
    }

    public void TestAddScoreAndEndGame()
{
    AddScore(50); // Add 50 points for testing
    Debug.Log("Added 50 points. Current score: " + playerScore);
    EndGame();
}

}
