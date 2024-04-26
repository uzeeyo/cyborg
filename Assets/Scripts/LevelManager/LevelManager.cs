using UnityEngine.SceneManagement;
public static class LevelManager
{
    public static void OpenBossScene()
    {
        SceneManager.LoadScene("BossScene2");
    }

    public static void OpenMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public static void OpenStartMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public static void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
