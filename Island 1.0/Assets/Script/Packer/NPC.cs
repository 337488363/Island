using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject player;
    private Transform npcUI;
    public Text npcText;
    public Transform Talk;
    public Transform UnshopButton;
    private bool isFirst = true;
    void Start()
    {
        npcUI = GameObject.Find("NPC").transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject==player)
        {
            if (isFirst)
            {
                npcUI.DOScale(new Vector3(0.024f, 0.024f, 0.024f), 0.5f).OnComplete(() =>
                {
                    npcText.text = "";
                    npcText.DOText("你好，勇士，听说这个岛上有许多珍宝，但是附近有许多怪物，你能为我收集一些吗？", 5f).OnComplete(()=>
                    {
                        Talk.DOLocalMove(new Vector3(0, 0, 0), 0.5f);
                    });
                });
            }
            else
            {
                OK();
            }
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject == player)
        {
            npcText.DOPause();
            npcText.text = "";
            npcUI.DOScale(new Vector3(0, 0, 0), 0.5f);
            UnshopButton.DOLocalMove(new Vector3(0, 1000, 0), 0);
            Talk.DOLocalMove(new Vector3(0, 1000, 0), 0);
        }
    }
    public void OK()
    {
        isFirst = false;
        Talk.DOLocalMove(new Vector3(0, 1000, 0), 0).OnComplete(() =>
        {
            npcUI.DOScale(new Vector3(0.024f, 0.024f, 0.024f), 0.5f).OnComplete(()=>
            {
                npcText.text = "";
                npcText.DOText("谢谢你，如果有需要的话请看看我的背包，里面可能会有你想要的东西。", 5f).OnComplete(() =>
                {
                    UnshopButton.DOLocalMove(new Vector3(0, 0, 0), 0.5f);
                });
            }
            );

        });
        
    }

    public void Unshop()
    {
        PlayerHp.Instance.ShopButton();
    }

    public void Closeshop()
    {
        PlayerHp.Instance.CloseShop();
    }

    public void refuse()
    {
        npcText.text = "";
        npcText.DOText("那真是太遗憾了，祝你好运。", 5f).OnComplete(delegate
        {
            npcText.text = "";
            npcUI.DOScale(new Vector3(0, 0, 0), 0.5f);
            UnshopButton.DOLocalMove(new Vector3(0, 1000, 0), 0);
            Talk.DOLocalMove(new Vector3(0, 1000, 0), 0);
        });
    }
}
