using System.Collections;
using System.Collections.Generic;
using Common;
using DG.Tweening;
using PoliceTrainingEditor.InputSystem;
using UnityEngine;
using UnityEngine.UI;
using Ximmerse.RhinoX;

public class AK47 : shoot
{
    public static AK47 Instance;
    public int Damage;
    public int NornamDamage;
    private int time = 0;
    public GameObject pack;
    public GameObject FirePoint;
    public GameObject HitFx;
    private RaycastHit hit;
    public GameObject DamageFx;
    public GameObject fog;
    public GameObject Exit;
    

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        GameObject pack = GameObject.Find("Pack");
        if (pack.GetComponentInChildren<Image>().enabled == true||Exit.activeSelf||PlayerHp.Instance.isPick)
        {
            return;
        }
        Text.text = BulletNum + "/" + num; //UI界面子弹数
        count++;
        //设置射击间隔
        if (count == ShootTime)
        {
            start = true;
        }
        if (RxController.IsButtonDown(ControllerButtonCode.Trigger) || Input.GetMouseButtonDown(0) || GunInput.Instance.IsTriggerDown)
        {
            if (BulletNum==0)
            {
                Audio.PlayOneShot(Clip2);
                return;   
            }
            var fx = GameObjectPool.Instance.CreateObject(HitFx.name, HitFx,
                FirePoint.transform.position + transform.forward * 0.1f, Quaternion.Euler(90, 0, 0));
            fx.transform.forward = -FirePoint.transform.forward;
            fx.transform.SetParent(FirePoint.transform);
            fx.GetComponent<ParticleSystem>().Play(true);
            GameObjectPool.Instance.CollectObject(fx, 0.15f);

            if (Physics.Raycast(FirePoint.transform.position, FirePoint.transform.forward, out hit, 100f))
            {
                if (hit.collider.gameObject.transform.parent != null)
                {
                    if (hit.collider.gameObject.transform.parent.tag == "Scene")
                    {
                        var Fx = GameObjectPool.Instantiate(DamageFx, hit.point + Vector3.forward * 0.001f,
                            Quaternion.identity);
                        Fx.transform.forward = -hit.normal;

                        var Fog = GameObjectPool.Instance.CreateObject(fog.name, fog, Fx.transform.position,
                            Quaternion.identity);
                        Fog.GetComponent<ParticleSystem>().Play(true);
                        Destroy(Fx, 5f);
                        GameObjectPool.Instance.CollectObject(Fog, 1f);
                    }
                }
            }

            if (BulletNum > 0 && start == true)
                {
                    start = !start;
                    GameObjectPool.Instance.CreateObject(bullet.name, bullet, RxController.RaycastOrigin.position,
                        RxController.RaycastOrigin.rotation);

                    Animator.SetTrigger(Isfire); //播放射击动画
                    Audio.PlayOneShot(Clip1); //播发射击声音
                    gameObject.transform.DOShakePosition(0.2f, new Vector3(0.0025f, 0.0025f, 0.0025f));

                    BulletNum--;
                    count = 0;
                }
            }

        //子弹伤害增加计时器
        if (Damage != NornamDamage)
        {
            time++;
            if (time == 300)
            {
                Damage = NornamDamage;
                time = 0;
            }
        }
    }
    }

