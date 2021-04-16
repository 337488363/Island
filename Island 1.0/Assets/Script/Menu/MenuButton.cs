using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{

    public void StartButton()
    {
        SceneManager.LoadScene(1);
        GlobalParameter.Instance.isGameOver = false;
    }

    public void ShowsButton()
    {
        SceneManager.LoadScene(3);
    }
}
