using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropEffect : MonoBehaviour
{
    public static DropEffect Instance;
    public int HpDropRange;
    public int BoultDropRange;
    public int DamageDropRange;



    void Awake()
    {
        Instance = this;
    }
}
