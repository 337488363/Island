using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFly : MonoBehaviour
{
    public Transform[] target;
    public Transform dragon;
    public float speed;
    public float rotatespeed;
    private int index = 0;

    void Start()
    {

    }

    void Update()
    {
        dragon.localPosition = Vector3.MoveTowards(dragon.localPosition, target[index].localPosition, Time.deltaTime * speed);
        dragon.localRotation = Quaternion.Slerp(dragon.localRotation, target[index].localRotation, Time.deltaTime * rotatespeed);
        if (index == target.Length - 1&& Vector3.Distance(dragon.localPosition, target[index].localPosition) < 0.01f)
        {
            index = 0;
        }
        if (Vector3.Distance(dragon.localPosition, target[index].localPosition) < 0.01f)
        {
            index++;
        }

    }

}
