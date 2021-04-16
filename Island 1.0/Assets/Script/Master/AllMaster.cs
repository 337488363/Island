using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class AllMaster : MonoBehaviour
{
    public static AllMaster Instance;
    public Animator animator;

    //特效
    public GameObject HitEx;
    public GameObject HealEx;
    public GameObject BigDeathEx, BigDeathEx2;
    protected GameObject bigdeathex, bigdeathex2;
    protected GameObject healex;
    public GameObject HpDrop, DamageDrop, BulletDrop;
    public Slider HpSlider;

    //转换
    protected int Istalk = Animator.StringToHash("talk");
    protected int Isattack = Animator.StringToHash("attack");
    protected int Ishit = Animator.StringToHash("hit");
    protected int Isdie = Animator.StringToHash("die");

    //动画
    private bool isbigheal = false;
    protected bool Emove = true;
    protected bool Eattack = false;
    protected bool Ehit = false;
    public bool Edie = false;
    protected bool follow = true;
    protected bool slow = false;
    protected bool firebighurt = false;
    protected bool isdeathex = false;
    public bool Eheal = false;

    public int Damage;//攻击伤害
    public float normalSpeed;//正常速度
    public float speed; //当前移动速度
    public int AttackRange; //攻击距离
    public int GhostDamage = 10;//恢复数值
    public int currentHp; //初始血量
    public int maxHp; //最大血量
    private int a = 0; //范围治疗计数器
    public int DropOdd = 3;//掉落概率，数组越大概率越小
    public float rate;//当前怪物生成时间间隔
    public int maxCreate;//当前怪物存在最大数量
    public int Score;

    void Awake()
    {
        Instance = this;
    }

    public virtual void IceDieEffect()
    {

    }

    public virtual void Attack()
    {
        GameObject play = GameObject.Find("Ximmerse Controller");
        if (Vector3.Distance(transform.position, play.transform.position) > AttackRange && follow == true)
        {
            Emove = true;
            Eattack = false;
        }

        if (Vector3.Distance(transform.position, play.transform.position) <= AttackRange || follow == false)
        {
            Emove = false;
            Eattack = true;
            if (Eattack == true) animator.SetBool(Isattack, true);
        }


        if (Eattack == false) animator.SetBool(Isattack, false);

        if (Ehit == true && currentHp > 0)
        {
            animator.SetTrigger(Ishit);
            Ehit = false;
        }

        if (Edie == true && isdeathex == false)
        {
            Skill(); //死亡技能
            RandomDrop();//死亡掉落
            isdeathex = true;
            animator.SetBool(Isdie, true);
            follow = false;
        }
    }


    public virtual void Move()
    {
        GameObject play = GameObject.Find("Ximmerse Controller");
        var target = play.transform.position;
        target.y = transform.position.y;
        transform.LookAt(target);

        if (Emove == true)
        {
            animator.SetBool(Istalk, true);
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
        }
        else animator.SetBool(Istalk, false);

        if (Edie==true)
        {
            Common.GameObjectPool.Instance.CollectObject(gameObject, 1f);
            Invoke("Restart",2f); //重置怪物状态
        }
    }

    public virtual void Skill()
    {
        bigdeathex = Common.GameObjectPool.Instance.CreateObject(BigDeathEx.name, BigDeathEx, transform.position+Vector3.down*0.2f, Quaternion.Euler(-90, 0, 0));
        bigdeathex.GetComponent<ParticleSystem>().Play(true);

        bigdeathex2 = Common.GameObjectPool.Instance.CreateObject(BigDeathEx2.name, BigDeathEx2, transform.position + Vector3.up, Quaternion.Euler(-90, 0, 0));
        bigdeathex2.GetComponent<ParticleSystem>().Play(true);

        Common.GameObjectPool.Instance.CollectObject(bigdeathex, 100f);
        Common.GameObjectPool.Instance.CollectObject(bigdeathex2, 3f);
    }

    public void BigHealEffect()
    {
        a++;
        if (GameObject.FindGameObjectWithTag("bigheal")  && Edie == false && a > 100&&isbigheal==false)
        {
            if (HurtRange.HealRange(transform, HealBig.Instance.transform, HealBig.Instance.HealRange) && currentHp < maxHp)
            {
                a = 0;
                currentHp += HealBig.Instance.HealDamage;
                HpSlider.value = currentHp;
                isbigheal = true;
            }
            else isbigheal = false;
        }
        else
        {
            if (isbigheal) isbigheal = !isbigheal;
        }

    }

    public void Heal()
    {
        
            if (GameObject.FindGameObjectWithTag("ghost"))
        {
            if (currentHp < maxHp && Eheal == false)
            {
                if (gameObject.tag == "ghost" || gameObject.tag == "wolf"||gameObject.tag=="orc")
                    healex = GameObjectPool.Instance.CreateObject(HealEx.name, HealEx,
                        gameObject.transform.position + Vector3.up, Quaternion.identity);
                else
                    healex = GameObjectPool.Instance.CreateObject(HealEx.name, HealEx,
                        gameObject.transform.position + Vector3.up * 3, Quaternion.identity);
    
                healex.GetComponent<ParticleSystem>().Play(true);
                healex.transform.SetParent(gameObject.transform); //把特效放置自己身上
                Eheal = true;
            }
    
            if ((currentHp <= 0 || currentHp == maxHp) && Eheal == true)
            {
                GameObjectPool.Instance.CollectObject(healex, 0);
                Eheal = false;
            }
        }
            else
            {
              if (Eheal) 
              { 
                  GameObjectPool.Instance.CollectObject(healex, 0);
                  Eheal = !Eheal;
              }
            }
    }

    public void GetHit(int damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        //怪物喷血特效
        if (HitEx != null)
        {
            var hitex = Common.GameObjectPool.Instance.CreateObject(HitEx.name, HitEx, hitPoint, Quaternion.identity);
            hitex.transform.forward = hitNormal;
            hitex.GetComponent<ParticleSystem>().Play(true);
            Common.GameObjectPool.Instance.CollectObject(hitex, 0.5f);
        }
        //怪物被击中动画
        Ehit = true;
        Eattack = false;
        Emove = false;

        //怪物血条变化
        currentHp -= damage;
        HpSlider.value = currentHp;

    }


    public void fireDieEffect()
    {
        if (GameObject.FindGameObjectWithTag("firebig"))
        {
            if (Vector3.Distance(transform.position, FireBigEffect.Instance.transform.position) < FireBigEffect.Instance.range && firebighurt==false)
            {
                a++;
                if (a>=100&&currentHp>0)
                {
                    currentHp -= FireBigEffect.Instance.damage;
                    HpSlider.value = currentHp;
                    a = 0;
                }

                if (currentHp<=0)
                {
                    Edie = true;
                }
            }
        }
        else
        {
            if (firebighurt == true)
            {
                firebighurt = false;
            }
        }
    }

    //掉落概率
    public void RandomDrop()
    {
        int num = Random.Range(0, DropOdd);
        switch (num)
        {
            case 0:
                hplootdrop();
                break;
            case 1:
                Bulletlootdrop();
                break;
            case 2:
                damagelootdrop();
                break;
        }
    }

    void hplootdrop()
    {
        Common.GameObjectPool.Instance.CreateObject(HpDrop.name, HpDrop, gameObject.transform.position + Vector3.up * 3,
            Quaternion.identity);
    }

    void Bulletlootdrop()
    {
        Common.GameObjectPool.Instance.CreateObject(BulletDrop.name, BulletDrop, gameObject.transform.position + Vector3.up * 3,
            Quaternion.identity);
    }

    void damagelootdrop()
    {
        Common.GameObjectPool.Instance.CreateObject(DamageDrop.name, DamageDrop, gameObject.transform.position + Vector3.up * 3,
            Quaternion.identity);
    }

    void Restart()
    {
        currentHp = maxHp;
        HpSlider.value = currentHp;
        Edie = false;
        follow = true;
        isdeathex = false;
    }

    public void ScoreAdd(int score)
    {
        Debug.Log(Score);
        score += Score;
        PlayerHp.Instance.Sum = score;
    }

}




