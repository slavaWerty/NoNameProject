using Constants;
using UnityEngine.SceneManagement;

public static class SceneController
{
    private static void OpenScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void OpenGame()
    {
        OpenScene(SceneConstants.Game);
    }

    public static void OpenMenu()
    {
        OpenScene(SceneConstants.Menu);
    }
}

