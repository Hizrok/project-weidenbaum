using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public Question[] questions;
    public int lives = 3;
    public List<int> questionIndex = new List<int>();
    public TextMeshProUGUI questionTextBox;
    public TextMeshProUGUI[] answerTextBoxes;

    public TextMeshProUGUI timer;
    public float start = 10f;
    private float timeStart; 
    
    private void Start()
    {
        timeStart = start;
        for (int i = 0; i < questions.Length; i++)
        {
            questionIndex.Add(i);
        }

        GetNextQuestion();
    }

    private void Update()
    {
        if (questionIndex.Count != 0)
        {

            if (timeStart >= 0)
            {
                timeStart -= Time.deltaTime;
                timer.text = Mathf.Round(timeStart).ToString();
            }
            else
            {
                GetNextQuestion();
                timeStart = start;
            }
        }
    }
    public void GetNextQuestion()
    {
        int x = Random.Range(0, questionIndex.Count);

        string chosenQuestion = questions[questionIndex[x]].question;
        string chosenAnswer = questions[questionIndex[x]].answer;
        questionTextBox.text = chosenQuestion;
        
        int random = Random.Range(0, 3);
        answerTextBoxes[random].text = chosenAnswer;

        for (int i = 0; i < 4; i++)
        {
            if (i > random)
            {
                answerTextBoxes[i].text = questions[questionIndex[x]].wrongAnswers[i-1];
            } else if (i < random)
            {
                answerTextBoxes[i].text = questions[questionIndex[x]].wrongAnswers[i];
            }
        }
        questionIndex.RemoveAt(x);
    }
}

