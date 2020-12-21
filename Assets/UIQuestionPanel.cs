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
    [Header("Is Solved")]
    [SerializeField] Image outlineSolved;
    [SerializeField] Color isSolved;
    [SerializeField] Color isStandart;


    [Header("Main Buttons")]
    [SerializeField] Button next;
    [SerializeField] Button last;
    [SerializeField] Button hint;

    [Header("Inputs")]
    [SerializeField] UICrosswordPanel crossword;
    [SerializeField] UIOptionalPanel options;
    [SerializeField] UIInputFieldPanel inputfield;

    QuestionInfo question;

    int issuedHints = 0;
    public bool IsSolved;
    internal void InitData()
    {
        question = DataManager.Get.Question();
        foreach(Hint hint in DataManager.Get.Question().hints)
        {
            if (hint.issued)
            {
                issuedHints++;
            }
        }
        Debug.Log($"Hints {issuedHints}");
        IsSolved = question.isSolved;
        ResetAll();
        ShowIsSolved();
        ShowTypeQuestion(question);
        ShowTypeInput(question);
        ShowButtons();
    }

    private void ShowIsSolved()
    {
        if (IsSolved)
            outlineSolved.color = this.isSolved;
        else
            outlineSolved.color = this.isStandart;
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
            case TypeQuestion.Sound: { soundQuestion.gameObject.SetActive(true);/**/ } break;
            case TypeQuestion.Text:  { textQuestion.gameObject.SetActive(true); textQuestion.text = contactInfo.text;  } break;
        }
    }

    void ShowTypeInput(QuestionInfo contactInfo)
    {
        switch (contactInfo.answer.type)
        {
            case TypeAnswer.Crossword: { crossword.gameObject.SetActive(true); crossword.InitData(question.answer.answerText); } break;
            case TypeAnswer.Input: { inputfield.gameObject.SetActive(true); inputfield.InitData(question.answer.answerText); } break;
            case TypeAnswer.Options: { options.gameObject.SetActive(true); options.InitData(question.answer.answerText, question.answer.falseOpitons); } break;
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
        if (issuedHints > 2) issuedHints = 2;
        Debug.Log($"Activate Hints {issuedHints}");
        DataManager.Get.Question().hints[issuedHints].issued = true;
        DataManager.Get.Save();
        UIController.Get.ShowHintPanel();
        issuedHints++;
    }
}
