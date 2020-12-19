using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New ImageQuestion", menuName = "Quiz Model/ImageQuestion")]
[System.Serializable]
public class ImageQuestion : Question
{
    [SerializeField]
    private Sprite spriteQuestion;

    [HideInInspector] public Sprite Sprite => spriteQuestion;

    public override void OnGUI()
    {
        type = TypeQuestion.Image;
        //Texture2D texture = Resources.Load<Texture2D>("Sprites/Icons/" + spriteQuestion.itemDatabase[i].itemName);
        //GUILayout.Label(texture)
        spriteQuestion = (Sprite)EditorGUILayout.ObjectField("Sprite", spriteQuestion, typeof(Sprite), allowSceneObjects: true);
        base.OnGUI();
    }
    public ImageQuestion(){
        
    }
}