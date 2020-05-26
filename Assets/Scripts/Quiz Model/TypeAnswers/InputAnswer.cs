using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class InputAnswer : Answer
{
	public override void OnGUI(SerializedObject so)
	{
		type = TypeAnswer.Input;
		base.OnGUI(so);
	}
	public InputAnswer()
	{
		type = TypeAnswer.Input;
	}
}
