using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    private Quaternion player;
    private Quaternion target;

    private float speed = 50f;
    private float lerp_speed = 0;
    private float lerp;

    public GameObject TargetgGameObject;
    public GameObject PlayGameObject;

    public void Start()
    {
        player = PlayGameObject.transform.rotation;
        PlayGameObject.transform.LookAt(TargetgGameObject.transform);
        target = PlayGameObject.transform.rotation;
        PlayGameObject.transform.rotation = player;
        float RotateTransform = Quaternion.Angle(player, target);
        lerp_speed = speed / RotateTransform;
    }

    void Update()
    {
        lerp += Time.deltaTime * lerp_speed;
        PlayGameObject.transform.rotation = Quaternion.Lerp(player, target, lerp);
    }
}
