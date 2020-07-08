using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10;

    Vector3 _destPos;
    bool _moveToDest = false;

    void Start()
    {
        // 혹시라도 다른 부분에서 KeyAction에 동일한 이벤트를 넣었을 경우, 두번 실행되는 것을 방지하기 위해서 하나를 끊는다.
        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard;

        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;
    }

    void Update()
    {
        if (_moveToDest)
        {
            // 방향벡터 
            Vector3 dir = _destPos - transform.position;

            // 캐릭터가 도착했을 때
            // dir.magnitude : 크기가 있는 방향벡터
            if (dir.magnitude < 0.0001f)
            {
                _moveToDest = false;
            }
            else
            {
                float moveDistance = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
                // dir.normalized : 크기가 1인 방향벡터
                transform.position += (dir.normalized * moveDistance);
                // 목적지를 바라본다.
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 *ㅇ Time.deltaTime);
            }
        }
    }

    void OnKeyboard()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.5f);
            //transform.Translate(Vector3.forward * Time.deltaTime * _speed); // 회전을 하면서 앞으로 나아가기 때문에 커브로 회전한다.
            transform.position += Vector3.forward * Time.deltaTime * _speed; // 회전과 동시에 월드좌표를 기준으로 움직이면 커브로 회전하지 않는다.
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.5f);
            //transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            transform.position += Vector3.back * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.5f);
            //transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            transform.position += Vector3.left * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.5f);
            //transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            transform.position += Vector3.right * Time.deltaTime * _speed;
        }

        _moveToDest = false;
    }

    void OnMouseClicked(Define.MouseEvent evnt)
    {
        if (evnt != Define.MouseEvent.Click)
            return;

        Ray cameraRay = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        Debug.DrawRay(Camera.main.transform.position, cameraRay.direction * 100.0f, Color.red, 1.0f);
        int layerMask = (1 << (int)Define.Layer.Wall);

        RaycastHit hitInfo;
        if (Physics.Raycast(cameraRay, out hitInfo, 100.0f, layerMask))
        {
            // 클릭한 좌표
            _destPos = hitInfo.point;
            _moveToDest = true;
        }
    }
}
