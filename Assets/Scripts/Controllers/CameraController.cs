using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraController : MonoBehaviour, IDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        Managers.Camera.FreeLook.m_XAxis.Value += eventData.delta.x / 10f;
        Managers.Camera.FreeLook.m_YAxis.Value -= eventData.delta.y / 500f;
    }

}
