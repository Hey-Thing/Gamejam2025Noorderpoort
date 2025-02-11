using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;

public class JobMinigameVR : MonoBehaviour
{
    [SerializeField] private TMP_Text _number1Text;
    [SerializeField] private TMP_Text _number2Text;
    [SerializeField] private TMP_Text _feedbackText;
    [SerializeField] private TMP_Text _answerText;
    [SerializeField] private int _minValue = 1;
    [SerializeField] private int _maxValue = 50;

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
    }

    private int RandomNumber()
    {
        return Random.Range(_minValue, _maxValue + 1);
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
            yield return null;
        }
        _timeIsUp = true;
        CheckAnswer();
    }


    public void AppendDigit(int digit)
    {
        if (_userInput.Length < 5)
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

    public void CheckAnswer()
    {
        if (_timeIsUp) return;

        if (int.TryParse(_userInput, out int userAnswer))
        {
            if (userAnswer == _answer)
            {
                _feedbackText.text = "Correct!";
            }
            else
            {
                _feedbackText.text = "wrong" + _answer;
            }
        }
        else
        {
            _feedbackText.text = "invalid number!";
        }
    }

    public void NewEquation()
    {
        ClearInput();
        _feedbackText.text = "";
        GenerateEquation();
    }
}
