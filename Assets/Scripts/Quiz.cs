using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
  [SerializeField] TextMeshProUGUI questionText;
  [SerializeField] QuestionSO question;
  [SerializeField] GameObject[] answersButton;
  int correctAnswerIndex;
  [SerializeField] Sprite defaultAnswerSprite;
  [SerializeField] Sprite correctAnswerSprite;


  void Start()
  {
    questionText.text = question.GetQuestion();

    for (int i = 0; i < answersButton.Length; ++i)
    {
      TextMeshProUGUI buttonText = answersButton[i].GetComponentInChildren<TextMeshProUGUI>();
      buttonText.text = question.GetAnswer(i);
    }
  }

  public void OnAnswerSelected(int index)
  {
    Image buttonImage;

    if(index == question.GetCorrectAnsweIndex())
    {
      questionText.text = "correct";
      buttonImage = answersButton[index].GetComponent<Image>();
      buttonImage.sprite = correctAnswerSprite;
    }
    else
    {
      correctAnswerIndex = question.GetCorrectAnsweIndex();
      string correctAnswer = question.GetAnswer(correctAnswerIndex);
      questionText.text = "sorry, the correct answer was;\n" + correctAnswer;
      buttonImage = answersButton[correctAnswerIndex].GetComponent<Image>();
      buttonImage.sprite = correctAnswerSprite;
    }
  }


  void Update()
  {

  }
}
