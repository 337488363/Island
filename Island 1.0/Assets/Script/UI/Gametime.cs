using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class Gametime : MonoBehaviour
{
    public double num=10 ;

    public TextMesh Text;

    public static Gametime Instance;
    public bool isStart = false;
    public bool ischange = false;

    void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        if (CountDown.Instance.num==0&&!isStart)
        {
            StartCoroutine(gametime());
            isStart = !isStart;
        }

        if (num<5)
        {
           
        }
        if (num<=0&&!ischange)
        {
            EnemyCreate.Instance.rank++;
            Text.text = "";
            Waittime.Instance.isstart = false;
            Waittime.Instance.ischange = false;
            Waittime.Instance.num = 20;
            ischange = !ischange;
        }
    }

    IEnumerator  gametime()
    {
        while (Application.isPlaying)
        {
            Text.text = num.ToString("f2");
            yield return new WaitForSeconds(0.0001f);
            num -= 0.01f;
            if (num<0)
            {
                yield break;
            }
        }
    }
}
