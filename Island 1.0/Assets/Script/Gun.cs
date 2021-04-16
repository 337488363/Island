using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PoliceTrainingEditor.InputSystem;
using UnityEngine;
using Ximmerse.RhinoX;

public class Gun : MonoBehaviour
{
    public GameObject FirePoint;
    public Transform OldGun, NewGun;

    public RXController Controller;

    private TrackableIdentity _controllerTrackable;

    // Start is called before the first frame update
    void Start()
    {
        _controllerTrackable = Controller.gameObject.GetComponent<TrackableIdentity>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (GunInput.Instance.IsTriggerDown)
        {
            SetGunTrackId();
            
        }

    }

    private void SetGunTrackId()
    {
        if (RXController.GetControllerTrackIDByType(0x0105) > 0 && _controllerTrackable.TrackableID != RXController.GetControllerTrackIDByType(0x0105))
        {
            _controllerTrackable.TrackableID = RXController.GetControllerTrackIDByType(0x0105);

            if (_controllerTrackable.TrackableID == 91)
            {
                FirePoint.transform.localPosition = OldGun.localPosition;
                FirePoint.transform.localRotation = OldGun.localRotation;
            }
            else
            {
                FirePoint.transform.localPosition = NewGun.localPosition;
                FirePoint.transform.localRotation = NewGun.localRotation;
            }
        }
        else
        {
           
        }
    }
}
