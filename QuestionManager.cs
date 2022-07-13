using System;
using TMPro;
using TutorialAssets.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuestionManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _questionTextField;
    [SerializeField] private TMP_InputField _answerInputField;
    [SerializeField] private TMP_Text _answerPlaceholderText;
    [SerializeField] private MonsterManager _monsterManager;
    
    [SerializeField] private float _answer;
    [SerializeField] private GameObject _player;
    
    // Start is called before the first frame update
    void Start()
    {
        GenerateQuestion();
    }

    void GenerateQuestion()
    {
        if (_monsterManager.IsMonsterListEmpty())
        {
            ClearInputField("You cleared the wave!");
            return;
        }
        
        MonsterType monsterType = _monsterManager.GetMonsterType(0);
        
        ClearInputField("Enter your Answer");

        string question = "";
        
        switch (monsterType)
        {
            case MonsterType.Add:
                GenerateAddSubtractQuestion(100, out question, out _answer);
                break;
            case MonsterType.Multiply:
                GenerateMultiplyQuestion(50, 12, out question, out _answer);
                break;
            case MonsterType.Divide:
                GenerateDivisionQuestion(100, 10, out question, out _answer);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        _questionTextField.text = question;
        
        //Force focus to answer input field
        _answerInputField.ActivateInputField();

	Debug.Log(_answer);
    }

    void GenerateAddSubtractQuestion(int maxNumber, out string question, out float answer)
    {
        int operand1 = Mathf.FloorToInt(Random.Range(1,maxNumber));
        int operand2 = Mathf.FloorToInt(Random.Range(1,maxNumber));

        //Is it a + or - question, creates 50% chance
        if (Random.value < 0.5f)
        {
            question = $"{operand1} - {operand2} = ?";
            answer =  (operand1 - operand2);
        }
        else
        {
            question = $"{operand1} + {operand2} = ?";
            answer =  (operand1 + operand2);
        }
    }

    void GenerateMultiplyQuestion(int maxNumber, int maxMultiply, out string question, out float answer)
    {
        int operand1 = Mathf.FloorToInt(Random.Range(0,maxNumber));
        int operand2 = Random.Range(2, maxMultiply);
        
        question = $"{operand1} * {operand2} = ?";
        answer = operand1 * operand2;
    }
    
    void GenerateDivisionQuestion(int maxNumber, int maxdivision, out string question, out float answer)
    {
        int operand1 = Mathf.FloorToInt(Random.Range(1,maxNumber));
        int operand2 = Random.Range(2, maxdivision);
        
        question = $"{operand1} / {operand2} = ?";
        answer = (float)operand1 / operand2;
        
        string stringAnswer = answer.ToString("0.0");
        answer = float.Parse(stringAnswer);
    }

    public void CheckAnswerCorrect()
    {
        if (_answerInputField.text == _answer.ToString())
        {
            // _monsterManager.KillMonster(0);
            // _monsterManager.MonsterAttacks(0);
            // _monsterManager.MoveNextMonsterToQueue();

            Health monsterHealth = _monsterManager._monsters[0].GetComponent<Health>();
            monsterHealth.TakeDamage(10);
            GenerateQuestion();
        }
        else
        {
            Health playerHealth = _player.GetComponent<Health>();
            playerHealth.TakeDamage(10);
            ClearInputField("Incorrect. Try again!");
            _answerInputField.ActivateInputField();
        }
    }

    private void ClearInputField(string placeholderText)
    {
        _answerInputField.text = "";
        _answerPlaceholderText.text = placeholderText;
    }
}
