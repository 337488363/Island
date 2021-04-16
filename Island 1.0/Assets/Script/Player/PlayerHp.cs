using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using DG.Tweening;
using Michsky.UI.ModernUIPack;
using PoliceTrainingEditor.InputSystem;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.UI;
using Ximmerse.RhinoX;


public class PlayerHp : MonoBehaviour
{
    public static PlayerHp Instance;
    private GameObject Obj;
    public ProgressBar bar;
    public int Hp = 100;
    public int CurrentHp = 100;
    public int speed;
    private RXController controller;
    public GameObject PackGameObject; //背包框
    public GameObject ShopGameObject; //商店框
    public GameObject Exit;
    public GameObject DieUI;
    public GameObject Tip;
    private Shop shop;

    private CanvasRenderer[] Renderers;
    private Image[] Images;
    private TextMeshProUGUI[] TextMeshProUguis;

    private CanvasRenderer[] shopRenderers;
    private Image[] shopImages;
    private TextMeshProUGUI[] shopMeshProUguis;

    public Sprite SpriteNone;
    public Pack Pack;
    public Transform tr;
    private RaycastHit hit;
    private Button[] Buttons;
    private bool isPackup = false;
    private bool isShopPackup = false;
    public CanvasGroup RedImage;
    private bool on = false;
    public int Sum = 0;
    public bool isPick;
    private float UIdistance = 1.4f;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        shop = GameObject.FindObjectOfType<Shop>();
        Obj = GameObject.Find("Line");
        controller = GetComponent<RXController>();
        Buttons = GameObject.Find("CollectPack").GetComponentsInChildren<Button>();
        shopRenderers = ShopGameObject.GetComponentsInChildren<CanvasRenderer>();
        shopImages = ShopGameObject.GetComponentsInChildren<Image>();
        shopMeshProUguis = ShopGameObject.GetComponentsInChildren<TextMeshProUGUI>();
        for (int i = 0; i < shopRenderers.Length; i++)
        {
            shopRenderers[i].materialCount = 0;
        }

        for (int i = 0; i < shopImages.Length; i++)
        {
            shopImages[i].enabled = false;
        }

        for (int i = 0; i < shopMeshProUguis.Length; i++)
        {
            shopMeshProUguis[i].enabled = false;
        }

        Renderers = PackGameObject.GetComponentsInChildren<CanvasRenderer>();
        Images = PackGameObject.GetComponentsInChildren<Image>();
        TextMeshProUguis = PackGameObject.GetComponentsInChildren<TextMeshProUGUI>();
        for (int i = 0; i < Renderers.Length; i++)
        {
            Renderers[i].materialCount = 0;
        }

        for (int i = 0; i < Images.Length; i++)
        {
            Images[i].enabled = false;
        }

