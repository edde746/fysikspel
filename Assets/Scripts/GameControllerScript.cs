using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour
{
    List<GameObject> enemies;
    void Start()
    {
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    void Update()
    {
        if (enemies.TrueForAll(enemy => enemy == null))
        {
            // No enemies exist, go back to level select screen
            Utility.Savefile.MarkLevelCompleted(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("Level Select");
        }
    }
}
