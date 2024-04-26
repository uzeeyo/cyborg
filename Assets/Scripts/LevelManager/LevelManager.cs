using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public static class LevelManager 
{
    public static void OpenBossScene()
    {
        SceneManager.LoadScene("BossLevel");
    }
    public static void OpenStartMenu()
    {
        //SceneManager.LoadScene("BossLevel");
    }
    public static void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
