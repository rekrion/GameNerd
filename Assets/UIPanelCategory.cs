using UnityEngine;
using UnityEngine.UI;
using PolyAndCode.UI;

public class UIPanelCategory : MonoBehaviour, ICell
{
    //UI
    public Text nameLabel;
    public Image iconImage;
    public Text countLabel;

    //Model
    private CategoryInfo _contactInfo;
    private int _cellIndex;

    private void Start()
    {
        //Can also be done in the inspector
        GetComponent<Button>().onClick.AddListener(ButtonListener);
    }

    //This is called from the SetCell method in DataSource
    public void ConfigureCell(CategoryInfo contactInfo, int cellIndex)
    {
        _cellIndex = cellIndex;
        _contactInfo = contactInfo;

        nameLabel.text = contactInfo.nameCategory;
        iconImage.sprite = contactInfo.icon;
        countLabel.text = $"Решено: {contactInfo.GetSolvedCount()}/{contactInfo.questions.Count}";
    }


    private void ButtonListener()
    {
        UIController.Get.ShowQuesitions(_cellIndex);
    }
}
