using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class JobMinigameVR : MonoBehaviour
{
    [SerializeField] private TMP_Text _number1Text;
    [SerializeField] private TMP_Text _number2Text;
    [SerializeField] private TMP_Text _feedbackText;
    [SerializeField] private TMP_Text _answerText;
    [SerializeField] private int _minValue = 1;
    [SerializeField] private int _maxValue = 50;
    [SerializeField] private TMP_Text _timerText;
   // [SerializeField] private GameObject Boss;
    public GameController gameController;  // Reference to GameController

    [SerializeField] private float _timeLimit = 10f;
    private float _timeRemaining;
    private bool _timeIsUp = false;

    private int _number1;
    private int _number2;
    private int _answer;
    private string _userInput = "";

    void Start()
    {
        GenerateEquation();
        //  Boss.SetActive(false);
        StartCoroutine(TimerCoroutine());

    }

    private int RandomNumber()
    {
        return Random.Range(_minValue, _maxValue + 1);
    }

    IEnumerator TimerCoroutine()
    {
        yield return new WaitForSeconds(120f);
        SceneManager.LoadScene("damian1");
    }
    private void GenerateEquation()
    {
        _number1 = RandomNumber();
        _number2 = RandomNumber();
        _answer = _number1 + _number2;

        _number1Text.text = _number1.ToString();
        _number2Text.text = _number2.ToString();
        _answerText.text = "";

        _timeRemaining = _timeLimit;
        _timeIsUp = false;
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        while (_timeRemaining > 0f && !_timeIsUp)
        {
            _timeRemaining -= Time.deltaTime;
            _timerText.text = "Time Left: " + Mathf.CeilToInt(_timeRemaining).ToString();
            yield return null;
        }

        if (!_timeIsUp)
        {
            _timeIsUp = true;
            CheckAnswer();
        }
    }

    public void AppendDigit(int digit)
    {
        if (_userInput.Length < 2)
        {
            _userInput += digit.ToString();
            _answerText.text = _userInput;
        }
    }

    public void ClearInput()
    {
        _userInput = "";
        _answerText.text = "";
    }
public void DeleteLetter()
{
    if (_userInput.Length > 0)
    {
        _userInput = _userInput.Substring(0, _userInput.Length - 1);
        _answerText.text = _userInput; // Update the display
    }
}

public void CheckAnswer()
{
    if (_timeIsUp)
    {
        if (int.TryParse(_userInput, out int userAnswer))
        {
            if (userAnswer == _answer)
            {
                _feedbackText.text = "Correct!";
                gameController.AddScore(10);
            }
            else
            {
                _feedbackText.text = "Incorrect! " + _answer;
                Debug.Log("Make Boss appear here...");
              //  Boss.SetActive(true);
                SceneManager.LoadScene("damian1");
            }
        }
        else
        {
            _feedbackText.text = "Invalid!";
            Debug.Log("Make Boss appear here...");
            SceneManager.LoadScene("damian1");
        }

        StartCoroutine(WaitBeforeNewEquation());
    }
}


    private IEnumerator WaitBeforeNewEquation()
    {
        yield return new WaitForSeconds(2f);
        NewEquation();
    }

    public void NewEquation()
    {
        ClearInput();
        _feedbackText.text = "";
        GenerateEquation();
    }
}
