using System.Collections;
using System.Collections.Generic;
using Michsky.UI.ModernUIPack;
using PoliceTrainingEditor.InputSystem;
using UnityEngine;
using UnityEngine.UI;

public class StartPoint : MonoBehaviour
{
    public Image loop;
    public Image loop0;
    public GameObject Canvas;

    void Update()
    {

        if (loop.fillAmount==1)
        {
            loop.fillAmount = 0;
            loop0.fillAmount = 0;
            Canvas.SetActive(false);
        }
    }

    void OnTriggerStay(Collider collider)
    {
        loop.fillAmount += Time.deltaTime * 0.5f;
    }

    void OnTriggerExit(Collider collider)
    {
        loop.fillAmount = 0;
    }
}
