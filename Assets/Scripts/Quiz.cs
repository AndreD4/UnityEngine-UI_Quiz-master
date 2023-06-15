using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{

  [Header("Questions")]
  [SerializeField] TextMeshProUGUI questionText;
  [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
   QuestionSO currentQuestion;

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

  [Header("Scoreing")]
  [SerializeField] TextMeshProUGUI scoreText;
  ScoreKeeper scoreKeeper;


  void Start()
  {
    timer = FindObjectOfType<Timer>();
    scoreKeeper = FindObjectOfType<ScoreKeeper>();
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
    scoreText.text = "Score:" + scoreKeeper.CalculateScore() + "%";
  }

  void DisplayAnswer(int index)
  {
    Image buttonImage;

    if (index == currentQuestion.GetCorrectAnsweIndex())
    {
      questionText.text = "correct";
      buttonImage = answersButton[index].GetComponent<Image>();
      buttonImage.sprite = correctAnswerSprite;
      scoreKeeper.IncrementCorrectAnswers();
    }
    else
    {
      correctAnswerIndex = currentQuestion.GetCorrectAnsweIndex();
      string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
      questionText.text = "sorry, the correct answer was;\n" + correctAnswer;
      buttonImage = answersButton[correctAnswerIndex].GetComponent<Image>();
      buttonImage.sprite = correctAnswerSprite;
    }
  }

  void GetNextQuestion()
  {
    if(questions.Count > 0)
    {
    SetButtonState(true);
    SetDefaultButtonSprite();
    GetRandomQuestion();
    DisplayQuestion();
    scoreKeeper.IncrementQuestionSeen();
    }
  }

  void GetRandomQuestion()
  {
    int index = Random.Range(0, questions.Count);
    currentQuestion = questions[index];

    if(questions.Contains(currentQuestion))
    {
    questions.Remove(currentQuestion);
    }
  }


  void DisplayQuestion()
  {
    questionText.text = currentQuestion.GetQuestion();

    for (int i = 0; i < answersButton.Length; i++)
    {
      TextMeshProUGUI buttonText = answersButton[i].GetComponentInChildren<TextMeshProUGUI>();
      buttonText.text = currentQuestion.GetAnswer(i);
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
