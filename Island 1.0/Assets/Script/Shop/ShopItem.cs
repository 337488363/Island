using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public int objId;
    public string objName;
    public int objCount;
    public Sprite objSprite;
    public int objMaxnum;
    public int convertId;
    public int convertNum;
    public string Tooltip;
    public string ShopTooltip;
    [HideInInspector]
    public bool isFor;
    [HideInInspector]
    public bool isAdd = false;

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

    public int ObjMaxnum
    {
        get => objMaxnum;
        set => objMaxnum = value;
    }
}
