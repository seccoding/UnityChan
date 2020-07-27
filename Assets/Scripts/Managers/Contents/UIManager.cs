using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager
{

    public Action<GameObject> ChooseTargetAction;

    readonly Dictionary<GameObject, Rect> _eneniesDict = new Dictionary<GameObject, Rect>();
    List<GameObject> _enemies;

    public void CreateEnemiesIcon()
    {
        _eneniesDict.Clear();
        _enemies = Managers.Enemy.GetAllEnemies();

        for (int i = 0; i < _enemies.Count; i++)
        {
            float width = 140f;
            float height = 140f;

            float x = i * width;
            float y = 20f;

            _eneniesDict.Add(_enemies[i], new Rect(x + 20f, y, width, height));
        }
    }

    public void OnGUI()
    {
        ShowEnemiesIcon();
    }

    private void ShowEnemiesIcon()
    {
        if (_enemies != null)
        {
            foreach (GameObject enemy in _enemies)
            {
                if (enemy == null) continue;

                Rect rect;
                if (_eneniesDict.TryGetValue(enemy, out rect))
                    if (GUI.Button(rect, enemy.name))
                        ChooseTargetAction.Invoke(enemy);
            }
        }
    }

}
