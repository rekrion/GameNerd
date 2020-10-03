using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TextLocalizerEditorWindow : EditorWindow
{
    public static void Open(string key)
    {
        TextLocalizerEditorWindow window = (TextLocalizerEditorWindow)ScriptableObject.CreateInstance(typeof(TextLocalizerEditorWindow));// new TextLocalizerEditorWindow();
        window.titleContent = new GUIContent("Localizer Window");
        window.ShowUtility();
        window.key = key;
    }

    public string key;
    public string value;

    public void OnGUI()
    {
        key = EditorGUILayout.TextField("Key: ", key);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Value: ",GUILayout.MaxWidth(50));
        value = EditorGUILayout.TextArea(value, EditorStyles.textArea, GUILayout.Height(100), GUILayout.Width(400));
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Add"))
        {
            if (LocalizationSystem.GetLocalisedValue(key) != string.Empty)
            {
                LocalizationSystem.Replace(key, value);
            }
            else
            {
                LocalizationSystem.Add(key, value);
            }
        }
        minSize = new Vector2(460, 250);
        maxSize = minSize;
    }
}

public class TextLocalizerSearchWindow : EditorWindow
{
    public static void Open()
    {
        TextLocalizerSearchWindow window = (TextLocalizerSearchWindow)ScriptableObject.CreateInstance(typeof(TextLocalizerSearchWindow));// new TextLocalizerSearchWindow();
        window.titleContent = new GUIContent("Localization Search");

        Vector2 mouse = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
        Rect r = new Rect(mouse.x - 450, mouse.y + 10, 10, 10);
        window.ShowAsDropDown(r, new Vector2(500, 300));
    }

    public static void Open(string key)
    {
        TextLocalizerSearchWindow window = (TextLocalizerSearchWindow)ScriptableObject.CreateInstance(typeof(TextLocalizerSearchWindow));// new TextLocalizerSearchWindow();
        window.titleContent = new GUIContent("Localization Search");

        Vector2 mouse = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
        Rect r = new Rect(mouse.x - 450, mouse.y + 10, 10, 10);
        window.ShowAsDropDown(r, new Vector2(500, 300));
        window.value = key;
    }

    public string value;
    public Vector2 scroll;
    public Dictionary<string, string> dictionary;
    private void OnEnable()
    {
        dictionary = LocalizationSystem.GetDictionaryForEditor();
    }
    public void OnGUI()
    {
        EditorGUILayout.BeginHorizontal("Box");
        EditorGUILayout.LabelField("Search: ", EditorStyles.boldLabel);
        value = EditorGUILayout.TextField(value);
        EditorGUILayout.EndHorizontal();
        GetSearchResults();
    }

    private void GetSearchResults()
    {
        if (value == null) { value = ""; /*return;*/ }

        EditorGUILayout.BeginVertical();
        scroll=EditorGUILayout.BeginScrollView(scroll);
        foreach (KeyValuePair<string, string> element in dictionary)
        {
            if (element.Key.ToLower().Contains(value.ToLower()) || element.Value.ToLower().Contains(value.ToLower()))
            {
                var r = EditorGUILayout.BeginHorizontal("Box") ;
                Texture closeIcon = (Texture)Resources.Load("Icons\\close");
                GUIContent content = new GUIContent(closeIcon);
                if (GUILayout.Button(content, GUILayout.MaxWidth(20), GUILayout.MaxHeight(20)))
                {
                    if (EditorUtility.DisplayDialog($"Remove key {element.Key} ?", "This will remove the element from localization are you sure?", "Do it"))
                    {
                        LocalizationSystem.Remove(element.Key);
                        AssetDatabase.Refresh();
                        LocalizationSystem.Init();
                        dictionary = LocalizationSystem.GetDictionaryForEditor();
                    }
                }
                EditorGUILayout.TextField(element.Key);
                EditorGUILayout.LabelField(element.Value);
                EditorGUILayout.EndHorizontal();
                if(element.Key.ToLower()==value.ToLower() || element.Value.ToLower() == value.ToLower())
                {
                    Color backgroundColor = new Color(1f, 0.0f, 0.0f, 0.3f);
                    Rect rx = new Rect(r.x, r.y, EditorGUIUtility.currentViewWidth, r.height);
                    EditorGUI.DrawRect(rx, backgroundColor);
                }
            }
        }
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }
}
