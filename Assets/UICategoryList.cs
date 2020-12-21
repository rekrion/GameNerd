using PolyAndCode.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICategoryList : MonoBehaviour, IRecyclableScrollRectDataSource
{
    [SerializeField] RecyclableScrollRect _recyclableScrollRect;

    List<CategoryInfo> categories;


    //Recyclable scroll rect's data source must be assigned in Awake.
    private void Awake()
    {
        //InitData();
       // _recyclableScrollRect.DataSource = this;
    }

    void Start()
    {
        InitData();

    }

    //Initialising _contactList with dummy data 
    public void InitData()
    {
        categories = DataManager.Get.Categories;
        _recyclableScrollRect.DataSource = this;
    }

    public int GetItemCount()
    {
        return categories.Count;
    }

    public void SetCell(ICell cell, int index)
    {
        var item = cell as UIPanelCategory;
        item.ConfigureCell(categories[index], index);
    }
}
