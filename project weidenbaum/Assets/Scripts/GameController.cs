using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public Question[] questions;
    public int zivoty = 3;

    public TextMeshProUGUI otazka;
    public TextMeshProUGUI[] odpovedi;

    public TextMeshProUGUI timer;
    public float timeStart = 10f; 
    
    private void Start()
    {
        Otazky();
    }

    private void Update()
    {
        if (timeStart >= 0)
        {
            timeStart -= Time.deltaTime;
            timer.text = Mathf.Round(timeStart).ToString();
        } else
        {
            Otazky();
            timeStart = 10f;
        }
    }

    public void Otazky()
    {
        List<int> indexy = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            indexy.Add(i);
        }
        int x = Random.Range(0,questions.Length);
        string zvolenaotazka = questions[x].question;
        string sodpvoved = questions[x].answer;
        otazka.text = zvolenaotazka;
        int random = Random.Range(0, 3);
        indexy.RemoveAt(random);
        odpovedi[random].text = sodpvoved;
        
        for (int i = 0; i < 3; i++)
        {
                odpovedi[indexy[i]].text = questions[x].wrongAnswers[i];
        }
        
    }
}

