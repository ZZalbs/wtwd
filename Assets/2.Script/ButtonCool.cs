using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCool : MonoBehaviour
{
    public GameObject getManager;
    GameManager ManagerLogic;

    Image k = null;


    void Awake()
    {
        k = gameObject.GetComponent<Image>();
        ManagerLogic = getManager.GetComponent<GameManager>();
    }

    public void ClickSpawn()
    {
        ManagerLogic.spawnClick();
        StartCoroutine("skillCheck");
    }

    IEnumerator skillCheck() // 스킬쿨타임 돌리기
    {
        k.fillAmount = 0.0f;
        ManagerLogic.skillOn = false;
        for (int i = 0; i < 20; i++)
        {
            k.fillAmount += 0.05f;
            yield return new WaitForSeconds(0.1f);
        }
        ManagerLogic.skillOn = true;
    }
}
