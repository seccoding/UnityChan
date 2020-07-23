using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager
{
    public List<GameObject> GetAllEnemies()
    {
        List<GameObject> enemies = new List<GameObject>();

        GameObject[] objects = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject obj in objects)
        {
            if (obj.layer == 8)
            {
                enemies.Add(obj);
                Debug.Log(obj.name);
            }
        }

        return enemies;
    }
}
