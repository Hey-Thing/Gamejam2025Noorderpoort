using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainUIHandler : MonoBehaviour
{
    [Header("UI Buttons")]
    public GameObject MainMenu;
    public GameObject NameInput;

    [Header("Name Input")]
    public TMP_InputField nameInputField;  // The name input field
    public Button startButton;  // Start button to begin the game
    public GameController gameController;  // Reference to GameController
    private string playerName = "";  // Stores player's name

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
            gameController.SetPlayerName(playerName); // Send name to GameController
            //SWAP SCENE HERE
        }
    }
        public void StartBeforeName()
    {
        MainMenu.SetActive(false);
        NameInput.SetActive(true);
    }
}
