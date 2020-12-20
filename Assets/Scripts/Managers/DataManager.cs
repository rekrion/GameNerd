using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public static DataManager instance = null;
    public List<Category> categories;
    [SerializeField] DataCaterogies data;
    [SerializeField] string filename = "data_categories.json";

    [HideInInspector] public List<CategoryInfo> Categories => data.Categories;
    [HideInInspector] public int currentQuestion;

    internal CategoryInfo Category()
    {
        return data.Categories[currentCategory];
    }

    [HideInInspector] public int currentCategory;


    void Awake()
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

    internal QuestionInfo Question()
    {
        return data.Categories[currentCategory].questions[currentQuestion];
    }
    internal QuestionInfo NextQuestion()
    {
        return null;
    }
    internal QuestionInfo LastQuestion()
    {
        return null;
    }

    private void InitializeManager()
    {
        LoadData();
    }

    private void LoadData()
    {   
        List<CategoryInfo> temp = LoadData(filename);
        if(temp == null)
        {
            SetupData();
            SaveData(filename, data.Categories);
        }
        else
        {
            data.Categories = temp;
        }
    }

    private bool SaveData(string filename, List<CategoryInfo> categories)
    {
        JSONLoader loader = new JSONLoader();
        return loader.SetData(filename, categories);
    }

    private List<CategoryInfo> LoadData(string filename)
    {
        JSONLoader loader = new JSONLoader();
        return loader.GetData(filename, categories);
    }

    public void SetupData()
    {
        foreach (Category category in data.Origins)
        {
            Category current = Instantiate(category);
            CategoryInfo categoryInfo = new CategoryInfo(category);
            data.Categories.Add(categoryInfo);
        }
    }

    internal void Save()
    {
        SaveData(filename, data.Categories);
    }

    internal bool IsStartQuestion()
    {
        return currentQuestion == 0;
    }

    internal bool IsLastQuestion()
    {
        return currentQuestion == data.Categories[currentCategory].questions.Count - 1;
    }
}

[System.Serializable]
public class DataCaterogies
{
    [SerializeField] List<Category> categories;
    public List<CategoryInfo> Categories;
    public List<Category> Origins => categories;
}
