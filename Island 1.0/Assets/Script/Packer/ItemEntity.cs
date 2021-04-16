using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemEntity : MonoBehaviour
{

    public int objID;
    public int objCount;
    public string Name;
    public Sprite MySprite;
    public string Tooltip;

    public static ItemEntity FillData(ObjectItem item)
    {
        ItemEntity itemEntity=new ItemEntity();
        itemEntity.objID = item.objID;
        itemEntity.objCount = item.objCount;
        itemEntity.MySprite = item.MySprite;
        itemEntity.Name = item.Name;
        itemEntity.Tooltip = item.Tooltip;
        return itemEntity;
    }

}
