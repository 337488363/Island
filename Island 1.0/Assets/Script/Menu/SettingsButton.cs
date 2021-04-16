using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    public CameraRotate Camera;

    public void Down()
    {
        Camera.enabled = true;
    }
}
