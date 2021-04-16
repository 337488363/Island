using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LiquidTooltip : MonoBehaviour
{
    private Image image;
    private Shop shop;
    private Transform firepoint;
    private GameObject tip;
    private TextMeshProUGUI[] Texts;
    private string describe;
    private string Name;
    bool isHave;

    void Start()
    {

        tip = gameObject.transform.GetChild(2).gameObject;
        firepoint = GameObject.Find("FirePoint").transform;
        Texts = tip.GetComponentsInChildren<TextMeshProUGUI>();
        shop = GameObject.FindObjectOfType<Shop>();
    }

    void Update()
    {
        onEnter();
        int i = 0;
        image = GetComponent<Image>();
        foreach (PackItem item in shop.packItems)
        {
            i++;
            if (item.ObjSprite == image.sprite)
            {
                describe = item.Tooltip;
                Name = item.objName;
                isHave = true;
            }

            if (i == shop.packItems.Count && isHave == false)
            {
                break;
            }
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

