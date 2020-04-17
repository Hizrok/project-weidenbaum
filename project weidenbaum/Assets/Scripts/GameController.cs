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
    private void Start()
    {
        Otazky();
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

