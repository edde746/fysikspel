using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void LevelSelectButton()
    {
        SceneManager.LoadScene("Level Select");
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
