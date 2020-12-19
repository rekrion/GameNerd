using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName ="New Category", menuName = "Quiz Model/Category")]
[System.Serializable]
public class Category : ScriptableObject
{
    [SerializeField] string nameCategory;
    [SerializeField] Sprite icon;
    [HideInInspector]
    [SerializeField] public List<Question> questions;

    [HideInInspector] public string NameCategory => nameCategory;
    [HideInInspector] public Sprite Icon => icon;

    public void OnEnable()
    {
        if (questions == null)
            questions = new List<Question>();
        //hideFlags = HideFlags.HideAndDontSave;
    }
}

[System.Serializable]
public class CategoryInfo
{
    public string nameCategory;
    public Sprite icon;

    internal object GetSolvedCount()
    {
        return questions.Where(q => q.isSolved == true).ToArray().Length;
    }

    public List<QuestionInfo> questions;

    public CategoryInfo(Category category)
    {
        this.nameCategory = category.NameCategory;
        this.icon = category.Icon;
        this.questions = new List<QuestionInfo>();
        foreach(Question question in category.questions)
        {
            QuestionInfo questionInfo = new QuestionInfo();
            switch (question.Type)
            {
                case TypeQuestion.Image:
                    {
                        questionInfo = new QuestionInfo(question as ImageQuestion);
                    }break;
                case TypeQuestion.Text:
                    {
                        questionInfo = new QuestionInfo(question as TextQuestion);
                    }
                    break;
                case TypeQuestion.Sound:
                    {
                        questionInfo = new QuestionInfo(question as SoundQuestion);
                    }
                    break;
            }
            questions.Add(questionInfo);
        }
    }
}


public enum TypeQuestion
{
    Image,Text,Sound
}
