using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : Singleton<UIController>
{
    [SerializeField] UICategoryList ui_caterogies;
    [SerializeField] UIQuestionGrid ui_questions;
    [SerializeField] UIQuestionPanel ui_panelQuestion;

    public void ShowQuesitions(int category)
    {
        DataManager.Get.currentCategory = category;
        ui_caterogies.gameObject.SetActive(false);
        ui_questions.InitData();
        ui_questions.gameObject.SetActive(true);
    }

    internal void ShowPanelQuesition(int question)
    {
        DataManager.Get.currentQuestion = question;
        ui_questions.gameObject.SetActive(false);
        ui_panelQuestion.InitData();
        ui_panelQuestion.gameObject.SetActive(true);
    }
}
