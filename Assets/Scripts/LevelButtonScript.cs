using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButtonScript : MonoBehaviour
{
    public int level = 1;
    void Start()
    {
        var textComponent = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        textComponent.SetText($"{level}");
    }
    public void ChangeLevel()
    {
        SceneManager.LoadScene($"Level {level}");
    }
}
