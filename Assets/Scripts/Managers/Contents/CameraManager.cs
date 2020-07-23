using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class CameraManager
{
    private CinemachineFreeLook _freeLookCamera;

    public CinemachineFreeLook FreeLook { get { Init();  return _freeLookCamera; } }

    public Camera Main { get { return Camera.main; } }

    public void Init()
    {
        if (_freeLookCamera == null)
            _freeLookCamera = GameObject.FindObjectOfType<CinemachineFreeLook>();
    }

}
