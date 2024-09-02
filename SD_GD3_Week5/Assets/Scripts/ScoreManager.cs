using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public TMP_Text scoreText;


    public void AddScore(int value)
    {

        score += value;
        UpdateScoreUI();

    }

    void UpdateScoreUI()
    {

        scoreText.text = "Score: " + score.ToString();
    }




}

