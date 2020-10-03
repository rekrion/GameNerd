using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using UnityEngine;

public class CSVLoader 
{
    private TextAsset csvFile;
    private const char lineSeparator = '\n';
    private const char surround = '"';
    private string[] fieldSeparator = { "\",\"" };

    public void LoadCSV()
    {
        csvFile = Resources.Load<TextAsset>("localization");
    }

    public Dictionary<string,string> GetDictionaryValues(string attributeID)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        string[] lines = csvFile.text.Split(lineSeparator);
        int attrIndex = -1;
        string[] headers = lines[0].Split(fieldSeparator, System.StringSplitOptions.None);
        for(int i = 0; i < headers.Length; i++)
        {
            if (headers[i].Contains(attributeID))
            {
                attrIndex = i;
                break;
            }
        }
        Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
        
        for(int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] fields = CSVParser.Split(line);
            for(int j = 0; j < fields.Length; j++)
            {
                fields[j] = fields[j].TrimStart(' ', surround);
                fields[j] = fields[j].TrimEnd(surround);
            }
            if (fields.Length > attrIndex)
            {
                string key = fields[0];
                if (dictionary.ContainsKey(key)) { continue; }
                string value = fields[attrIndex];
                dictionary.Add(key, value);
            }
        }
        return dictionary;
    }
#if UNITY_EDITOR
    public void Add(string key, string value)
    {
        string appended = string.Format("\n\"{0}\",\"{1}\",\"\"", key, value);
        File.AppendAllText("Assets/Resources/localization.csv", appended);
        UnityEditor.AssetDatabase.Refresh();
    }
    public void Remove(string key)
    {
        string[] lines = csvFile.text.Split(lineSeparator);
        string[] keys = new string[lines.Length];
        for(int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            keys[i] = line.Split(fieldSeparator, StringSplitOptions.None)[0];
        }
        int index=-1;
        for(int i = 0; i < keys.Length; i++)
        {
            if (keys[i].Contains(key))
            {
                index = i;
                break;
            }
        }
        if (index > -1)
        {
            string[] newLines = lines.Where(w => w != lines[index]).ToArray();
            string replaced = string.Join(lineSeparator.ToString(), newLines);
            File.WriteAllText("Assets/Resources/localization.csv",replaced);
        }

    }
    public void Edit(string key,string value)
    {
        Remove(key);
        Add(key, value);
    }
#endif
}
