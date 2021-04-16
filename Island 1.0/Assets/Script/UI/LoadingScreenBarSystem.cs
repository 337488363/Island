using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using PoliceTrainingEditor.InputSystem;
using UnityEngine.SceneManagement;
using Ximmerse.RhinoX;

public class LoadingScreenBarSystem : MonoBehaviour {

    public GameObject bar;
    public GameObject CycleBarBackGround;
    public GameObject[] backgroundImages;
    public RXController RxController;
    public Text loadingText;
    public bool backGroundImageAndLoop;
    public float LoopTime;
    
    float time = 0;
    [Range(0,1f)]public float vignetteEfectVolue; 
    AsyncOperation async;
    Image vignetteEfect;


    public void loadingScreen (int sceneNo)
    {
        this.gameObject.SetActive(true);
        StartCoroutine(Loading(sceneNo));
    }

    private void Start()
    {
        vignetteEfect = transform.Find("VignetteEfect").GetComponent<Image>();
        vignetteEfect.color = new Color(vignetteEfect.color.r,vignetteEfect.color.g,vignetteEfect.color.b,vignetteEfectVolue);

        if (backGroundImageAndLoop)
            StartCoroutine(transitionImage());
        if (GlobalParameter.Instance.isGameOver)
        {
            loadingScreen(0);
        }
        else
        {
            loadingScreen(2);
        }
        
    }

    void Update()
    {
        time += Time.deltaTime*2f;
    }

    // The pictures change according to the time of
    IEnumerator transitionImage ()
    {
        for (int i = 0; i < backgroundImages.Length; i++)
        {
            yield return new WaitForSeconds(LoopTime);
            for (int j = 0; j < backgroundImages.Length; j++)
            {
                StartCoroutine(PlayerHp.Fade(backgroundImages[j].GetComponent<CanvasGroup>(), 0, 1f));
            }
            StartCoroutine(PlayerHp.Fade(backgroundImages[i].GetComponent<CanvasGroup>(), 1, 1f));
            if (i==backgroundImages.Length-1)
            {
                i = -1;
            }
        }
    }

    // Activate the scene 
    IEnumerator Loading (int sceneNo)
    {
        async = SceneManager.LoadSceneAsync(sceneNo);
        async.allowSceneActivation = false;
        
        while (async.isDone == false)
        {
            bar.transform.localScale = new Vector3(time*0.1f, 0.9f,1);
            if (bar.transform.localScale.x>1)
            {
                bar.transform.localScale = new Vector3(1, 0.9f, 1);
            }

            if (loadingText != null)
                loadingText.text = "";

            if (bar.transform.localScale.x >= 1)
            {
                loadingText.text = "按任意键开始游戏";
                loadingText.transform.localScale=new Vector3(1.2f,1.2f,1.2f);
                loadingText.GetComponent<CanvasGroup>().alpha = Mathf.PingPong(Time.time*0.8f, 1);
                CycleBarBackGround.SetActive(false);
                if (Input.GetMouseButtonDown(0)||GunInput.Instance.IsTriggerDown|| RxController.IsButtonDown(ControllerButtonCode.Trigger))
                {
                    async.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }

}
