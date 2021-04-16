using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Ximmerse.RhinoX;

public class shoot : MonoBehaviour
{
    
    public RXController RxController;
    public GameObject bullet;
    protected int Isfire = Animator.StringToHash("Fire");
    public Animator Animator;
    public AudioClip Clip1, Clip2;
    public AudioSource Audio;
    public int BulletNum;//当前子弹数
    public int ShootTime;
    protected int count;//时间计数器
    protected bool start = true;
    public int num ;//子弹初始数
    public TextMeshProUGUI Text;

    void Start()
    {
        RxController = transform.parent.GetComponent<RXController>();
    }
}
