using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string SceneToLoad;

    public void LoadGame()
    {
        SceneManager.LoadScene(SceneToLoad);
        Time.timeScale = 1.0f;
    }

}
