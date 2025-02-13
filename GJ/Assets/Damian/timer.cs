using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float elapsedTime = 0f;
    private bool timerRunning = true;
    public bool isDayScene = true;

    void Update()
    {
        if (timerRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerUI();

            if (elapsedTime >= 60f)
            {
                timerRunning = false;
                SwitchScene();
            }
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    void SwitchScene()
    {
        if (isDayScene)
        {
            SceneManager.LoadScene("Office");
        }
        else
        {
            SceneManager.LoadScene("MainMenuTest");
        }
    }
}
