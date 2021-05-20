using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public GameObject gameover;
    public Text scoretxt;
    int score = 0;

    void Start()
    {
        StartCoroutine(ScoreCoroutine());
    }

    void Update()
    {
        if (gameover.activeSelf)
        {
            End();
        }
    }
    void End()
    {
        StopCoroutine(ScoreCoroutine());
        scoretxt.text = "SCORE : " + score;
        PlayerPrefs.SetInt("M_R_L",1);
        if (PlayerPrefs.GetInt("Best") < score)
        {
            PlayerPrefs.SetInt("Best",score);
        }
    }

    IEnumerator ScoreCoroutine()
    {
        while (true)
        {
            score++;
            scoretxt.text = "SCORE : " + score;
            yield return new WaitForSeconds(1f);
        }
    }
}
