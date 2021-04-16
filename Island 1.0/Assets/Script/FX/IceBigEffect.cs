using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;

public class IceBigEffect : MonoBehaviour
{
    public static IceBigEffect Instance;
    public int range = 10;
    public float speed = 0.5f;

    void Awake()
    {
        Instance = this;
    }

}
