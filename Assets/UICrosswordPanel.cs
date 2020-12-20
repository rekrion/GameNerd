using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UICrosswordPanel : MonoBehaviour
{
    class LineOutput
    {
        public string textLine;
        public Transform lineObject;
        public UICharCell[] charLine;
        public UICharCell[] cellSymbols;
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
                    charLine[i].TextChar.text = "";
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
            cellSymbols = charLine.Where(q => q.isOther == false).ToArray();
            foreach (UICharCell charCell in cellSymbols)
            {
                charCell.GetComponentInChildren<Button>().interactable = false;
                charCell.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            }
        }
        internal void ResetChars()
        {
            if (charLine == null) return;
            countSymbols = 0;
            for (int i = 0; i < charLine.Length; i++)
            {
                Destroy(charLine[i].gameObject);
            }
            cellSymbols = null;
            charLine = null;
        }

        internal int SetCurrentChar(int index, int indexButton, char symbol)
        {
            indexChar = index;
            cellSymbols[indexChar].TextChar.text = symbol.ToString();
            cellSymbols[indexChar].IndexButton = indexButton;
            cellSymbols[indexChar].GetComponentInChildren<Button>().interactable = true;
            return indexChar;
        }
    }


    class PanelCrosswordOutput
    {
        LineOutput[] lines = new LineOutput[maxLines];
        int selectLines;
        int selectIndex;
        string currentText;
        string answer;
        public bool isBlocked = false;

        public void Setup(string answer, GameObject outputPanel, GameObject charPrefab, GameObject otherCharPrefab)
        {
            string currentAnswer = answer.ToUpper().Trim();
            if (this.answer == currentAnswer) return;
            this.answer = currentAnswer;
            ResetPanel();
            SetupLines(outputPanel);
            InitLines(charPrefab, otherCharPrefab);
        }
        void SetupLines(GameObject outputPanel)
        {
            for (int i = 0; i < outputPanel.transform.childCount; i++)
            {
                Transform transform = outputPanel.transform.GetChild(i);
                LineOutput lineOtput = new LineOutput();
                lineOtput.lineObject = transform;
                lines[i] = lineOtput;
            }
        }
        void InitLines(GameObject charPrefab, GameObject otherCharPrefab)
        {
            int indexCounts = GetIndexLines(answer.Length);

            string textarea = answer;
            for (int i = 0; i < lines.Length; i++)
            {
                if (indexCounts >= i)
                {
                    lines[i].lineObject.gameObject.SetActive(true);
                    int length = maxCharInLine;
                    string text = GetText(textarea, ref length);

                    SetLines(lines[i],text,charPrefab,otherCharPrefab);

                    textarea = textarea.Remove(0, length);
                }
                else
                {
                    lines[i].lineObject.gameObject.SetActive(false);
                }
            }
        }
        void SetLines(LineOutput line, string text, GameObject charPrefab, GameObject otherCharPrefab)
        {
            line.lineObject.gameObject.SetActive(true);
            line.textLine = text;
            line.InitChars(line.textLine, charPrefab, otherCharPrefab);
        }
        void ResetPanel()
        {
            foreach(LineOutput line in lines)
            {
                if(line!=null)
                    line.ResetChars();
            }
        }
        string GetText(string text,ref int length)
        {
            if (text.Length < length)
                length = text.Length;
            return text.Substring(0, length).Trim();
        }
        int GetIndexLines(int maxLength)
        {
            int length = maxLength;
            int charMax = maxChars;
            int charLineMax = maxCharInLine;

            if (length <= charMax && length > charLineMax * 2)
            {
                return 2;
            }
            else if (length <= charLineMax)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        bool GetLinesIndex()
        {
            int index = 0;
            int maxchar = lines[selectLines].countSymbols;
            if(selectIndex >= maxchar)
            {
                selectLines += 1;
                selectIndex = 0;
                return true;
            }
            return false;
        }

        internal bool SetCell(int indexButton, char symbol)
        {
            GetLinesIndex();
            if (lines.Length < selectLines)
            {
                currentText = CheckResultText();
                if (currentText == answer)
                {
                    DataManager.Get.Question().isSolved = true;
                    DataManager.Get.Save();
                }
                Debug.Log(currentText);
                return true;
            }             
            if (lines[selectLines].lineObject.gameObject.activeSelf == false)
            {
                currentText = CheckResultText();
                if (currentText == answer)
                {
                    DataManager.Get.Question().isSolved = true;
                    DataManager.Get.Save();
                }
                Debug.Log(currentText);
                return true;
            }
            else
            {
                lines[selectLines].SetCurrentChar(selectIndex, indexButton, symbol);
                selectIndex++;
                return false;
            }



        }
        string CheckResultText()
        {
            string text = "";
            foreach(LineOutput line in lines)
            {
                string textArea = "";
                if (line.cellSymbols != null)
                {
                    foreach (UICharCell uchar in line.cellSymbols)
                    {
                        textArea += uchar.TextChar.text;
                    }
                    text += textArea;
                }
            }
            return text;
        }

        internal void Blocked()
        {
            isBlocked = true;
        }
    }

    class PanelCrosswordInput
    {
        Button[] charButtons;
        char[] chars;

        int selectIndex;

        Action<char, int> action;
        string answer;


        public void Setup(string answer, GameObject inputPanel, Action<char, int> action)
        {
            answer = answer.ToUpper();
            answer = answer.Trim();
            this.answer = SetAnswer(answer);
            Debug.Log($"Origin: {answer} Now: {this.answer}");
            charButtons = inputPanel.GetComponentsInChildren<Button>();
            chars = new char[charButtons.Length];
            this.action = action;
            ClearButtonListeners();
            AddRandomSymbols();
        }
        void ClearButtonListeners()
        {
            foreach (Button charButton in charButtons)
                charButton.onClick.RemoveAllListeners();
        }
        void AddRandomSymbols()
        {
            chars = RandomChars(maxChars);

            chars = AddSymbolsWithAnswer(chars);
            for (int i = 0; i < charButtons.Length; i++)
            {
                charButtons[i].GetComponentInChildren<Text>().text = chars[i].ToString();
                char symbol = chars[i];
                int index = i;
                charButtons[i].onClick.AddListener(() => action(symbol, index));
            }
        }
        char[] AddSymbolsWithAnswer(char[] chars)
        {
            var random = new System.Random();
            int[] indexes = new int[answer.Length];
            int currentIndex = 0;
            foreach (char curChar in answer)
            {
                int indexRandom = FindUniqueRandom(random, indexes);

                chars[indexRandom] = curChar;
                indexes[currentIndex] = indexRandom;
                currentIndex++;
            }
            return chars;
        }
        int FindUniqueRandom(System.Random random, int[] indexes)
        {

            int index = random.Next(0, 32);
            if (indexes.Contains(index))
            {
                return FindUniqueRandom(random, indexes);
            }
            else
                return index;
        }
        string SetAnswer(string answer)
        {
            string temp="";
            foreach(char symbol in answer)
            {
                if (charsArray.Contains(symbol))
                    temp += symbol;
            }
            return temp;
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

        internal void Blocked()
        {
            foreach (Button charButton in charButtons)
                charButton.interactable = false;
        }
        internal void Unblocked()
        {
            foreach (Button charButton in charButtons)
                charButton.interactable = true;

        }

        internal void CloseCell(int index)
        {
            charButtons[index].interactable = false;
            charButtons[index].GetComponentInChildren<Text>().text = "";
        }
    }

    [SerializeField] GameObject inputPanel;
    [SerializeField] GameObject outputPanel;

    [SerializeField] UIQuestionPanel main;

    [Header("Prefabs")]
    [SerializeField] GameObject charPrefab;
    [SerializeField] GameObject otherCharPrefab;


   // CrosswordOutput outputData;
    PanelCrosswordInput inputData;
    PanelCrosswordOutput outputData;
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
        InitButtons(answer);
        OutputPanel(answer);
    }

    void InitButtons(string answer)
    {
        if(inputData==null)
            inputData = new PanelCrosswordInput();
        inputData.Setup(answer, inputPanel, ActivateButton);
    }
    void OutputPanel(string answer)
    {
        if (outputData == null)
            outputData = new PanelCrosswordOutput();
        outputData.Setup(answer, outputPanel, charPrefab, otherCharPrefab);
    }

    void ActivateButton(char symbol, int index)
    {
        isBlocked = outputData.SetCell(index, symbol);
        Debug.Log($"Ввод {symbol} IndexButton {index}");
        inputData.CloseCell(index);
        if (!isBlocked)
        {
            currentChar++;
        }
           
        else
        {
            //outputData.ResetPanel();
            outputData.Blocked();
            inputData.Blocked();
            if(DataManager.Get.Question().isSolved)
                main.InitData();
        }
    }
}
