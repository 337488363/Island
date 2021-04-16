using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopTooltip : MonoBehaviour
{
    private ShopItem shopItem;
    private Transform firepoint;
    private GameObject tip;
    private TextMeshProUGUI[] Texts;
    private string describe;
    private string Name;
    private bool isHave;

    void Start()
    {
        
        tip = gameObject.transform.GetChild(2).gameObject;
        firepoint = GameObject.Find("FirePoint").transform;
        Texts = tip.GetComponentsInChildren<TextMeshProUGUI>();
        shopItem = transform.GetComponentInChildren<ShopItem>();
    }

    void Update()
    {
        onEnter();
        isHave = false;
        if (shopItem!=null)
        {
            describe = shopItem.ShopTooltip;
            Name = "";
            isHave = !isHave;
        }
    }

    void onEnter()
    {
        if (Physics.Raycast(firepoint.position, firepoint.forward, out var hit, 10f))
        {
            if (hit.collider.gameObject == gameObject)
            {
                tip.transform.position = firepoint.transform.position + firepoint.transform.forward * 1.3f;
                Texts[0].text = describe;
                Texts[1].text = Name;
                if (isHave)
                {
                    tip.SetActive(true);
                }
            }
            else if (hit.collider.gameObject != gameObject)
            {
                tip.SetActive(false);
            }

        }

    }
}

