using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Best : MonoBehaviour
{
    public Text best;
    void Start()
    {
        best.text = "BEST : " + PlayerPrefs.GetInt("Best");
    }
}