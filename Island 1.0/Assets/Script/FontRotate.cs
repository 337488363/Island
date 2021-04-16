using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FontRotate : MonoBehaviour
{
    public Transform Transform;

    public float xAngle;
    public float yAngleleft;
    public float yAngleright;
    public float zAngle;
    

    private bool count=true;
    
    private float yrotate;
  
    void Start()
    {
        
    }

    void Update()
    {
        yrotate = transform.rotation.y;
        if (yrotate >0.2 )
        {
            count = false;
        }
        if (yrotate <-0.2 )
        {
            count = true;
        }
        if (count==true)
        {
            transform.Rotate(xAngle,yAngleleft,zAngle);
        }
        if (count==false)
        {
            transform.Rotate(xAngle, yAngleright, zAngle);
        }
        
    }
}
