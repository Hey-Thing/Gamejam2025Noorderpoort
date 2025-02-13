using UnityEngine;
using TMPro;

public class HighScoreDisplay : MonoBehaviour
{
    public GameController gameController;  // Reference to GameController
    public TMP_Text highScoreText;  // The TMP Text that will display the high scores
    public GameObject highScoreUI;  // The High Score UI to activate when the game ends

    // This will be called from the GameController when the game ends
    public void ShowHighScores()
    {
        var highScores = gameController.highScoreManager.GetHighScores();
        string displayText = "High Scores:\n\n";

        foreach (var entry in highScores)
        {
            displayText += $"{entry.playerName}: {entry.score}\n";
        }

        highScoreText.text = displayText;
        highScoreUI.SetActive(true);  // Show the High Score UI
    }

    // Optional: Close the High Score UI
    public void CloseHighScores()
    {
        highScoreUI.SetActive(false);  // Hide the High Score UI
    }
}
