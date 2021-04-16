using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalParameter : MonoBehaviour
{
    public static GlobalParameter Instance;
    public bool isGameOver;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }
}
