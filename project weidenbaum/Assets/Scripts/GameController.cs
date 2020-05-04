using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    [Space]
    [Header("Game over UI")]
    public GameObject gameOverUI;
    public GameObject gameOverImg;
    public GameObject mistakesTextBox;
    public GameObject timeTextBox;
    public GameObject gradeTextBox;
    public GameObject menuBtn;
    public GameObject replayBtn;
    public GameObject nextBtn;
    [Space]
    [Header("Other")]
    public bool konechry = false;
    public int lives = 3;
    public string[] hlasky;
    public GameObject HlaskyText;
    public TextMeshProUGUI PocetZivotu;
    public TextMeshProUGUI PocetZivotuText;
    #endregion

    private void Start()
    {
        PocetZivotuText.text = "Pocet zivotu: ";
        PocetZivotu.text = lives.ToString();
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
                    PocetZivotu.text = lives.ToString();
                    if (lives == 0)
                    {
                        konechry = true;
                        ToggleUI(false);
                        StartCoroutine(GameOverSequence());
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
            PocetZivotu.text = lives.ToString();
            if (lives == 0)
            {
                konechry = true;
                ToggleUI(false);
                StartCoroutine(GameOverSequence());
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
        int indexHlasky = Random.Range(0, hlasky.Length);
        HlaskyText.SetActive(true);
        HlaskyText.GetComponent<TextMeshProUGUI>().text = hlasky[indexHlasky];
        GameObject.Find("teacher").GetComponent<Animator>().SetInteger("State", 3-lives);
        yield return new WaitForSeconds(2);
        HlaskyText.SetActive(false);
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
    IEnumerator GameOverSequence()
    {
        gameOverUI.SetActive(true);
        Image r = gameOverUI.GetComponent<Image>();
        LeanTween.value(gameOverUI, 0, .6f, 1).setOnUpdate((float val) =>
        {
            Color c = r.color;
            c.a = val;
            r.color = c;
        });
        LeanTween.moveLocalY(gameOverImg, 0, 1).setDelay(1);
        yield return new WaitForSeconds(2.5f);
        mistakesTextBox.SetActive(true);
        yield return new WaitForSeconds(.5f);
        timeTextBox.SetActive(true);
        yield return new WaitForSeconds(1);
        gradeTextBox.SetActive(true);
        yield return new WaitForSeconds(.5f);
        menuBtn.SetActive(true);
        yield return new WaitForSeconds(.5f);
        replayBtn.SetActive(true);
        yield return new WaitForSeconds(.5f);
        nextBtn.SetActive(true);
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

