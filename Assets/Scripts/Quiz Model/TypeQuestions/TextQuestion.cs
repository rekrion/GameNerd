using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class TextQuestion : Question
{
    [SerializeField]
    private string text;

    [HideInInspector] public string Text => text;
    public override void OnGUI()
    {
        type = TypeQuestion.Text;
        text = EditorGUILayout.TextField("Text",text);
        base.OnGUI();
    }
    public TextQuestion()
    {

    }
}