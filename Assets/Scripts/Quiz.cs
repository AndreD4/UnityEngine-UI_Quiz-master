using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Quiz : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO question;
    [SerializeField] GameObject[] answersButton;


    void Start()
    {
        questionText.text = question.GetQuestion();
    }

    
    void Update()
    {
        
    }
}
