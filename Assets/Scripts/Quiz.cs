using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{

  [Header("Questions")]
  [SerializeField] TextMeshProUGUI questionText;
  [SerializeField] QuestionSO question;

  [Header("Answers")]
  [SerializeField] GameObject[] answersButton;
  int correctAnswerIndex;
  bool hasAnswerEarly;
  
  [Header("Sprites")]
  [SerializeField] Sprite defaultAnswerSprite;
  [SerializeField] Sprite correctAnswerSprite;

  [Header("Timer")]
  [SerializeField] Image timerImage;
  Timer timer;


  void Start()
  {
    timer = FindObjectOfType<Timer>();
     //GetNextQuestion();
    DisplayQuestion();
  }

  void Update()
  {
    timerImage.fillAmount = timer.fillFraction;
    if(timer.loadNextQuestion)
    {
      hasAnswerEarly = false;
      GetNextQuestion();
      timer.loadNextQuestion = false;
    }
    else if(!hasAnswerEarly && !timer.isAnsweringQuestion)
    {
      DisplayAnswer(-1);
      SetButtonState(false);
    }
  }


  public void OnAnswerSelected(int index)
  {
    hasAnswerEarly = true;
    DisplayAnswer(index);
    SetButtonState(false);
    timer.CancelTimer();
  }

  void DisplayAnswer(int index)
  {
    Image buttonImage;

    if (index == question.GetCorrectAnsweIndex())
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

  void GetNextQuestion()
  {
    SetButtonState(true);
    SetDefaultButtonSprite();
    DisplayQuestion();
  }


  void DisplayQuestion()
  {
    questionText.text = question.GetQuestion();

    for (int i = 0; i < answersButton.Length; i++)
    {
      TextMeshProUGUI buttonText = answersButton[i].GetComponentInChildren<TextMeshProUGUI>();
      buttonText.text = question.GetAnswer(i);
    }
  }

  void SetButtonState(bool state)
  {
    for(int i = 0; i < answersButton.Length; i++)
    {
      Button button = answersButton[i].GetComponent<Button>();
      button.interactable = state;
    }
  }

  void SetDefaultButtonSprite()
  {
    for(int i = 0; i < answersButton.Length; i++)
    {
      Image buttonImage = answersButton[i].GetComponent<Image>();
      buttonImage.sprite = defaultAnswerSprite;
    }
  }
}
