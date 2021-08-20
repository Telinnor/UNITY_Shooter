using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    private Text ScoreText;
    private int score;
    void Start()
    {
        ScoreText = gameObject.GetComponent<Text>();
        ScoreText.text="Score: 0";
    }

    void Update()
    {
        
    }

    public void GetPoint(){
        score++;
        ScoreText.text="Score: "+score;
    }
}
