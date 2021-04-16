using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Removecollider : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    void Start()
    {
         canvasGroup = gameObject.GetComponentInParent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canvasGroup != null&& gameObject.GetComponent<BoxCollider>()!=null)
        {
            if (canvasGroup.alpha != 1)
            {
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
            else
            {
                gameObject.GetComponent<BoxCollider>().enabled = true;
            }
        }
    }
}
