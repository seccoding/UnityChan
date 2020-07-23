using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{

    static Managers _instance;
    readonly CameraManager _camera = new CameraManager();
    readonly EnemyManager _enemy = new EnemyManager();
    readonly InputManager _input = new InputManager();
    readonly ResourceManager _resource = new ResourceManager();
    readonly SceneManager _scene = new SceneManager();
    readonly UIManager _ui = new UIManager();

    public static Managers Instance { get { Init(); return _instance; } }
    public static CameraManager Camera { get { return Instance._camera; } }
    public static InputManager Input { get { return Instance._input; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManager Scene { get { return Instance._scene; } }
    public static EnemyManager Enemy { get { return Instance._enemy; } }
    public static UIManager UI { get { return Instance._ui; } }


    void Start()
    {
        Init();
        _input.Init();
    }

    void Update()
    {
        _input.OnUpdate();
    }

    private void OnGUI()
    {
        _ui.OnGUI();
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

    public void GameOver()
    {
        Debug.Log("GameOver");
    }
}
