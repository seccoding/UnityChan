using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        if (prefab == null)
        {
            Debug.Log($"Failed to load prefab: {path}");
            return null;
        }
        
        return GameObject.Instantiate(prefab, parent);
    }

    public void Destroy(GameObject resource, float time = 0.0f)
    {
        if (resource == null)
            return;

        GameObject.Destroy(resource, time);
    }

}
