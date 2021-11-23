using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPanelScript : MonoBehaviour
{
    public GameObject levelButton;
    void Start()
    {
        int sceneIndex = 1;
        while (true)
        {
            if (DoesSceneExist($"Level {sceneIndex}"))
                Instantiate(levelButton, transform).GetComponent<LevelButtonScript>().level = sceneIndex;
            else
                break;

            sceneIndex++;
        }
    }

    // Credits: https://gist.github.com/yagero/2cd50a12fcc928a6446539119741a343
    public static bool DoesSceneExist(string name)
    {
        if (string.IsNullOrEmpty(name))
            return false;

        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            var scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            var lastSlash = scenePath.LastIndexOf("/");
            var sceneName = scenePath.Substring(lastSlash + 1, scenePath.LastIndexOf(".") - lastSlash - 1);

            if (string.Compare(name, sceneName, true) == 0)
                return true;
        }

        return false;
    }
}
