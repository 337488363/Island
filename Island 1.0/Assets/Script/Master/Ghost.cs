using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ghost : AllMaster
   {
       public GameObject self;
       public GameObject ConjureEx;
       private GameObject conjureex;


       void Start()
    {
        Conjure();//施法特效
    }

    void Update()
    {
        Move();
        Heal();
        BigHealEffect();
        fireDieEffect();

    }


    public void Conjure()
    {
        conjureex = Common.GameObjectPool.Instance.CreateObject(ConjureEx.name, ConjureEx,self.transform.position, Quaternion.identity);
        conjureex.GetComponent<ParticleSystem>().Play(true);
        conjureex.transform.SetParent(self.transform);
    }

    public override void Move()
    {

        GameObject player = GameObject.Find("Ximmerse Controller");
        var target = player.transform.position;
        target.y = transform.position.y;
        transform.LookAt(target);

        if (Vector3.Distance(transform.position, player.transform.position) > AttackRange) Eattack = false;
        
        if (Vector3.Distance(transform.position, player.transform.position) <= AttackRange) Eattack = true;

        if (Eattack == false) animator.SetBool(Isattack, false);

        if (Vector3.Distance(transform.position, player.transform.position) <= AttackRange)
            if (Eattack == true) animator.SetBool(Isattack, true);
            else Eattack = false;

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
            Common.GameObjectPool.Instance.CollectObject(conjureex);

            Common.GameObjectPool.Instance.CollectObject(gameObject, 1f);
            Invoke("Restart", 2f); //重置怪物状态
        }

        if (Edie)
        {
            EnemyCreate.Instance.Ghosts.Remove(this);
        }
    }



}


