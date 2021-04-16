using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectItem : MonoBehaviour
{
    public int objID;
    public int objCount;
    public Sprite MySprite;
    public string Name;
    public bool ispick;
    public string Tooltip;


    void Update()
    {

        if (ispick)
        {
            gameObject.GetComponent<MeshRenderer>().material.color=Color.red;
            gameObject.GetComponent<MeshRenderer>().material.SetColor("_ColorTint",new Color(191,3,3,0));
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material.color = new Color(1,1,1,1);
            gameObject.GetComponent<MeshRenderer>().material.SetColor("_ColorTint", new Color(1, 1, 1, 1));
        }
        ispick = false;
    }

}
