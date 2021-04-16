using System.Collections;
using System.Collections.Generic;
using PoliceTrainingEditor.InputSystem;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public GameObject FirePoint;
    public GameObject aim;

    void OnTriggerStay(Collider collider)
    {
        if (collider)
        {
            aim.SetActive(true);
            if (Physics.Raycast(FirePoint.transform.position, FirePoint.transform.forward, out var hit, 100f))
            {
                if (GunInput.Instance.IsTriggerDown || Input.GetMouseButtonDown(0))
                {
                    if (hit.collider.gameObject.name == "aim")
                    {
                        Quaternion calibratedRotation = Quaternion.LookRotation(
                            (aim.transform.position -
                             FirePoint.transform.parent.localToWorldMatrix.MultiplyPoint(FirePoint.transform
                                 .localPosition)).normalized,
                            Vector3.up);
                        FirePoint.transform.rotation = calibratedRotation;
                        aim.SetActive(false);
                        gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
