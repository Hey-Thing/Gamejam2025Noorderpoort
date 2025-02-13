using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController instance;
    private int playerScore = 0;
    private string playerName = "";
    public HighScoreManager highScoreManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates when reloading scenes
        }
    }

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
