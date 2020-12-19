using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIQuestionPanel : MonoBehaviour
{
    [Header("Question")]
    [SerializeField] Image imageQuestion;
    [SerializeField] Text textQuestion;
    [SerializeField] GameObject soundQuestion;

    [SerializeField] Image outlineSolved;

    [Header("Main Buttons")]
    [SerializeField] Button next;
    [SerializeField] Button last;
    [SerializeField] Button hint;

    [Header("Inputs")]
    [SerializeField] GameObject crossword;
    [SerializeField] GameObject options;
    [SerializeField] GameObject inputfield;

    QuestionInfo question;
    internal void InitData()
    {
        question = DataManager.Get.Question();
        ResetAll();
        ShowTypeQuestion(question);
        ShowTypeInput(question);
        ShowButtons();
    }



    void ResetAll()
    {
        imageQuestion.gameObject.SetActive(false);
        textQuestion.gameObject.SetActive(false);
        soundQuestion.gameObject.SetActive(false);

        crossword.gameObject.SetActive(false);
        options.gameObject.SetActive(false);
        inputfield.gameObject.SetActive(false);
    }

    void ShowTypeQuestion(QuestionInfo contactInfo)
    {
        switch (contactInfo.type)
        {
            case TypeQuestion.Image: { imageQuestion.gameObject.SetActive(true); imageQuestion.sprite = contactInfo.sprite; } break;
            case TypeQuestion.Sound: { imageQuestion.gameObject.SetActive(true); textQuestion.text = contactInfo.text; } break;
            case TypeQuestion.Text: { soundQuestion.gameObject.SetActive(true);/**/ } break;
        }
    }

    void ShowTypeInput(QuestionInfo contactInfo)
    {
        switch (contactInfo.answer.type)
        {
            case TypeAnswer.Crossword: { crossword.gameObject.SetActive(true);/**/ } break;
            case TypeAnswer.Input: { inputfield.gameObject.SetActive(true); /**/ } break;
            case TypeAnswer.Options: { options.gameObject.SetActive(true);/**/ } break;
        }
        
    }

    void ShowButtons()
    {
        if (DataManager.Get.IsStartQuestion())
            last.gameObject.SetActive(false);
        else
            last.gameObject.SetActive(true);

        if (DataManager.Get.IsLastQuestion())
            next.gameObject.SetActive(false);
        else
            next.gameObject.SetActive(true);
    }

    public void NextQuestion()
    {
        DataManager.Get.currentQuestion++;
        InitData();
    }
    public void LastQuestion()
    {
        DataManager.Get.currentQuestion--;
        InitData();
    }
    public void ShowHint()
    {

    }
}
