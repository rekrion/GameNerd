using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONLoader
{
    private TextAsset jsonFile;

    public void LoadJSON()
    {
        jsonFile = Resources.Load<TextAsset>("localization_JSON_EN");
    }
    public Dictionary<string, string> GetDictionaries(string fileName)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);

            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

            for (int i = 0; i < loadedData.items.Length; i++)
            {
                dictionary.Add(loadedData.items[i].key, loadedData.items[i].value);
            }
            Debug.Log("Data loaded, dictionary contains: " + dictionary.Count + " entries");
            return dictionary;
        }
        else
        {
            Debug.LogError("Cannot find file");
            return null;
        }

    }

    internal bool SetData(string fileName, List<CategoryInfo> categories)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        CaterogyData caterogy = new CaterogyData();
        caterogy.items = categories.ToArray();
        string data = JsonUtility.ToJson(caterogy, true);
        File.WriteAllText(filePath, data);
        Debug.Log("Data Saved, dictionary contains: [" + filePath + "]");
        return true;
    }

    internal List<CategoryInfo> GetData(string fileName, List<Category> categories)
    {
        List<CategoryInfo> dictionary = new List<CategoryInfo>();

        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            CaterogyData loadedData = JsonUtility.FromJson<CaterogyData>(dataAsJson);

            for (int i = 0; i < loadedData.items.Length; i++)
            {
                dictionary.Add(loadedData.items[i]);
            }
            Debug.Log("Data loaded, dictionary contains: " + dictionary.Count + " entries");
            return dictionary;
        }
        else
        {
            Debug.LogError("Cannot find file");
            return null;
        }
    }
}

[Serializable]
public class LocalizationData
{
    public DictionaryItem[] items;
}

[Serializable]
public class DictionaryItem
{
    public string key;
    public string value;
}

[Serializable]
public class CaterogyData
{
    public CategoryInfo[] items;
}


