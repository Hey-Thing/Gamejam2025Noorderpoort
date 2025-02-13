using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VRNameInputManager : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public Button startButton;
    public GameController gameController;
    private string playerName = "";

    private void Start()
    {
        startButton.onClick.AddListener(StartGame);
    }

    public void SetPlayerName(string name)
    {
        playerName = name;
    }

    void StartGame()
    {
        if (!string.IsNullOrEmpty(nameInputField.text))
        {
            playerName = nameInputField.text;
            gameController.SetPlayerName(playerName); // Send name to GameController
            gameObject.SetActive(false); // Hide name input UI
        }
    }
}
