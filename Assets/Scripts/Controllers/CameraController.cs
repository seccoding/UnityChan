using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Define.CameraMode _mode = Define.CameraMode.QuaterView;
    [SerializeField]
    Vector3 _delta;
    [SerializeField]
    GameObject _player = null;

    void Start()
    {
        SetQuarterView(Camera.main.transform.position);
    }

    // Update 이후에 실행됨. 모든 처리가 끝난 후 처리해야 할 것들을 정의한다.
    void LateUpdate()
    {
        if (_mode == Define.CameraMode.QuaterView)
        {
            int wallMask = (1 << (int)Define.Layer.Wall);
            RaycastHit hit;
            if (Physics.Raycast(_player.transform.position, _delta, out hit, _delta.magnitude, wallMask))
            {
                float dist = (hit.point - _player.transform.position).magnitude * 0.8f;
                transform.position = _player.transform.position + _delta.normalized * dist;
            }
            else
            {
                SetQuarterView(_delta);
                transform.position = _player.transform.position + _delta;
                transform.LookAt(_player.transform);
            }
            
        }
    }

    public void SetQuarterView(Vector3 delta)
    {
        _mode = Define.CameraMode.QuaterView;
        _delta = delta;
    }
}
