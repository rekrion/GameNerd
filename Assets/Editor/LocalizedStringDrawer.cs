using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomPropertyDrawer(typeof(LocalizedString))]
public class LocalizedStringDrawer : PropertyDrawer
{
    bool dropdown;
    float height;
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (dropdown)
        {
            return height + 25;
        }
        return 20;
        //return base.GetPropertyHeight(property, label);
    }
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        int posX = 15;

        EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        position.width -= 34;
        position.height = 18;

        Rect valueRect = new Rect(position);
        valueRect.x += 0;//posX
        valueRect.width -= posX;

        position.x -= posX;//-

        Rect foldButtonRect = new Rect(position);
        foldButtonRect.width = posX;

        dropdown = EditorGUI.Foldout(foldButtonRect, dropdown, "");

        position.x  += posX;
        position.width -= posX+54;

        SerializedProperty key = property.FindPropertyRelative("key");
        key.stringValue = EditorGUI.TextField(position, key.stringValue);

        position.x += position.width + 2;
        position.width = 50;
        position.height = 17;

        //Texture searchIcon = (Texture)Resources.Load("Icons\\search");
        //GUIContent searchContent = new GUIContent(searchIcon,"Search");
        if (GUI.Button(position, "Search"))//searchContent))
        {
            TextLocalizerSearchWindow.Open(key.stringValue);
        }
        position.x += position.width + 2;
        //Texture storeIcon = (Texture)Resources.Load("Icons\\store");
        //GUIContent storeContent = new GUIContent(storeIcon,"Store");
        if (GUI.Button(position,"Store"))//storeContent))
        {
            TextLocalizerEditorWindow.Open(key.stringValue);
        }
        if (dropdown)
        {
            var value = LocalizationSystem.GetLocalisedValue(key.stringValue);
            GUIStyle style = GUI.skin.box;
            height = style.CalcHeight(new GUIContent(value), valueRect.width);

            valueRect.height = height;
            valueRect.y += 21;
            EditorGUI.LabelField(valueRect,"Value: "+ value, EditorStyles.wordWrappedLabel);

        }

        EditorGUI.EndProperty();
            //base.OnGUI(position, property, label);
        }
    }
