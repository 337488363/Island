using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MasterDiffcultManage : MonoBehaviour
{
    public int diffculty;

    [HideInInspector]
    public Dragon dragon;
    [HideInInspector]
    public Orc orc;
    [HideInInspector]
    public DragonFire dragonFire;
    [HideInInspector]
    public DragonIce dragonIce;
    [HideInInspector]
    public Ghost ghost;

    //根据难度获取怪物各项数据
    public void GetMaster<T>(T item) where T : AllMaster
    {
             if (typeof(T)==typeof(Dragon))
            {
                switch (diffculty)
                {
                case 1:
                    item.Damage = 5;
                    item.normalSpeed = Random.Range(1, 1.2f);//正常移动速度
                    item.speed = Random.Range(1, 1.2f);//当前移动速度
                    break;
                case 2:
                    item.Damage = 6;
                    item.normalSpeed = Random.Range(1, 1.4f);
                    item.speed = Random.Range(1, 1.4f);
                    break;
                case 3:
                    item.Damage = 7;
                    item.normalSpeed = Random.Range(1.2f, 1.4f);
                    item.speed = Random.Range(1.2f, 1.4f);
                    break;
                case 4:
                    item.Damage = 8;
                    item.normalSpeed = Random.Range(1.2f, 1.6f);
                    item.speed = Random.Range(1.2f, 1.6f);
                    break;
                case 5:
                    item.Damage = 9;
                    item.normalSpeed = Random.Range(1.6f, 2f);
                    item.speed = Random.Range(1.6f, 2f);
                    break;
            }
            }

             if (typeof(T) == typeof(Orc))
             {
                 switch (diffculty)
                 {
                     case 1:
                         item.Damage = 5;
                         item.normalSpeed = Random.Range(1, 1.2f);//正常移动速度
                         item.speed = Random.Range(1, 1.2f);//当前移动速度
                         break;
                     case 2:
                         item.Damage = 6;
                         item.normalSpeed = Random.Range(1, 1.2f);
                         item.speed = Random.Range(1, 1.2f);
                         break;
                     case 3:
                         item.Damage = 7;
                         item.normalSpeed = Random.Range(1.2f, 1.4f);
                         item.speed = Random.Range(1.2f, 1.4f);
                         break;
                     case 4:
                         item.Damage = 8;
                         item.normalSpeed = Random.Range(1.2f, 1.6f);
                         item.speed = Random.Range(1.2f, 1.6f);
                         break;
                     case 5:
                         item.Damage = 9;
                         item.normalSpeed = Random.Range(1.6f, 2f);
                         item.speed = Random.Range(1.6f, 2f);
                         break;
                 }
             }

             if (typeof(T) == typeof(Ghost))
             {
                 switch (diffculty)
                 {
                     case 1:
                         item.Damage = 10;
                         item.normalSpeed = Random.Range(1, 1.2f);//正常移动速度
                         item.speed = Random.Range(1, 1.2f);//当前移动速度
                         break;
                     case 2:
                         item.Damage = 11;
                         break;
                     case 3:
                         item.Damage = 12;
                         break;
                     case 4:
                         item.Damage = 13;
                         break;
                     case 5:
                         item.Damage = 14;
                         break;
                 }
             }

             if (typeof(T) == typeof(DragonFire))
             {
                switch (diffculty)
                {
                case 1:
                    item.Damage = 5;
                    item.normalSpeed = Random.Range(1, 1.2f);//正常移动速度
                    item.speed = Random.Range(1, 1.2f);//当前移动速度
                    break;
                case 2:
                    item.Damage = 6;
                    item.normalSpeed = Random.Range(1, 1.2f);
                    item.speed = Random.Range(1, 1.2f);
                    break;
                case 3:
                    item.Damage = 7;
                    item.normalSpeed = Random.Range(1.2f, 1.4f);
                    item.speed = Random.Range(1.2f, 1.4f);
                    break;
                case 4:
                    item.Damage = 8;
                    item.normalSpeed = Random.Range(1.2f, 1.6f);
                    item.speed = Random.Range(1.2f, 1.6f);
                    break;
                case 5:
                    item.Damage = 9;
                    item.normalSpeed = Random.Range(1.6f, 2f);
                    item.speed = Random.Range(1.6f, 2f);
                    break;
            }
             }

             if (typeof(T) == typeof(DragonIce))
             {
                switch (diffculty)
                {
                case 1:
                    item.Damage = 5;
                    item.normalSpeed = Random.Range(1, 1.2f);//正常移动速度
                    item.speed = Random.Range(1, 1.2f);//当前移动速度
                    break;
                case 2:
                    item.Damage = 6;
                    item.normalSpeed = Random.Range(1, 1.2f);
                    item.speed = Random.Range(1, 1.2f);
                    break;
                case 3:
                    item.Damage = 7;
                    item.normalSpeed = Random.Range(1.2f, 1.4f);
                    item.speed = Random.Range(1.2f, 1.4f);
                    break;
                case 4:
                    item.Damage = 8;
                    item.normalSpeed = Random.Range(1.2f, 1.6f);
                    item.speed = Random.Range(1.2f, 1.6f);
                    break;
                case 5:
                    item.Damage = 9;
                    item.normalSpeed = Random.Range(1.6f, 2f);
                    item.speed = Random.Range(1.6f, 2f);
                    break;
            }
             }

           
    }

    public void GetBorn()//根据难度获取生成数据
    {
        switch (diffculty)
        {
            case 1:
                dragon.rate = 7;
                dragon.maxCreate =2 ;

                orc.rate = 7;
                orc.maxCreate = 0;

                ghost.rate = 0;
                ghost.maxCreate = 0;

                dragonFire.rate = 0;
                dragonFire.maxCreate = 0;

                dragonIce.rate = 0;
                dragonIce.maxCreate = 0;
                break;

            case 2:
                dragon.rate = 6;
                dragon.maxCreate = 3;

                orc.rate = 6;
                orc.maxCreate = 3;

                ghost.rate = 0;
                ghost.maxCreate = 0;

                dragonFire.rate = 0;
                dragonFire.maxCreate = 0;

                dragonIce.rate = 0;
                dragonIce.maxCreate = 0;
                break;
            case 3:
                dragon.rate = 5;
                dragon.maxCreate = 3;

                orc.rate = 5;
                orc.maxCreate = 3;

                ghost.rate = 5;
                ghost.maxCreate = 1;

                dragonFire.rate = 5;
                dragonFire.maxCreate = 1;

                dragonIce.rate = 5;
                dragonIce.maxCreate = 1;
                break;
            case 4:
                dragon.rate = 4;
                dragon.maxCreate = 3;

                orc.rate = 4;
                orc.maxCreate = 3;

                ghost.rate = 5;
                ghost.maxCreate = 1;

                dragonFire.rate = 5;
                dragonFire.maxCreate = 1;

                dragonIce.rate = 5;
                dragonIce.maxCreate = 1;
                break;
            case 5:
                dragon.rate = 7;
                dragon.maxCreate = 3;

                orc.rate = 7;
                orc.maxCreate = 3;

                ghost.rate = 7;
                ghost.maxCreate = 2;

                dragonFire.rate = 5;
                dragonFire.maxCreate = 1;

                dragonIce.rate = 5;
                dragonIce.maxCreate = 1;
                break;
        }
    }
}
