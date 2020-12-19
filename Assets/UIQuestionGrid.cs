using PolyAndCode.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIQuestionGrid : MonoBehaviour, IRecyclableScrollRectDataSource
{
    [SerializeField] RecyclableScrollRect _recyclableScrollRect;
    List<QuestionInfo> questions;

    public int GetItemCount()
    {
        return questions.Count;
    }

    public void InitData()
    {
        questions = DataManager.Get.Category().questions;
        _recyclableScrollRect.DataSource = this;
    }


    public void SetCell(ICell cell, int index)
    {
        var item = cell as UICellQuestion;
        item.ConfigureCell(questions[index], index);
    }
}
