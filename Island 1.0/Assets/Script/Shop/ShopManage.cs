using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Michsky.UI.ModernUIPack;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ShopManage : MonoBehaviour
{
    // public List<Button> PackButtons;
    private Button[] PackButtons;
    private Shop Shop;
    private Pack Pack;
    public Sprite Sprite;
    private Button[] CollectButtons;
    public Transform Tip;

    void Start()
    {
        PackButtons = GameObject.Find("LiquidPack").GetComponentsInChildren<Button>();
        CollectButtons = GameObject.Find("CollectPack").GetComponentsInChildren<Button>();
        Shop = GameObject.FindObjectOfType<Shop>();
        Pack = GameObject.FindObjectOfType<Pack>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void aaaDown()
    {
        ShopItem shopItem = gameObject.GetComponent<ShopItem>();
         Shop.getItem(shopItem);

         //兑换的物品增加
        for (int i = 0; i < Shop.packItems.Count; i++)
        {
            PackButtons[i].GetComponent<Image>().sprite = Shop.packItems[i].objSprite;
            PackButtons[i].GetComponent<ButtonManagerBasic>().buttonText = Shop.packItems[i].objName + "x" + Shop.packItems[i].objCount.ToString();
            PackButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = Shop.packItems[i].objName + "x" + Shop.packItems[i].objCount.ToString();
        }

        //背包的物品减少
        for (int i = 0; i < Pack.items.Count; i++)
        {
            CollectButtons[i].GetComponent<Image>().sprite = Pack.items[i].MySprite;
            CollectButtons[i].GetComponent<ButtonManagerBasic>().buttonText =
                Pack.items[i].Name + "x" + Pack.items[i].objCount.ToString();
            CollectButtons[i].GetComponentInChildren<TextMeshProUGUI>().text =
                Pack.items[i].Name + "x" + Pack.items[i].objCount.ToString();

            if (Pack.items[i].objCount==0)
            {
                CollectButtons[i].GetComponent<Image>().sprite = this.Sprite;
                CollectButtons[i].GetComponent<ButtonManagerBasic>().buttonText = "";
                CollectButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = "";

                // Pack.items.Remove(Pack.items[i]);
                // Pack.items.Add(Pack.items[i]);
            }
        }
    }

    public void Trim()
    {
        Pack.items.Sort(new Count());
        for (int i = 0; i < Pack.items.Count; i++)
        {
            CollectButtons[i].GetComponent<Image>().sprite = Pack.items[i].MySprite;
            CollectButtons[i].GetComponent<ButtonManagerBasic>().buttonText =
                Pack.items[i].Name + "x" + Pack.items[i].objCount.ToString();
            CollectButtons[i].GetComponentInChildren<TextMeshProUGUI>().text =
                Pack.items[i].Name + "x" + Pack.items[i].objCount.ToString();

            if (Pack.items[i].objCount == 0)
            {
                CollectButtons[i].GetComponent<Image>().sprite = this.Sprite;
                CollectButtons[i].GetComponent<ButtonManagerBasic>().buttonText = "";
                CollectButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }
    public class  Count:IComparer<ItemEntity>
    {
        public int Compare(ItemEntity y, ItemEntity x)
        {
            return x.objCount.CompareTo(y.objCount);
        }
    }

}
