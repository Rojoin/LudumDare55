using UnityEngine.SceneManagement;

public class Loader
{
    public enum Scenes
    {
        Menu,
        Game
    }

    public static void LoadScene(Scenes sceneToLoad)
    {
        SceneManager.LoadScene((int)sceneToLoad);
    }
}
