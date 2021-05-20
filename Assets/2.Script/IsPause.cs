using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IsPause : MonoBehaviour
{
    public GameObject pausepage, option,pausebtn;

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                if (!pausepage.activeSelf && !option.activeSelf)
                    CheckIsPause();
                else if (pausepage.activeSelf)
                    CheckIsPlay();
                else if (option.activeSelf)
                    ExitOption();
            }
               
        }
    }

    public void CheckIsPause()
    {
        Time.timeScale = 0;
        pausepage.SetActive(true);
        pausebtn = GameObject.Find("pausebtn");
        pausebtn.SetActive(false);
    }

    public void CheckIsPlay()
    {
        Time.timeScale = 1;
        pausepage.SetActive(false);
        pausebtn.SetActive(true);
    }

    public void CheckIsOption()
    {
        option.SetActive(true);
        pausepage.SetActive(false);
    }

    public void ExitOption()
    {
        option.SetActive(false);
        pausepage.SetActive(true);
    }

    public void CheckIsExit()
    {
        SceneManager.LoadScene("menu");
    }

}