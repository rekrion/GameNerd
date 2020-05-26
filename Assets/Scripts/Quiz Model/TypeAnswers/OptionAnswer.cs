using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class OptionAnswer : Answer
{
	public override void OnGUI(SerializedObject so)
	{
		type = TypeAnswer.Options;
		base.OnGUI(so);
		SerializedProperty answerProperty = so.FindProperty("answer");
		SerializedProperty answerType = answerProperty.FindPropertyRelative("falseOpitons");
		if (answerType != null)
			EditorGUILayout.PropertyField(answerType, true);
	}
	public OptionAnswer()
	{
		type = TypeAnswer.Options;
	}


}
