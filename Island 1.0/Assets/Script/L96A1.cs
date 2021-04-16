using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ximmerse.RhinoX;

public class L96A1 : shoot
{
    void Update()
    {
        count++;
        //设置射击间隔
        if (count == ShootTime)
        {
            start = true;
        }
        if ((RxController.IsButtonDown(ControllerButtonCode.Trigger) || Input.GetKeyDown(KeyCode.Space)) && BulletNum > 0 && start == true)
        {
            start = !start;
            Common.GameObjectPool.Instance.CreateObject(bullet.name, bullet, RxController.RaycastOrigin.position, RxController.RaycastOrigin.rotation);

            Animator.SetTrigger(Isfire);//播放射击动画
            Audio.PlayOneShot(Clip1);//播发射击声音

            BulletNum--;
            count = 0;
        }

        if ((RxController.IsButtonDown(ControllerButtonCode.Trigger) || Input.GetKeyDown(KeyCode.Space)) && BulletNum == 0)
        {
            Audio.PlayOneShot(Clip2);//播放空弹夹声音

        }
    }
}
