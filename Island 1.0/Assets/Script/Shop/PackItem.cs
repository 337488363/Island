using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackItem : MonoBehaviour
{
    public int objId;
    public string objName;
    public int objCount;
    public int convertId;
    public int convertNum;
    public string Tooltip;
    public Sprite objSprite;
    public string ShopTooltip;

    public static PackItem FillData(ShopItem item)
    {
        PackItem packItem=new PackItem();
        packItem.objId = item.objId;
        packItem.objCount = item.objCount;
        packItem.objName = item.objName;
        packItem.objSprite = item.objSprite;
        packItem.convertId = item.convertId;
        packItem.convertNum = item.convertNum;
        packItem.Tooltip = item.Tooltip;
        packItem.ShopTooltip = item.ShopTooltip;


        return packItem;
    }

    public int ObjId
    {
        get => objId;
        set => objId = value;
    }

    public string ObjName
    {
        get => objName;
        set => objName = value;
    }

    public int ObjCount
    {
        get => objCount;
        set => objCount = value;
    }

    public Sprite ObjSprite
    {
        get => objSprite;
        set => objSprite = value;
    }
}
