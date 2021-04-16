using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : AllMaster
{

    void Update()
    {
        Move();
        Attack();
        BigHealEffect();
        Heal();
        IceDieEffect();
        fireDieEffect();

        if (Edie == true)
        {
            EnemyCreate.Instance.Orcs.Remove(this);
        }
    }

    public override void IceDieEffect()
    {
        if (IceBigEffect.Instance != null)
        {
            if (Vector3.Distance(transform.position, IceBigEffect.Instance.transform.position) <
                IceBigEffect.Instance.range && slow == false)
            {
                speed = IceBigEffect.Instance.speed;
                slow = true;
            }

            if (Vector3.Distance(transform.position, IceBigEffect.Instance.transform.position) >
                IceBigEffect.Instance.range && slow == true)
            {
                speed = normalSpeed;
                slow = false;
            }
        }
    }


    public override void Skill()
    {
        bigdeathex2 = Common.GameObjectPool.Instance.CreateObject(BigDeathEx2.name, BigDeathEx2, transform.position,
            Quaternion.Euler(-90, 0, 0));
        bigdeathex2.GetComponent<ParticleSystem>().Play(true);

        Common.GameObjectPool.Instance.CollectObject(bigdeathex2, 1.5f);
    }



}

