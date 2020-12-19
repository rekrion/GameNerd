using PolyAndCode.UI;
using UnityEngine;
using UnityEngine.UI;

public class UICellQuestion : MonoBehaviour, ICell
{
    //UI
    public Text nameLabel;
    public Image iconImage;
    public Text countLabel;
    public Button button;
    //Model
    private QuestionInfo _contactInfo;
    private int _cellIndex;

    [SerializeField] Color isSolved;
    [SerializeField] Color isStandart;
    private void Start()
    {
        //Can also be done in the inspector
        //GetComponent<Button>().onClick.AddListener(ButtonListener);
    }

    //This is called from the SetCell method in DataSource
    public void ConfigureCell(QuestionInfo contactInfo, int cellIndex)
    {
        _cellIndex = cellIndex;
        _contactInfo = contactInfo;

        nameLabel.text = $"Тип вопроса  {GetType(contactInfo.type)}";
        countLabel.text = (cellIndex + 1).ToString();
        if (contactInfo.isSolved)
            iconImage.color = this.isSolved;
        else
            iconImage.color = this.isStandart;
        button.onClick.AddListener(ButtonListener);
    }

    private string GetType(TypeQuestion typeQuestion)
    {
        switch (typeQuestion)
        {
            case TypeQuestion.Image: return "Рисунок";
            case TypeQuestion.Text: return "Текст";
            case TypeQuestion.Sound: return "Звук";
        }
        return null;
    }


    private void ButtonListener()
    {
        UIController.Get.ShowPanelQuesition(_cellIndex);
    }
}
