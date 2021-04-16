using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtRange : MonoBehaviour
{
    //喷火攻击范围
    public static bool FireRange(Transform self, Transform target, float maxDistance, float maxAngle)
    {
        return FireRange( self, target, 0 , maxDistance, maxAngle);
    }

    public static bool FireRange(Transform self, Transform target, float minDistance, float maxDistance, float maxAngle)
    {
        Vector3 selfDir = self.forward;
        Vector3 targetDir = (target.position-self.position).normalized;

        float angle = Vector3.Angle(selfDir, targetDir);
        if (angle>maxAngle*0.5f)
        {
            return false;
        }

        float distance = Vector3.Distance(self.position, target.position);
        if (distance<maxDistance&&distance>=minDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public static bool HealRange(Transform self,Transform target,int healrange)
    {
        if (Vector3.Distance(self.position, target.position)>healrange)
        {
            return false;
        }

        return true;
    }
}
