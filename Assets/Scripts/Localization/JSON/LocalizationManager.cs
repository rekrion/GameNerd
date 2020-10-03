using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager instance;

    private static Dictionary<string, string> localizedText;
    private static Dictionary<string, string> localizedQuestion;
    bool isReady = false;
    private const string missingText = "Localized text not found!";

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void LoadLocalizedText(string filename)
    {
        JSONLoader csvLoader = new JSONLoader();
        csvLoader.LoadJSON();
        localizedText = csvLoader.GetDictionaries(filename);
        isReady = true;
    }
    public void LoadLocalizedFile(string filename)
    {
        JSONLoader csvLoader = new JSONLoader();
        csvLoader.LoadJSON();
        localizedQuestion = csvLoader.GetDictionaries(filename);
        isReady = true;
    }

    public string GetLocalizedValue(string key)
    {
        string result = missingText;
        if (localizedText == null)
        {
            Debug.LogError("Not Localized Text");
            return result;
        }
        if (localizedText.ContainsKey(key))
        {
            result = localizedText[key];
        }
        return result;
    }

    public string GetQuestionValue(string key)
    {
        string result = missingText;
        if (localizedQuestion == null)
        {
            Debug.LogError("Not Localized Question");
            return result;
        }
        if (localizedQuestion.ContainsKey(key))
        {
            result = localizedQuestion[key];
        }
        return result;
    }
    public bool GetReady()
    {
        return isReady;
    }
}