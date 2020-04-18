using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public Question[] questions;
    public int zivoty = 3;
    public List<int> indexyotazek = new List<int>();
    public TextMeshProUGUI otazka;
    public TextMeshProUGUI[] odpovedi;

    public TextMeshProUGUI timer;
    public float timeStart = 10f; 
    
    private void Start()
    {
        Otazky();
        GetNextQuestion();
    }

    private void Update()
    {
        if (indexyotazek.Count != 0)
        {

            if (timeStart >= 0)
            {
                timeStart -= Time.deltaTime;
                timer.text = Mathf.Round(timeStart).ToString();
            }
            else
            {
                GetNextQuestion();
                timeStart = 10f;
            }
        }
    }
    public void GetNextQuestion()
    {
        List<int> indexy = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            indexy.Add(i);
        }
        int x = Random.Range(0, indexyotazek.Count);
        string zvolenaotazka = questions[indexyotazek[x]].question;
        string sodpvoved = questions[indexyotazek[x]].answer;
        otazka.text = zvolenaotazka;
        int random = Random.Range(0, 3);
        indexy.RemoveAt(random);
        odpovedi[random].text = sodpvoved;
        for (int i = 0; i < 3; i++)
        {
            odpovedi[indexy[i]].text = questions[indexyotazek[x]].wrongAnswers[i];
        }
        indexyotazek.RemoveAt(x);
    }

    public void Otazky()
    {
        
        for (int i = 0; i < questions.Length; i++)
        {
            indexyotazek.Add(i);
        }
        
    }
}

