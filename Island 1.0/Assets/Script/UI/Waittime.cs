using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Waittime : MonoBehaviour
{
    public int num ;

    public TextMesh Text;

    public static Waittime Instance;
    public bool isstart = false;
    public bool ischange = false;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Gametime.Instance.Text.text==""&&!isstart)
        {
            isstart = true;
            StartCoroutine(waittime());
        }

        if (num <= 0 && !ischange)
        {
            Text.text ="";
            Gametime.Instance.num = 10;
            Gametime.Instance.isStart = false;
            Gametime.Instance.ischange = false;
            EnemyCreate.Instance.Create = false;
            EnemyCreate.Instance.isStart = false;
            ischange = !ischange;
        }
    }

    IEnumerator waittime()
    {
        while (Application.isPlaying)
        {
            Text.text = "离下一波怪物还有" + num.ToString() + "秒";
            yield return new WaitForSeconds(1);
            num--;
            if (num<=0)
            {
                yield break;
            }
        }
    }
}
