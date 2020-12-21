using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : Singleton<UIController>
{
    [SerializeField] UICategoryList ui_caterogies;
    [SerializeField] UIQuestionGrid ui_questions;
    [SerializeField] UIQuestionPanel ui_panelQuestion;

    [SerializeField] UIHintPanel ui_hint;
    [SerializeField] Button backButton;

    private void Start()
    {
        backButton.onClick.AddListener(BackMenu);
    }

    public void ShowQuesitions(int category)
    {
        DataManager.Get.currentCategory = category;
        ui_caterogies.gameObject.SetActive(false);
        ui_questions.InitData();
        ui_questions.gameObject.SetActive(true);
        backButton.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(BackCategories);
    }

    internal void ShowPanelQuesition(int question)
    {
        DataManager.Get.currentQuestion = question;
        ui_questions.gameObject.SetActive(false);
        ui_panelQuestion.InitData();
        ui_panelQuestion.gameObject.SetActive(true);
        backButton.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(BackQuestions);
    }
    void BackMenu()
    {
        Destroy(this.gameObject);
        SceneManager.LoadScene(0);      
    }
    void BackCategories()
    {
        ui_panelQuestion.gameObject.SetActive(false);
        ui_questions.gameObject.SetActive(false);
        ui_caterogies.gameObject.SetActive(true);
        ui_caterogies.InitData();
        backButton.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(BackMenu);
    }
    void BackQuestions()
    {
        ui_panelQuestion.gameObject.SetActive(false);
        ui_caterogies.gameObject.SetActive(false);
        ui_questions.gameObject.SetActive(true);
        ui_questions.InitData();
        backButton.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(BackCategories);
    }

    public void ShowHintPanel()
    {
        ui_hint.gameObject.SetActive(true);
        ui_hint.UpdateData(DataManager.Get.Question().hints);
    }
}
