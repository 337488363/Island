using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    private Image image;
    private Pack pack;
    private Transform firepoint;
    private GameObject tip;
    private TextMeshProUGUI[] Texts;
    public RaycastHit Hit;
    private string describe;
    private string Name;
    bool isHave;

    void Start()
    {
        tip = gameObject.transform.GetChild(2).gameObject;
        firepoint = GameObject.Find("FirePoint").transform;
        pack = GameObject.FindObjectOfType<Pack>();
        Texts = tip.GetComponentsInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        onEnter();
        isHave = false;
        int i = 0;
        image = GetComponent<Image>();
        foreach (ItemEntity item in pack.items)
        {
            i++;
            if (item.MySprite == image.sprite)
            {
                describe = item.Tooltip;
                Name = item.Name;
                isHave = true;
            }

            if (i == pack.items.Count && isHave == false)
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
                tip.transform.position = firepoint.transform.position+firepoint.transform.forward*0.7f;
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
