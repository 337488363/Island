using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyCreate : MonoBehaviour
{
    public GameObject[] OrcRange;
    public GameObject[] DragonRange;
    public GameObject[] DragonFireRange;
    public GameObject[] DragonIceRange;
    public GameObject[] GhostRange;

    protected float time = 0;
    public int rank = 1;
    public bool Create = false;
    
    public List<AllMaster> All;
    public List<Dragon> Dragons;
    public List<Orc> Orcs;
    public List<Ghost> Ghosts;
    public List<DragonFire> DragonFires;
    public List<DragonIce> DragonIces;


    public GameObject Dragon, Orc, Ghost, DragonFire, DragonIce;
    private MasterDiffcultManage MasterManage;

    public static EnemyCreate Instance;
    
    public bool isStart = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        MasterManage = GetComponent<MasterDiffcultManage>();
    }
    void Update()
    {
        if (CountDown.Instance.num==0&& !Create)
        {
            Create = !Create;
            switch (rank)//第几关
            {
                case 1:
                    MasterManage.diffculty = 1;//设定当前关卡的难度
                    MasterManage.GetBorn();//获取当前难度的各项数据
                    StartGame();
                    break;

                case 2:
                    MasterManage.diffculty = 2;
                    MasterManage.GetBorn();
                    StartGame();
                    break;
                case 3:
                    MasterManage.diffculty = 3;
                    MasterManage.GetBorn();
                    StartGame();
                    break;
                case 4:
                    MasterManage.diffculty = 4;
                    MasterManage.GetBorn();
                    StartGame();
                    break;
                case 5:
                    MasterManage.diffculty = 5;
                    MasterManage.GetBorn();
                    break;
            }
        }
        
        if (Gametime.Instance.num<=0&&!isStart)
        {
            StopAllCoroutines();
            AllDie();//杀死所有场景中的怪物
            All.Clear();
            isStart = !isStart;
        }
    }


    public IEnumerator CreateNum<T>(float rate, int maxCreate, List<T> list, GameObject monster, GameObject[] range) where T : AllMaster
    {
        while (Application.isPlaying)
        {
            if (list.Count < maxCreate)
            {
                var enemy = Common.GameObjectPool.Instance.CreateObject(monster.name, monster,
                    range[Random.Range(0, 2)].transform.position,
                    Quaternion.identity);
                var master = enemy.GetComponent<T>();
                MasterManage.GetMaster(master);
                list.Add(master);
                All.Add(enemy.GetComponent<AllMaster>());
            }
            yield return new WaitForSeconds(rate);
        }
    }

    public void AllDie()
    {
        if (All!=null)
        {
            for (int i = 0; i < All.Count; i++)
            {
                All[i].Edie = true;
            }
        }
    }

    public void StartGame()
    {
        StartCoroutine(CreateNum(MasterManage.dragon.rate, MasterManage.dragon.maxCreate, Dragons, Dragon, DragonRange));
        StartCoroutine(CreateNum(MasterManage.orc.rate, MasterManage.orc.maxCreate, Orcs, Orc, OrcRange));
        StartCoroutine(CreateNum(MasterManage.ghost.rate, MasterManage.ghost.maxCreate, Ghosts, Ghost, GhostRange));
        StartCoroutine(CreateNum(MasterManage.dragonFire.rate, MasterManage.dragonFire.maxCreate, DragonFires, DragonFire, DragonFireRange));
        StartCoroutine(CreateNum(MasterManage.dragonIce.rate, MasterManage.dragonIce.maxCreate, DragonIces, DragonIce, DragonIceRange));
    }

}
