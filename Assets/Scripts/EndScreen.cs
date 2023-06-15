using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
  [SerializeField] TextMeshProUGUI FinalScoreText;
  ScoreKeeper scoreKeeper;
    void Start()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void ShowFinalScore()
    {
      FinalScoreText.text = "Congratulation!\nYou got a score of" + scoreKeeper.CalculateScore() + "%";
    }

    
}
