using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    #region variables
    [Header("Questions and Answers")]
    public Question[] questions;
    private int answerIndex;
    public TextMeshProUGUI questionTextBox;
    public TextMeshProUGUI[] answerTextBoxes;    
    private List<int> questionIndex = new List<int>();
    [Space]
    [Header("Timer")]
    public TextMeshProUGUI timerTextBox;
    public float time = 10f;
    private float countdown;
    [HideInInspector]
    public bool timerStart = false;
    [Space]
    [Header("UI")]
    public GameObject UIPanel;
    public GameObject dialogueTextBox;
    [Space]
    [Header("Other")]
    public bool konechry = false;
    public int lives = 3;
    public GameObject KonechryText;
    #endregion

    private void Start()
    {
        countdown = time;
        for (int i = 0; i < questions.Length; i++)
        {
            questionIndex.Add(i);
        }

        GetNextQuestion();
    }    
    private void Update()
    {
        if (konechry != true)
        {
            if (timerStart)
            {
                if (countdown >= 0)
                {
                    countdown -= Time.deltaTime;
                    timerTextBox.text = Mathf.Round(countdown).ToString();
                }
                else
                {
                    lives--;
                    if (lives == 0)
                    {
                        konechry = true;
                        KonechryText.SetActive(true);
                        ToggleUI(false);
                    }
                    else
                    {
                        StartCoroutine(FailSequence());
                    }
                }
            }
        }
       
    }
    public void Button(int id)
    {
        if (answerIndex != id)
        {
            lives--;            
            if (lives == 0)
            {
                konechry = true;
                KonechryText.SetActive(true);
                ToggleUI(false);
            }
            else
            {
                StartCoroutine(FailSequence());
            }
        }

        if (questionIndex.Count != 0 && answerIndex == id)
        {
            countdown = time;
            GetNextQuestion();            
        }    
    }
    public void GetNextQuestion()
    {
        int x = Random.Range(0, questionIndex.Count);
        string chosenQuestion = questions[questionIndex[x]].question;
        string chosenAnswer = questions[questionIndex[x]].answer;
        questionTextBox.text = chosenQuestion;
        int random = Random.Range(0, 3);
        answerIndex = random;
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
    IEnumerator FailSequence()
    {
        timerStart = false;
        ToggleUI(false);
        Camera.main.GetComponent<Animator>().SetBool("Zoom", false);

        yield return new WaitForSeconds(1);
        GameObject.Find("teacher").GetComponent<Animator>().SetInteger("State", 3-lives);

        yield return new WaitForSeconds(2);
        Camera.main.GetComponent<Animator>().SetBool("Zoom", true);

        yield return new WaitForSeconds(1);
        ToggleUI(true);
        countdown = time;
        timerStart = true;
        if (questionIndex.Count != 0)
        {
            GetNextQuestion();
        }
    }
    public void CameraZoom(bool b)
    {
        Camera.main.GetComponent<Animator>().SetBool("Zoom", b);
    }
    public void ToggleUI(bool b)
    {
        UIPanel.SetActive(b);
    }
}

