using System.Collections;
using System.Collections.Generic;
using Michsky.UI.ModernUIPack;
using UnityEngine;

public class Swtich : MonoBehaviour
{
    private GameObject Obj;
    public SwitchManager Switch;
    public static bool isRay;

    void Start()
    {
        Obj=GameObject.Find("Line");
    }
    void Update()
    {
        isRay = Switch.isOn;
    }

}

