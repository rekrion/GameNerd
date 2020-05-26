using System.Collections;
using System.Collections.Generic;
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

    public void OnEnable()
    {
        if (questions == null)
            questions = new List<Question>();
        //hideFlags = HideFlags.HideAndDontSave;
    }
}

public enum TypeQuestion
{
    Image,Text,Sound
}
