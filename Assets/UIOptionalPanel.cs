using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOptionalPanel : MonoBehaviour
{
    public struct OptionInput
    {
        public string text;
        public bool isCorrect;
    }

    [SerializeField] Button[] options;
    [SerializeField] UIQuestionPanel main;
    [Header("Is Solved")]
    [SerializeField] Color isSolved;
    [SerializeField] Color isStandart;
    [SerializeField] Color isFalsed;

    OptionInput[] optionInputs;

    const int count = 4;
    bool isBlocked = false;

    public void InitData(string answerOption, string[] falseOptions)
    {
        if(main.IsSolved)
        {
            isBlocked = true;
        }

        UpdateInput(answerOption, falseOptions);


        if (isBlocked)
        {
            ShowButtons();
        }
        else
        {
            SetupButtons();
        }
    }
    public void UpdateInput(string answerOption, string[] falseOptions)
    {
        ResetButtons();

        optionInputs = new OptionInput[4];

        optionInputs[0].text = answerOption;
        optionInputs[0].isCorrect = true;

        for (int i = 1; i < optionInputs.Length; i++)
        {
            optionInputs[i].text = falseOptions[i - 1];
            optionInputs[i].isCorrect = false;
        }
        Shuffle();
    }

    public void Shuffle()
    {
        for (int i = 0; i < optionInputs.Length; i++)
        {
            int rnd = Random.Range(0, optionInputs.Length);
            OptionInput tempGO = optionInputs[rnd];
            optionInputs[rnd] = optionInputs[i];
            optionInputs[i] = tempGO;
        }
    }
    public void SetupButtons()
    {
        for (int i = 0; i < optionInputs.Length; i++)
        {
            options[i].GetComponentInChildren<Text>().text = optionInputs[i].text;
            options[i].interactable = true;
            int closureIndex = i;
            if (optionInputs[i].isCorrect)
                options[i].onClick.AddListener(() => TrueClick(closureIndex));
            else
                options[i].onClick.AddListener(() => FalseClick(closureIndex));
        }
    }

    public void ShowButtons()
    {
        for (int i = 0; i < optionInputs.Length; i++)
        {
            options[i].GetComponentInChildren<Text>().text = optionInputs[i].text;
            options[i].interactable = false;
            if (optionInputs[i].isCorrect)
            {
                options[i].image.color = isSolved;
            }
                
        }
    }

    public void ResetButtons()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].onClick.RemoveAllListeners();
            options[i].image.color = isStandart;
        }
    }
    public void TrueClick(int index)
    {
        DataManager.Get.Question().isSolved = true;
        DataManager.Get.Save();
        options[index].image.color = isSolved;
        main.InitData();
    }

    public void FalseClick(int index)
    {
        options[index].image.color = isFalsed;
        isBlocked = true;
    }
}
