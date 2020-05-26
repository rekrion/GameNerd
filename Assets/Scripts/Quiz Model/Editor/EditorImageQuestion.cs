using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ImageQuestion), true)]
public class EditorImageQuestion : Editor
{
    ImageQuestion question;
    public override void OnInspectorGUI()
    {
        //serializedObject.Update();
        //SerializedObject serializedObj = new SerializedObject(target);
        //SerializedProperty answer = serializedObject.FindProperty("answer");
        DrawDefaultInspector();
        //
        //serializedObj.ApplyModifiedProperties();
    }
    void OnEnable()
    {
       // question = target as ImageQuestion;
    }
}
