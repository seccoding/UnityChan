using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour, IDragHandler
{
    [SerializeField] GameObject _player;
    [SerializeField] float _rotationSpeed;

    Vector3 delta;

    // Start is called before the first frame update
    void Start()
    {
        delta = new Vector3(0, 6, -15);
        Camera.main.transform.rotation = Quaternion.Euler(15, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.transform.localPosition = _player.transform.position + delta;
        Camera.main.transform.LookAt(_player.transform.position + (Vector3.up * 1.5f));
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 touchDirection = eventData.delta;

        Camera.main.transform.RotateAround(_player.transform.position, Vector3.right, touchDirection.y * 0.2f);
        Camera.main.transform.RotateAround(_player.transform.position, Vector3.up, touchDirection.x * 0.2f);

        Camera.main.transform.eulerAngles = new Vector3(touchDirection.y * 0.3f, 0f, 0f);
        Camera.main.transform.LookAt(_player.transform.position + (Vector3.up * 1.5f));

        delta = new Vector3(Camera.main.transform.localPosition.x - _player.transform.localPosition.x, Camera.main.transform.localPosition.y - _player.transform.localPosition.y, Camera.main.transform.localPosition.z - _player.transform.localPosition.z);
    }

}
