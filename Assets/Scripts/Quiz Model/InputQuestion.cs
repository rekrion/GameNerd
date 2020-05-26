using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Answer", menuName = "Quiz Model/X answer")]
[System.Serializable]
public class InputQuestion : ScriptableObject
{
	[SerializeField] protected TypeAnswer type;
	[SerializeField] protected string answerText;
	[SerializeField] string[] falseOpitons = new string[3];
	public virtual void OnGUI()
	{
		ScriptableObject target = this;
		SerializedObject so = new SerializedObject(target);
		SerializedProperty faflseOptProperty = so.FindProperty("falseOpitons");
		SerializedProperty textProperty = so.FindProperty("answerText");
		SerializedProperty typeProperty = so.FindProperty("type");
		so.ApplyModifiedProperties();

		EditorGUILayout.PropertyField(faflseOptProperty, true);
		EditorGUILayout.PropertyField(textProperty, true);
		EditorGUILayout.PropertyField(typeProperty, true);
	}
	public virtual string GetTypeAnswer()
	{
		return type.ToString();
	}
	public InputQuestion()
	{

	}
}
