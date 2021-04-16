using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private Pack pack;
    [HideInInspector]
    public  List<PackItem> packItems;
    public int PackNum = 6;
    public Transform Tip;

    void Start()
    {
        pack = GameObject.FindObjectOfType<Pack>();
    }
    public ShopItem getItem(ShopItem item)
    {
        PackItem packItem = PackItem.FillData(item);
        item.isFor = false;

        if (pack.items != null)
        {
            foreach (ItemEntity curriEntity in pack.items)
            {
                if (curriEntity.objID == packItem.convertId)//寻找相印的兑换物品
                {
                    
                    if (curriEntity.objCount >= packItem.convertNum)//当需要的原材料足够时
                    {
                        Tip.DOScale(new Vector3(1f, 1f, 1f), 0.5f).OnComplete(() =>
                        {
                            Tip.DOScale(new Vector3(0f, 0f, 0), 1f);
                        });

                        item.isFor = true;
                        curriEntity.objCount -= packItem.convertNum;

                        if (item.ObjMaxnum > 0)
                        {
                            if (packItems.Count < 1)
                            {
                                packItems.Add(packItem);
                            }

                            else
                            {
                                foreach (PackItem currItem in packItems)
                                {
                                    if (currItem.objId == packItem.objId)
                                    {
                                        currItem.objCount = currItem.objCount + packItem.objCount;
                                        item.objMaxnum--;
                                        item.isAdd = true;
                                        break;
                                    }
                                }

                                if (packItems.Count < PackNum && !item.isAdd)
                                {
                                    packItems.Add(packItem);
                                }
                            }
                        }
                    }
                    else
                    {
                        Tip.DOPause();
                        var TipText = Tip.GetChild(0).GetComponent<TextMeshProUGUI>();
                        TipText.fontSize = 4;
                        TipText.DOText("对不起，你的物品不够", 0).OnComplete(() =>
                        {
                            Tip.DOScale(new Vector3(1f, 1f, 1f), 0.5f).OnComplete(delegate
                            {
                                Tip.DOScale(new Vector3(0f, 0f, 0), 1f);
                            });
                            
                        });
                    }
                }
            }

            if (item.isFor == false)
            {
                Tip.DOPause();
                var TipText = Tip.GetChild(0).GetComponent<TextMeshProUGUI>();

                TipText.fontSize = 4;
                TipText.DOText("对不起，你没有兑换物品", 0).OnComplete(() =>
                {
                    Tip.DOScale(new Vector3(1f, 1f, 1f), 0.5f).OnComplete(delegate
                    {
                        Tip.DOScale(new Vector3(0f, 0f, 0), 1f);
                    });

                });
            }
        }
        else
        {
            Tip.DOPause();
            var TipText = Tip.GetChild(0).GetComponent<TextMeshProUGUI>();
            TipText.fontSize = 4;
            TipText.DOText("对不起，你的物品不够", 0).OnComplete(() =>
            {
                Tip.DOScale(new Vector3(1f, 1f, 1f), 0.5f).OnComplete(delegate
                {
                    Tip.DOScale(new Vector3(0f, 0f, 0), 1f);
                });
            });
        }

        return item;

    }
}
