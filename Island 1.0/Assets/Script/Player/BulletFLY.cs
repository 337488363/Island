using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using Ximmerse.RhinoX;

public class BulletFLY : MonoBehaviour
{
    public GameObject HpHitex, DamageHitex, BulletHitex;
    public AudioClip Clip;
    public AudioSource Audio;
    public int count = 0;

    void OnEnable()
    {
        ShootEffect<Dragon>();
        ShootEffect<DragonFire>();
        ShootEffect<Orc>();
        ShootEffect<DragonIce>();
        ShootEffect<Ghost>();

        if (Physics.Raycast(transform.position, transform.forward, out var hit, 50))
        {
            var target = hit.collider.gameObject; 
            // if (target.tag=="dragon")
        //     {
        //         
        //         var enemy = target.GetComponentInChildren<Dragon>();
        //         if (enemy != null)
        //         {
        //             enemy.GetHit(AK47.Instance.Damage, hit.point, hit.normal);
        //
        //             if (enemy.currentHp == enemy.maxHp)
        //             {
        //                 enemy.Edie = false;
        //             }
        //             if (enemy.currentHp <= 0 && enemy.Edie == false)
        //             {
        //                 enemy.Edie = true;
        //             }
        //         }
        //     }
        //
        //     if (target.tag == "ghost")
        //     {
        //         var enemy = target.GetComponentInChildren<Ghost>();
        //         if (enemy != null)
        //         {
        //             enemy.GetHit(AK47.Instance.Damage, hit.point, hit.normal);
        //
        //             if (enemy.currentHp == enemy.maxHp)
        //             {
        //                 enemy.Edie = false;
        //             }
        //             if (enemy.currentHp <= 0 && enemy.Edie == false)
        //             {
        //                 enemy.Edie = true;
        //             }
        //         }
        //     }
        //
        //     if (target.tag == "dragonfire")
        //     {
        //         var enemy = target.GetComponentInChildren<DragonFire>();
        //         if (enemy != null)
        //         {
        //             enemy.GetHit(AK47.Instance.Damage, hit.point, hit.normal);
        //
        //             if (enemy.currentHp == enemy.maxHp)
        //             {
        //                 enemy.Edie = false;
        //             }
        //             if (enemy.currentHp <= 0 && enemy.Edie == false)
        //             {
        //                 enemy.Edie = true;
        //             }
        //         }
        //     }
        //
        //     if (target.tag == "wolf")
        //     {
        //         var enemy = target.GetComponentInChildren<DragonIce>();
        //         if (enemy != null)
        //         {
        //             enemy.GetHit(AK47.Instance.Damage, hit.point, hit.normal);
        //
        //             if (enemy.currentHp == enemy.maxHp)
        //             {
        //                 enemy.Edie = false;
        //             }
        //             if (enemy.currentHp <= 0 && enemy.Edie == false)
        //             {
        //                 enemy.Edie = true;
        //             }
        //         }
        //     }
        //
        //     if (target.tag == "orc")
        //     {
        //         var enemy = target.GetComponentInChildren<Orc>();
        //         if (enemy != null)
        //         {
        //             enemy.GetHit(AK47.Instance.Damage, hit.point, hit.normal);
        //
        //             if (enemy.currentHp == enemy.maxHp)
        //             {
        //                 enemy.Edie = false;
        //             }
        //             if (enemy.currentHp <= 0 && enemy.Edie == false)
        //             {
        //                 enemy.Edie = true;
        //             }
        //         }
        //     }

        if (target.tag == "hpdrop")
            {
                DropHit(HpHitex, hit);
                PlayerHp.Instance.CurrentHp += DropEffect.Instance.HpDropRange;
                if (PlayerHp.Instance.CurrentHp > PlayerHp.Instance.Hp)
                {
                    PlayerHp.Instance.CurrentHp = PlayerHp.Instance.Hp;
                }
                PlayerHp.Instance.bar.currentPercent = PlayerHp.Instance.CurrentHp;
                Audio.PlayOneShot(Clip);
            }

            if (target.tag == "boultdrop")
            {
                DropHit(BulletHitex, hit);
                AK47.Instance.BulletNum += DropEffect.Instance.BoultDropRange;
                if (AK47.Instance.BulletNum>AK47.Instance.num)
                {
                    AK47.Instance.BulletNum = AK47.Instance.num;
                }
                Audio.PlayOneShot(Clip);
            }
            
            if (target.tag == "damagedrop")
            {
                DropHit(DamageHitex, hit);
                AK47.Instance.Damage += DropEffect.Instance.DamageDropRange;
                Audio.PlayOneShot(Clip);
            }
        }
        Common.GameObjectPool.Instance.CollectObject(gameObject);
    }

    public void DropHit(GameObject obj, RaycastHit hit)
    {
        Common.GameObjectPool.Instance.CreateObject(obj.name, obj, hit.collider.gameObject.transform.position,
            Quaternion.identity);
        Common.GameObjectPool.Instance.CollectObject(hit.collider.gameObject, 0);
        Common.GameObjectPool.Instance.CollectObject(obj, 2f);
    }


    public void ShootEffect<T>() where T : AllMaster
    {
        if (Physics.Raycast(transform.position, transform.forward, out var hit, 50))
        {
            var enemy = hit.collider.gameObject.GetComponent<T>();
            if (enemy != null)
            {
                enemy.GetHit(AK47.Instance.Damage, hit.point, hit.normal);

                if (enemy.currentHp == enemy.maxHp)
                {
                    enemy.Edie = false;
                }

                if (enemy.currentHp <= 0 && enemy.Edie == false)
                {
                    PlayerHp.Instance.ScoreAdd(enemy.Score);
                    enemy.Edie = true;
                }
            }
        }
    }

}
