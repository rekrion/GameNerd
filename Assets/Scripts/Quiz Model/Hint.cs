using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Hint
{
    [SerializeField] string text;
    [SerializeField] bool issued;

    public Hint()
    {

    }
}
