using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButtons : MonoBehaviour
{
    public void SkipLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene + 1 > SceneManager.sceneCount)
            Exit();
        else
            SceneManager.LoadScene(currentScene + 1);
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}
