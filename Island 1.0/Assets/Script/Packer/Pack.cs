using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Pack : MonoBehaviour
{
    public List<ItemEntity> items=null;
    public int maxItem = 6;

    void Start()
    {
        items = new List<ItemEntity>();
    }

    public ObjectItem getItem(ObjectItem item)
    {
        ItemEntity itemEntity=ItemEntity.FillData(item);


        //空背包时，直接添加
        if (items.Count < 1)
            {
                items.Add(itemEntity);
                item.objCount = 0;
            }
            else
            {
                //判断当前背包合并物品
                foreach (ItemEntity currItem in items)
                {
                    //相同物品，且可叠加，且分组未满
                if (currItem.objID.Equals(itemEntity.objID) )
                    {
                        //数量叠加
                        currItem.objCount = currItem.objCount + itemEntity.objCount;
                        item.objCount = 0;
                        break;
                    }
                   
                }
            //物品超出分组，有剩余格子则添加。否则剩余部分无法拾取
            if (items.Count < maxItem&& item.objCount > 0)
            {
                items.Add(itemEntity);
                item.objCount = 0;
            }
        }

            return item;
    }

    public void showPack()
    {
        string show = "";
        foreach (ItemEntity currItem in items)
        {
            show += " [" + currItem.objID + currItem.Name+"], 数量: " + currItem.objCount + "\n";
        }
    }
}
