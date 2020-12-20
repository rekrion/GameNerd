using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UICrosswordPanel : MonoBehaviour
{
    class CrosswordOutput
    {
        public string answerText;
        public LineOutput[] lines = new LineOutput[maxLines];
        int indexLines = 0;

        internal void InitLines(GameObject charPrefab, GameObject otherCharPrefab)
        {
            int indexCounts = CountLines(answerText.Length);

            int multiple = 0;
            string textarea = answerText.ToUpper();
            for (int i = 0; i < lines.Length; i++)
            {
                if (indexCounts >= i)
                {
                    int length = maxCharInLine;
                    if (textarea.Length < maxCharInLine) length = textarea.Length;

                    lines[i].lineObject.gameObject.SetActive(true);
                    lines[i].textLine = textarea.Substring(0, length);
                    lines[i].textLine = lines[i].textLine.Trim();
                    lines[i].InitChars(lines[i].textLine, charPrefab, otherCharPrefab);
                    textarea = textarea.Remove(0, length);
                    multiple++;
                }
                else
                {
                    lines[i].lineObject.gameObject.SetActive(false);
                }
            }
        }

        int CountLines(int maxLength)
        {
            int length = maxLength;

            if (length <= maxChars && length > maxCharInLine * 2)
            {
                return 2;
            }
            else if (length <= maxCharInLine)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
        int FindLineIndex(int index)
        {
  


            int maxCharInLine = lines[indexLines].countSymbols;
            index = index - (indexLines * (maxCharInLine + 1));
            if (index <= maxCharInLine)
            {
                return indexLines;
            }
            else
            {
                indexLines += 1;
                return indexLines;
            }
        }

        internal int SetCurrentChar(int index, char symbol,out bool isFinal)
        {
            int indexLines = FindLineIndex(index);
            int indexChar = index - (indexLines * (maxCharInLine - 1));
            if (lines[indexLines].lineObject.gameObject.activeSelf)
            {
                indexChar = lines[indexLines].SetCurrentChar(indexChar, symbol);
                indexChar += (indexLines * (maxCharInLine - 1));
                isFinal = false;
                return indexChar;
            }
            else
            {
                isFinal = true;
                return indexChar;
            }

        }


    }
    class LineOutput
    {
        public string textLine;
        public Transform lineObject;
        public UICharCell[] charLine;
        public int countSymbols;

        int indexChar = 0;
        internal void InitChars(string chars, GameObject charPrefab, GameObject otherCharPrefab)
        {
            countSymbols = 0;
            charLine = new UICharCell[chars.Length];
            for (int i = 0; i < chars.Length; i++)
            {
                if (charsArray.Contains(chars[i]))
                {
                    charLine[i] = Instantiate(charPrefab, lineObject).GetComponent<UICharCell>();
                    charLine[i].TextChar.text = "";// chars[i].ToString();
                    charLine[i].isOther = false;
                    countSymbols++;
                }
                else
                {
                    charLine[i] = Instantiate(otherCharPrefab, lineObject).GetComponent<UICharCell>();
                    charLine[i].TextChar.text = chars[i].ToString();
                    charLine[i].isOther = true;
                }
            }
        }
        internal void ResetChars()
        {
            countSymbols = 0;
            for (int i = 0; i < charLine.Length; i++)
            {
                Destroy(charLine[i].gameObject);
            }
        }

        internal int SetCurrentChar(int index, char symbol)
        {
            indexChar = index;
            indexChar = FindChar(index);
            charLine[indexChar].TextChar.text = symbol.ToString();
            return indexChar;
        }
        int FindChar(int index)
        {
            int indexChar = index;
            if (charLine[indexChar].isOther)
            {
                indexChar += 1;
                return FindChar(indexChar);      
            }
                
            else
                return indexChar;
        }
    }



    [SerializeField] GameObject inputPanel;
    [SerializeField] GameObject outputPanel;

    [SerializeField] UIQuestionPanel main;

    [Header("Prefabs")]
    [SerializeField] GameObject charPrefab;
    [SerializeField] GameObject otherCharPrefab;


    CrosswordOutput outputData;
    Button[] charButtons;
    char[] chars;
    int currentChar = 0;


    const int maxCharInLine = 11;
    const int maxLines = 3;
    const int maxChars = 33;
    const string charsArray = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    const string numbersArray = "0123456789";
    bool isBlocked = false;

    public void InitData(string answer)
    {
        if (main.IsSolved)
        {
            isBlocked = true;
        }
        InitButtons();
        OutputPanel(answer);
    }

    void InitButtons()
    {
        if (inputPanel)
        {
            charButtons = inputPanel.GetComponentsInChildren<Button>();
            chars = new char[charButtons.Length];
        }
        chars = RandomChars(maxChars);
        for (int i = 0; i < charButtons.Length; i++)
        {
            charButtons[i].GetComponentInChildren<Text>().text = chars[i].ToString();
            char symbol = chars[i];
            charButtons[i].onClick.AddListener(() => ActivateButton(symbol));
        }
        
    }

    char[] RandomChars(int length)
    {

        var stringChars = new char[length];
        var random = new System.Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = charsArray[random.Next(charsArray.Length)];
        }
        return stringChars;
    }

    void OutputPanel(string answer)
    {
        outputData = new CrosswordOutput();
        outputData.answerText = answer;
        for (int i = 0; i < outputPanel.transform.childCount; i++)
        {
            Transform transform = outputPanel.transform.GetChild(i);
            LineOutput lineOtput = new LineOutput();
            lineOtput.lineObject = transform;
            outputData.lines[i] = lineOtput;
        }
        outputData.InitLines(charPrefab,otherCharPrefab);
    }

    void ActivateButton(char symbol)
    {      
        currentChar = outputData.SetCurrentChar(currentChar, symbol, out bool isFinal);
        Debug.Log($"Ввод {symbol}");
        if(!isFinal)
            currentChar++;
    }
}
