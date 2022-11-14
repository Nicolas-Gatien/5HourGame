using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static int score;
    int displayScore;
    public TextMeshProUGUI text;

    private void Start()
    {
        score = 0;        
    }

    // Update is called once per frame
    void Update()
    {
        if (score > displayScore)
        {
            displayScore++;
        }

        text.text = displayScore.ToString();
    }

    public static void AddScore(int add)
    {
        score += add;
    }
}
