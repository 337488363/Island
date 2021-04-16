using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoButton : MonoBehaviour
{
    public GameObject button1, button2;

    private bool isVedio = true;

    public void ButtonDown()
    {
        if (isVedio)
        {
            button1.SetActive(!isVedio);
            button2.SetActive(isVedio);
            isVedio = !isVedio;
        }
        else
        {
            button1.SetActive(!isVedio);
            button2.SetActive(isVedio);
            isVedio = !isVedio;
        }
    }
}
