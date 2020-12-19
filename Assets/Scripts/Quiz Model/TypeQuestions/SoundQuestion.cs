using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class SoundQuestion : Question
{
    [SerializeField]
    AudioClip sound;

    [HideInInspector] public AudioClip Sound => sound;
    public override void OnGUI()
    {
        type = TypeQuestion.Sound;
        sound = (AudioClip)EditorGUILayout.ObjectField("Sound", sound, typeof(AudioClip), true);
        base.OnGUI();
    }
    public SoundQuestion(){}
}
