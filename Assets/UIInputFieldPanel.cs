using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInputFieldPanel : MonoBehaviour
{
    [SerializeField] UIQuestionPanel main;
    [SerializeField] Button buttonActivate;
    [SerializeField] Image panel;
    [SerializeField] InputField inputField;
    [Header("Colors")]
    [SerializeField] Color isSolved;
    [SerializeField] Color isStandart;
    [SerializeField] Color isFalsed;
    string answer;
    bool isBlocked = false;
    public void InitData(string answer)
    {
        if (isBlocked)
        {
            buttonActivate.interactable = false;
            buttonActivate.onClick.RemoveAllListeners();
        }
        else
        {
            this.answer = answer.ToUpper().Trim();
            buttonActivate.interactable = true;
            buttonActivate.onClick.AddListener(ButtonClick);
        }

    }
    public void ButtonClick()
    {
        string inputText = inputField.text.ToUpper().Trim();
        if (inputText == answer)
        {
            isBlocked = true;
            DataManager.Get.Question().isSolved = true;
            DataManager.Get.Save();
            panel.color = isSolved;
            main.InitData();
        }
        else
        {
            panel.color = isFalsed;
        }
        StartCoroutine(ActivateColor());
    }
    IEnumerator ActivateColor()
    {
        buttonActivate.interactable = false;
        yield return new WaitForSeconds(1f);
        panel.color = isStandart;
        buttonActivate.interactable = true;
        yield return null;
    }
}
