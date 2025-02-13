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
        SceneManager.LoadScene("Office");
    }

    public void AddScore(int points)
    {
        playerScore += points;
    }

    public void EndGame()
    {
        if (!string.IsNullOrEmpty(playerName))
        {
            highScoreManager.SaveNewScore(playerName, playerScore);
            Debug.Log("Score saved for " + playerName);
        }
    }
}
