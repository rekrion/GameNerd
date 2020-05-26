using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance = null;
    public List<Category> categories;

    void Start()
    {
        
        if (instance == null)
        { 
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        InitializeManager();
    }

    private void InitializeManager()
    {
    }
}
