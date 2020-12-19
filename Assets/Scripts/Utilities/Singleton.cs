using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static T instance;

    public static T Get
    {
        get
        {
            return GetInstance();
        }
    }


    public static T GetInstance()
    {

        if (instance == null)
        {
            instance = (T)FindObjectOfType(typeof(T));

        }
        return instance;
    }
    private void OnDestroy()
    {
        instance = null;
    }
}