        for (int i = 0; i < TextMeshProUguis.Length; i++)
        {
            TextMeshProUguis[i].enabled = false;
        }
    }

    void Update()
    {
        isPick = false;
        Obj.SetActive(Swtich.isRay);
        Move();
        Pick();
        if (RedImage.alpha == 1)
        {
            StartCoroutine(Fade(RedImage, 0, 0.5f));
        }
    }

    public void GetHp(int damage)
    {
        RedImage.alpha = 0;
        //玩家血条变化
        CurrentHp -= damage;
        if (CurrentHp < 0)
        {
            DieUI.SetActive(true);
            DieUI.transform.DOLocalMove(new Vector3(0, 0, UIdistance), 0.25f)
            .OnComplete(() =>
            {
                foreach (PackItem item in shop.packItems)
                {
                    if (item.objId == 2)
                    {
                        item.objCount = 2;
                        CurrentHp = Hp;
                        on = true;
                    }
                }
                Time.timeScale = 0;
            });
            CurrentHp = 0;
        }

        StartCoroutine(Fade(RedImage, 1, 0.5f));
        bar.currentPercent = CurrentHp;


    }

    public void Move()
    {
        // float a = Input.GetAxis("Horizontal");
        // float w = Input.GetAxis("Vertical");
        // transform.Translate(Vector3.forward * w * Time.deltaTime * speed, Space.Self);
        // transform.Translate(Vector3.right * a * Time.deltaTime * speed, Space.Self);
    }

    public void Pick()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (!isShopPackup)
            {
                for (int i = 0; i < shopRenderers.Length; i++)
                {
                    shopRenderers[i].materialCount = 100;
                }

                for (int i = 0; i < shopImages.Length; i++)
                {
                    shopImages[i].enabled = true;
                }

                for (int i = 0; i < shopMeshProUguis.Length; i++)
                {
                    shopMeshProUguis[i].enabled = true;
                }

                isShopPackup = true;
                ShopGameObject.transform.DOLocalMove(new Vector3(0, 0, UIdistance), 0.5f);
            }
            else
            {
                for (int i = 0; i < shopRenderers.Length; i++)
                {
                    shopRenderers[i].materialCount = 0;
                }

                for (int i = 0; i < shopImages.Length; i++)
                {
                    shopImages[i].enabled = false;
                }

                for (int i = 0; i < shopMeshProUguis.Length; i++)
                {
                    shopMeshProUguis[i].enabled = false;
                }
                ShopGameObject.transform.DOLocalMove(new Vector3(0, 0, 0), 0.2f);
                isShopPackup = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.B) || controller.IsButtonDown(ControllerButtonCode.App) || GunInput.Instance.IsReleaseKeyDown)
        {
            if (!isPackup)
            {
                for (int i = 0; i < Renderers.Length; i++)
                {
                    Renderers[i].materialCount = 100;
                }

                for (int i = 0; i < Images.Length; i++)
                {
                    Images[i].enabled = true;
                }

                for (int i = 0; i < TextMeshProUguis.Length; i++)
                {
                    TextMeshProUguis[i].enabled = true;
                }

                PackGameObject.transform.DOLocalMove(new Vector3(0, 0, UIdistance), 0.5f).OnComplete(() =>
                {
                    Time.timeScale = 0;
                    isPackup = true;
                });


            }
            else
            {
                Time.timeScale = 1;
                for (int i = 0; i < Renderers.Length; i++)
                {
                    Renderers[i].materialCount = 0;
                }

                for (int i = 0; i < Images.Length; i++)
                {
                    Images[i].enabled = false;
                }

                for (int i = 0; i < TextMeshProUguis.Length; i++)
                {
                    TextMeshProUguis[i].enabled = false;
                }
                PackGameObject.transform.DOLocalMove(new Vector3(0, 0, 0f), 0.2f).OnComplete(() => { isPackup = false; });

            }
        }


        if (Physics.Raycast(tr.position, tr.forward, out hit, 1.5f))
        {
            GameObject game = hit.collider.gameObject;
            ObjectItem obj = game.GetComponent<ObjectItem>();


            if (obj != null)
            {
                isPick = true;
                obj.ispick = true;
                if (Input.GetKeyDown(KeyCode.Z) || controller.IsButtonDown(ControllerButtonCode.Home) || GunInput.Instance.IsTriggerDown || Input.GetMouseButtonDown(0))
                {

                    obj = Pack.getItem(obj);
                    for (int i = 0; i < Pack.items.Count; i++)
                    {
                        Buttons[i].GetComponent<Image>().sprite = Pack.items[i].MySprite;
                        Buttons[i].GetComponent<ButtonManagerBasic>().buttonText =
                            Pack.items[i].Name + "x" + Pack.items[i].objCount.ToString();
                        Buttons[i].GetComponentInChildren<TextMeshProUGUI>().text =
                            Pack.items[i].Name + "x" + Pack.items[i].objCount.ToString();

                        if (Pack.items[i].objCount == 0)
                        {
                            Buttons[i].GetComponent<Image>().sprite = SpriteNone;
                            Buttons[i].GetComponent<ButtonManagerBasic>().buttonText = "";
                            Buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
                            // Pack.items.Remove(Pack.items[i]);
                        }
                    }

                    if (obj.objCount == 0)
                    {
                        Destroy(game);
                    }
                }
            }
        }

        if (GunInput.Instance.IsPushKeyDown || Input.GetKey(KeyCode.Q))
        {
            Exit.SetActive(true);
            Exit.transform.DOLocalMove(new Vector3(0, 0, UIdistance), 0.2f);

        }
    }

    public static IEnumerator Fade(CanvasGroup red, float alpha, float duration)
    {
        float nowalpha = red.alpha;
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            red.alpha = Mathf.Lerp(nowalpha, alpha, time / duration);
            yield return new WaitForEndOfFrame();
        }
    }

    public void ExitCancle()
    {
        Exit.transform.DOLocalMove(new Vector3(0, 1, 0), 0.2f).OnComplete(() => { Exit.SetActive(false); });
    }

    public void ScoreAdd(int score)
    {
        Sum += score;
    }

    public void DieUIButton()
    {
        if (on)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 1;
            Tip.SetActive(true);
            DieUI.SetActive(false);
            Tip.transform.DOLocalMove(new Vector3(0, 0, 1.3f), 0.25f).OnComplete(() =>
            {
                Time.timeScale = 0;
            }); ;
        }
    }

    public void TipUI()
    {
        GlobalParameter.Instance.isGameOver = true;
        SceneManager.LoadScene(1);
    }

    public void ShopButton()
    {
        for (int i = 0; i < shopRenderers.Length; i++)
        {
            shopRenderers[i].materialCount = 100;
        }

        for (int i = 0; i < shopImages.Length; i++)
        {
            shopImages[i].enabled = true;
        }

        for (int i = 0; i < shopMeshProUguis.Length; i++)
        {
            shopMeshProUguis[i].enabled = true;
        }
        ShopGameObject.transform.DOLocalMove(new Vector3(0, 0, UIdistance), 0.5f);
    }


    public void CloseShop()
    {
        for (int i = 0; i < shopRenderers.Length; i++)
        {
            shopRenderers[i].materialCount = 0;
        }

        for (int i = 0; i < shopImages.Length; i++)
        {
            shopImages[i].enabled = false;
        }

        for (int i = 0; i < shopMeshProUguis.Length; i++)
        {
            shopMeshProUguis[i].enabled = false;
        }

        ShopGameObject.transform.DOLocalMove(new Vector3(0, 0, 0), 0.2f);
    }
}

