using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{

    static Managers _instance;
    readonly InputManager _input = new InputManager();
    readonly ResourceManager _resource = new ResourceManager();

    public static Managers Instance { get { Init(); return _instance; } }
    public static InputManager Input { get { return Instance._input; } }
    public static ResourceManager Resource { get { return Instance._resource; } }


    void Start()
    {
        Init();
        _input._joystick = FindObjectOfType<Joystick>();
    }

    void Update()
    {
        _input.OnUpdate();
    }

    private static void Init()
    {
        if (_instance == null)
        {
            GameObject manager = GameObject.Find("@Managers");
            if (manager == null)
            {
                manager = new GameObject("@Managers");
                manager.AddComponent<Managers>();
            }

            DontDestroyOnLoad(manager);
            _instance = manager.GetComponent<Managers>();
        }
    }
}
