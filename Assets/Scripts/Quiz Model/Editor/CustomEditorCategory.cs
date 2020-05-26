using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

[CustomEditor(typeof(Category))]
public class CustomEditorCategory : Editor
{
	bool foldout = false;
	Category category;
	bool[] showQuestions=new bool[100];
	const int spaceLevel = 15;
	public void OnEnable()
	{
		category = target as Category;

	}

	public override void OnInspectorGUI()
    {
	
		ClearArray();
		serializedObject.Update();
		ShowHead();
		ShowBody();
		serializedObject.ApplyModifiedProperties();
    }

	void ShowHead()
	{
		EditorGUILayout.HelpBox("Default Inspector", MessageType.None);
		EditorGUILayout.Space(); EditorGUILayout.Space();
		DrawDefaultInspector();
		EditorGUILayout.Space();
	}
	void ShowBody()
	{
		GUILayout.BeginVertical("HelpBox");
		EditorGUI.indentLevel++;
		foldout = EditorGUILayout.Foldout(foldout, "Questions", EditorStyles.foldoutHeader);
		if (foldout)
		{
			int count = 1;
			for (int i = 0; i < category.questions.Count; i++)
			{
				if(category.questions[i]!=null)
				ShowQuestion(i);
				count++;
			}
			ButtonAdds(category);
		}
		EditorGUI.indentLevel--;
		EditorGUILayout.EndVertical();

	}
	void ShowQuestion(int index)
	{
		EditorGUILayout.BeginVertical("HelpBox");
		EditorGUI.indentLevel++;
		showQuestions[index] = EditorGUILayout.Foldout(showQuestions[index], $"{index+1} Question (Type - {category.questions[index].GetType()})", EditorStyles.foldout);
		if (showQuestions[index] == true)
		{
			category.questions[index].OnGUI();
			EditorGUILayout.Space();
			ButtonDelete(index);
		}
		EditorGUI.indentLevel--;
		EditorGUILayout.EndVertical();

	}
	void ButtonDelete(int index)
	{
		GUILayout.BeginHorizontal();
		GUILayout.Space(EditorGUI.indentLevel * 15);
		GUILayout.FlexibleSpace();
		var oldColor = GUI.backgroundColor;
		GUI.backgroundColor = Color.red;
		if (GUILayout.Button("Delete", GUILayout.Width(100)))
		{
			SerializedProperty questionsProperty = this.serializedObject.FindProperty("questions");
			if (questionsProperty != null)
			{
				DestroySubAssetAtIndex(index, questionsProperty);
			}
		}
		GUI.backgroundColor = oldColor;
		GUILayout.EndHorizontal();
	}
	void ClearArray()
	{
		for (int index = 0; index < category.questions.Count; index++)
		{
			if (category.questions[index]==null)
				category.questions.RemoveAt(index);
		}
	}
	void DestroySubAssetAtIndex(int index, SerializedProperty listProperty)
	{
		//SerializedProperty elementProperty = listProperty.GetArrayElementAtIndex(index);
		DestroyImmediate(SelectSubAsset(index,listProperty), true);
		category.questions.RemoveAt(index);
		listProperty.DeleteArrayElementAtIndex(index);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
	}
	Object SelectSubAsset(int index, SerializedProperty listProperty)
	{
		SerializedProperty elementProperty = listProperty.GetArrayElementAtIndex(index);
		return elementProperty.objectReferenceValue;
	}
	void AddSubAssetAtIndex(string name,TypeQuestion type)
	{
		Question newAsset = SelectQuestion(type);
		newAsset.name = name;
		AssetDatabase.AddObjectToAsset(newAsset, category);
		AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(newAsset));
		category.questions.Add(newAsset);
		Debug.Log(AssetDatabase.GetAssetPath(category));
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
	}
	Question SelectQuestion(TypeQuestion type)
	{
		switch (type)
		{
			case TypeQuestion.Image: return CreateInstance<ImageQuestion>();
			case TypeQuestion.Sound: return CreateInstance<SoundQuestion>();
			case TypeQuestion.Text: return CreateInstance<TextQuestion>();
			default: return CreateInstance<Question>();
		}
	}
	void ButtonAdds(Category category)
	{
		GUILayout.BeginHorizontal();
		GUILayout.Space(EditorGUI.indentLevel * spaceLevel);
		GUILayout.Label("  Add:", EditorStyles.boldLabel);
		if (GUILayout.Button("Image"))
		{
			AddSubAssetAtIndex("ImageQuestion", TypeQuestion.Image);
		}
		if (GUILayout.Button("Text"))
		{
			AddSubAssetAtIndex("TextQuestion", TypeQuestion.Text);
		}
		if (GUILayout.Button("Sound"))
		{
			AddSubAssetAtIndex("SoundQuestion", TypeQuestion.Sound);
		}
		GUILayout.EndHorizontal();
	}
}
