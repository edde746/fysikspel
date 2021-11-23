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
            Debug.Log("Won");
            SceneManager.LoadScene("Level Select");
        }
    }
}
