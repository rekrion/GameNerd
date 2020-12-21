using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Shop : MonoBehaviour


{
    [System.Serializable] class ShopItem
    {
        public Sprite Image;
        public int Price;
        public bool IsPurchased = false;
    }


    [SerializeField] List<ShopItem> ShopItemsList;



    [SerializeField] UICellShop cellPrefab;

    [SerializeField] Transform contentPanel;
   
    // Start is called before the first frame update
    void Start()
    {
        InitData();
    }
     
    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitData()
    {
        UICellShop g;
        //ItemTemplate = ShopScrollView.GetChild(0).gameObject;
        int len = ShopItemsList.Count;
        for (int i = 0; i < len; i++)
        {
            g = Instantiate(cellPrefab, contentPanel);
            g.image.sprite = ShopItemsList[i].Image;
            g.price.text = ShopItemsList[i].Price.ToString();
            g.button.interactable = ShopItemsList[i].IsPurchased;
        }
    }
}

