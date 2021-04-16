using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragonFire : AllMaster
{
   
    public Transform self;
    public GameObject FireEx;
    public GameObject HaloEx;
    private GameObject ex;
    private GameObject haloex;

    private bool Efire = false;//判断特效是否出现
    public bool isHurtRange;//喷火攻击距离判定


    void Start()
    {
        halo();//光环特效
    }

    void Update()
    {
        Move();
        BigHealEffect();
        Heal();
        IceDieEffect();
        fireDieEffect();
    }

    public  void Fire()
    {
        GameObject player = GameObject.Find("Ximmerse Controller");

        isHurtRange = HurtRange.FireRange(transform, player.transform, 10, 30);//喷火技能判定距离
        if (Efire) ex.transform.up = -transform.forward; //使喷火特效跟随

        if (Efire == false && isHurtRange&&Edie==false)
        {
            //喷火特效
            ex = Common.GameObjectPool.Instance.CreateObject(FireEx.name, FireEx, transform.position+Vector3.up*1.2f, Quaternion.identity);
            ex.GetComponent<ParticleSystem>().Play(true);
            ex.transform.up = -transform.forward;
            Efire = true;
            Eattack = true;
        }
        if (Efire == true && !isHurtRange)
        {
            Common.GameObjectPool.Instance.CollectObject(ex, 0.5f);
            Efire = false;
        }
    }
    
    //光环特效
    public void halo()
    {
        haloex=Common.GameObjectPool.Instance.CreateObject(HaloEx.name,HaloEx,self.transform.position+Vector3.up*0.1f, Quaternion.identity);
        haloex.GetComponent<ParticleSystem>().Play(true);
        haloex.transform.SetParent(self.transform);
    }

    public override void IceDieEffect()
    {
        if (GameObject.FindGameObjectWithTag("icebig"))
        {
            if (Vector3.Distance(transform.position, IceBigEffect.Instance.transform.position) < IceBigEffect.Instance.range&&slow==false)
            {
                speed = 3;
                slow = true;
            }

            if (Vector3.Distance(transform.position, IceBigEffect.Instance.transform.position) > IceBigEffect.Instance.range && slow == true)
            {
                 speed = normalSpeed;
                 slow = false;
            }
        }
        else
        {
            if (slow==true)
            {
                speed = normalSpeed;
                slow = false;
            }
        }
    }

    public override void  Move()
    {
        GameObject player = GameObject.Find("Ximmerse Controller");
        var target = player.transform.position;
        target.y = transform.position.y;
        transform.LookAt(target);

        if (Vector3.Distance(transform.position, player.transform.position) > AttackRange && follow == true)
        {
            Emove = true;
            Eattack = false;
        }
        if (Vector3.Distance(transform.position, player.transform.position) <= AttackRange || follow == false)
        {
            Emove = false;
            Eattack = true;
        }

        if (Emove == true)
        {
            animator.SetBool(Istalk, true);
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
        }
        else animator.SetBool(Istalk, false);

        if (Eattack == false) animator.SetBool(Isattack, false);

        if (Vector3.Distance(transform.position, player.transform.position) <= AttackRange && Edie == false)
        {
            Fire();
            if (Eattack == true) animator.SetBool(Isattack, true);
        }

        else
        {
            Efire = false;
            Emove = true;
            Eattack = false;

            //超出攻击范围喷火特效消失
            if (ex != null && ex.activeSelf) Common.GameObjectPool.Instance.CollectObject(ex, 0.5f);
            
        }

        if (Ehit == true && currentHp > 0)
        {
            animator.SetTrigger(Ishit);
            Ehit = false;
        }

        if (Edie == true && isdeathex == false)
        {
            Skill();//死亡技能
            RandomDrop();//死亡掉落

            isdeathex = true;
            animator.SetBool(Isdie, true);

            follow = false;
            Common.GameObjectPool.Instance.CollectObject(haloex);
            if (ex != null) Common.GameObjectPool.Instance.CollectObject(ex, 0.5f);
            
            Common.GameObjectPool.Instance.CollectObject(gameObject, 1f);
            Invoke("Restart", 2f); //重置怪物状态

            EnemyCreate.Instance.DragonFires.Remove(this);
        }

    }
}
