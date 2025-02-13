using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class HighScoreDisplay : MonoBehaviour
{
    public GameController gameController;
    public TMP_Text highScoreText;
    public GameObject highScoreUI;

    public void ShowHighScores()
    {
        var highScores = gameController.highScoreManager.GetHighScores();
        highScoreText.text = FormatHighScores(highScores);
        highScoreUI.SetActive(true);
    }

    public void ShowHighScoresFromMainMenu()
    {
        var highScores = FindObjectOfType<HighScoreManager>().GetHighScores();
        highScoreText.text = FormatHighScores(highScores);
        highScoreUI.SetActive(true);
    }

    public void CloseHighScores()
    {
        highScoreUI.SetActive(false);
    }

    private string FormatHighScores(List<HighScoreEntry> highScores)
    {
        if (highScores == null || highScores.Count == 0)
            return "<b><size=36>No High Scores Yet!</size></b>";

        string displayText = "<b><size=40> High Scores </size></b>\n\n";
        displayText += "<b><size=32>Name           Score</size></b>\n";  // Header
        displayText += "---------------------------------\n";

        foreach (var entry in highScores)
        {
            string formattedName = entry.playerName.PadRight(15);  // Ensures names are always 15 chars wide
            string formattedScore = entry.score.ToString().PadRight(5);  // Left-align the score
            displayText += $"{formattedName} {formattedScore}\n";
        }

        return displayText;
    }
}
