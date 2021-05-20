using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class UpgradeRobot : MonoBehaviour
{
    public GameObject shop;
    public Text level,energy;
    public static Action UpgradeEverything;

    public GameObject getgm;
    private GameManager gm;

    private void Start()
    {
        gm=getgm.GetComponent<GameManager>();
    }

    public void setShop()
    {
        shop.SetActive(true);
        level.text = "Level : " + PlayerPrefs.GetInt("M_R_L");
        Time.timeScale = 0;
    }
    public void setfalseShop()
    {
        shop.SetActive(false);
        Time.timeScale = 1;
    }

    public void clickupgrade()
    {
        if (PlayerPrefs.GetInt("M_R_L")*100 < gm.resource)
        {
            PlayerPrefs.SetInt("M_R_L", PlayerPrefs.GetInt("M_R_L") + 1);
            level.text = "Level : " + PlayerPrefs.GetInt("M_R_L");
            gm.resource -= PlayerPrefs.GetInt("M_R_L") * 100;
            energy.text = "Resource : " + gm.resource;
        }
       
    }

}
