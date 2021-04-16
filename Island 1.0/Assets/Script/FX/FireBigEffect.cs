using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBigEffect : MonoBehaviour
{
    public static FireBigEffect Instance;
    public delegate void FireEffectHandler(int num);
    FireEffectHandler fireEffect;
    public int damage = 5;
    public int range = 10;

    void Awake()
    {
        Instance = this;
    }

    
}
