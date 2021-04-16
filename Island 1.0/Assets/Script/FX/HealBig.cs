using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBig : MonoBehaviour
{
    public static HealBig Instance;

    public int HealDamage;
    public int HealRange;

    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
