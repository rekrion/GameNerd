using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationSystem 
{
    public enum Language
    {
        English,
        Russia,
        Franch,
        Germany
    }

    public static Language language = Language.English;

    private static Dictionary<string, string> localisedEN;
    private static Dictionary<string, string> localisedRU;
    private static Dictionary<string, string> localisedFR;
    private static Dictionary<string, string> localisedGE;

    public static bool isInit;
    public static CSVLoader csvLoader;
    public static void Init()
    {
        csvLoader = new CSVLoader();
        csvLoader.LoadCSV();

        UpdateDictionaries();

        isInit = true;
    }

    private static void UpdateDictionaries()
    {
        localisedEN = csvLoader.GetDictionaryValues("en");
        localisedRU = csvLoader.GetDictionaryValues("ru");
        localisedFR = csvLoader.GetDictionaryValues("fr");
        localisedGE = csvLoader.GetDictionaryValues("ge");
    }

    public static string GetLocalisedValue(string key)
    {
        if (!isInit) { Init(); }
        string value = key;
        switch (language)
        {
            case Language.English:
                localisedEN.TryGetValue(key, out value);
                break;
            case Language.Russia:
                localisedRU.TryGetValue(key, out value);
                break;
            case Language.Franch:
                localisedFR.TryGetValue(key, out value);
                break;
            case Language.Germany:
                localisedGE.TryGetValue(key, out value);
                break;
        }
        return value;
    }
    public static void Add(string key,string value)
    {
        if (value.Contains("\""))
        {
            value.Replace('"', '\"');
        }
        if (csvLoader == null)
        {
            csvLoader = new CSVLoader();
        }
        csvLoader.LoadCSV();
        csvLoader.Add(key, value);
        UpdateDictionaries();
    }

    public static Dictionary<string, string> GetDictionaryForEditor()
    {
        if (!isInit) { Init(); }
        return localisedEN;
    }

    public static void Replace(string key, string value)
    {
        if (value.Contains("\""))
        {
            value.Replace('"', '\"');
        }
        if (csvLoader == null)
        {
            csvLoader = new CSVLoader();
        }
        csvLoader.LoadCSV();
        csvLoader.Edit(key, value);
        UpdateDictionaries();
    }
    public static void Remove(string key)
    {
        if (csvLoader == null)
        {
            csvLoader = new CSVLoader();
        }
        csvLoader.LoadCSV();
        csvLoader.Remove(key);
        UpdateDictionaries();
    }
}
