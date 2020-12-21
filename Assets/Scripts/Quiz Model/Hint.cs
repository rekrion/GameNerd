using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Hint
{
    [SerializeField] public string text;
    [SerializeField] public bool issued;

    public Hint()
    {

    }
}
