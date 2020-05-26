using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CrosswordAnswer : Answer
{
    const int countSymbols = 32;

	public override void OnGUI(SerializedObject so)
	{
		type = TypeAnswer.Crossword;
		base.OnGUI(so);
		EditorGUILayout.LabelField($"Count symbols:{countSymbols}");

	}
	public CrosswordAnswer()
	{
		type = TypeAnswer.Crossword;
	}
}
