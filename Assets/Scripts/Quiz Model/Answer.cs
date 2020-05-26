using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[System.Serializable]
public class Answer
{
    [SerializeField] protected TypeAnswer type;
    [SerializeField] protected string answerText;
	[SerializeField] string[] falseOpitons = new string[3];
	public virtual void OnGUI(SerializedObject so)
	{
		so.Update();
		SerializedProperty answerProperty = so.FindProperty("answer");
		SerializedProperty answerType = answerProperty.FindPropertyRelative("type");
		SerializedProperty answertext = answerProperty.FindPropertyRelative("answerText");
		EditorGUILayout.PropertyField(answerType, true);
		EditorGUILayout.PropertyField(answertext, true); // True means show children
		so.ApplyModifiedProperties();
	}
	public virtual string GetTypeAnswer()
	{
		return type.ToString();
	}
	public Answer()
	{

	}

}

public enum TypeAnswer
{
    Input, Crossword, Options
}