using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainUIHandler : MonoBehaviour
{
    [Header("UI Buttons")]
    public GameObject MainMenu;
    public GameObject NameInput;
    public GameObject HighScore;

    [Header("Name Input")]
    public TMP_InputField nameInputField;
    public Button startButton;
    public GameController gameController;
    private string playerName = "";

    private void Start()
    {
        startButton.onClick.AddListener(StartGame);
    }

    public void AddLetter(string letter)
    {
        nameInputField.text += letter; // Append letter to input field
    }

    public void DeleteLetter()
    {
        if (nameInputField.text.Length > 0)
        {
            nameInputField.text = nameInputField.text.Substring(0, nameInputField.text.Length - 1);
        }
    }

    public void StartGame()
    {
        if (!string.IsNullOrEmpty(nameInputField.text))
        {
            playerName = nameInputField.text;
            gameController.SetPlayerName(playerName);
            gameController.StartGame();

        }
    }
        public void StartBeforeName()
    {
        MainMenu.SetActive(false);
        NameInput.SetActive(true);
    }
        public void AddSpace()
    {
        nameInputField.text += " "; // Append space to input field
    }

        public void OpenHighscore()
    {
        MainMenu.SetActive(false);
        HighScore.SetActive(true);
    }

    public void CloseHighScores()
    {
        HighScore.SetActive(false);
        MainMenu.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
