using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Quiz Model/Question")]
[System.Serializable]
public class Question : ScriptableObject
{
    [SerializeField] protected TypeQuestion type;
    [SerializeField] protected Answer answer;
	[SerializeField] protected InputQuestion inputAnswer;
	[SerializeField] protected bool isSolved;
    [SerializeField] public Hint[] hints = new Hint[3];


	const int spaceLevel = 15;
	bool showAnswer = false;
	bool showInputAnswer = false;
	public void OnEnable()
	{
		//hideFlags = HideFlags.HideInHierarchy;
	}

	public virtual void OnGUI()
	{
		ScriptableObject target = this;
		SerializedObject so = new SerializedObject(target);
		OnGUIShowInputAnswer(so);
		OnGUIShowAnswer(so);
		OnGUIShowHints(so);
		isSolved = EditorGUILayout.Toggle("Is Solved", isSolved);
		so.ApplyModifiedProperties();
	}

	private void OnGUIShowInputAnswer(SerializedObject so)
	{
		showInputAnswer = EditorGUILayout.Foldout(showInputAnswer, $"InputAnswer (Type - {12/*answer.GetTypeAnswer()*/})", EditorStyles.foldout);
		if (showInputAnswer)
		{
			EditorGUI.indentLevel++;
			if (inputAnswer != null)
				inputAnswer.OnGUI();
			OnGUIShowButtons();
			EditorGUI.indentLevel--;
		}
	}

	private void OnGUIShowButtons()
	{
		GUILayout.BeginHorizontal();
		GUILayout.Space(EditorGUI.indentLevel * spaceLevel);
		if (GUILayout.Button("Add"))
		{
			AddSubAssetAtIndex("qeusito1");
		}
		GUILayout.EndHorizontal();
	}
	void AddSubAssetAtIndex(string name)
	{
		InputQuestion newAsset = new InputQuestion();
		newAsset.name = name;
		AssetDatabase.AddObjectToAsset(newAsset, this);
		AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(newAsset));
		this.inputAnswer = newAsset;
		Debug.Log(AssetDatabase.GetAssetPath(this));
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
	}
	private void OnGUIShowAnswerButtons()
	{
		GUILayout.BeginHorizontal();
		GUILayout.Space(EditorGUI.indentLevel * spaceLevel);
		if (GUILayout.Button("Input"))
		{
			answer = new InputAnswer();
		}
		if (GUILayout.Button("Option"))
		{
			answer = new OptionAnswer();
		}
		if (GUILayout.Button("Crossword"))
		{
			answer = new CrosswordAnswer();
		}
		GUILayout.EndHorizontal();
	}


	private void OnGUIShowAnswer(SerializedObject so)
	{
		showAnswer = EditorGUILayout.Foldout(showAnswer, $"Answer (Type - {answer.GetTypeAnswer()})", EditorStyles.foldout);
		if (showAnswer)
		{
			EditorGUI.indentLevel++;
			if (answer != null)
				answer.OnGUI(so);
			OnGUIShowAnswerButtons();
			EditorGUI.indentLevel--;
		}
	}

	private void OnGUIShowHints(SerializedObject so)
	{
		SerializedProperty stringsProperty = so.FindProperty("hints");
		EditorGUILayout.PropertyField(stringsProperty, true); // True means show children
	}
	public string GetTypeQuestion()
	{
		return type.ToString();
	}

	public Question()
	{

	}

}
