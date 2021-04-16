using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public static CountDown Instance;
    public Image loop;
    public bool isstart = false;
    private GameObject Fx;
    private GameObject flow;
    private GameObject stage;


    public int num;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
         Fx= GameObject.Find("fx_countDown");
         flow=GameObject.Find("fx_DirectionFlow");
         stage=GameObject.Find("StageFx");
         Fx.gameObject.SetActive(false);
    }

    void Update()
    {

        if (loop.fillAmount>0.98&&!isstart)
        {
            Fx.gameObject.SetActive(true);
            Fx.GetComponent<ParticleSystem>().Play(true);
            Destroy(flow);
            Destroy(stage);
            StartCoroutine(countDown());
            isstart = true;
        }

        if (num == 0)
        {
            
            gameObject.SetActive(false);
        }
    }

    IEnumerator countDown()
    {
        while (num>0)
        {
            yield return new  WaitForSeconds(1);
            num--;
        }
    }
}
