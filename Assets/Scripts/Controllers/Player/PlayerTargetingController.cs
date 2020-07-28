using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingController : MonoBehaviour
{
    private TargetPointerPopup _targetPopup;
    public GameObject Target { get; set; }
    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        Managers.UI.ChooseTargetAction -= ChooseTarget;
        Managers.UI.ChooseTargetAction += ChooseTarget;

        Managers.UI.CreateEnemiesIcon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool HaveTarget()
    {
        return Target != null;
    }

    public void ReleaseTarget()
    {
        Target = null;
    }

    public void Destroy()
    {
        DestroyImmediate(Target);
        ReleaseTarget();
    }

    public void ChooseTarget(GameObject obj)
    {
        if (_targetPopup != null)
        {
            TargetPointerPopup.Destroy(_targetPopup);
            _targetPopup = null;
        }

        if (Target == obj)
        {
            Managers.Camera.FreeLook.m_BindingMode = CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp;
            ReleaseTarget();
        }
        else
        {
            Target = obj;
            LookTargetSlerp();
            Managers.Camera.FreeLook.m_BindingMode = CinemachineTransposer.BindingMode.LockToTarget;

            _targetPopup = TargetPointerPopup.Create(Target.transform);
        }
    }

    public Vector3 GetDistanceVectorToTarget()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        Vector3 targetPosition = Target.transform.position;
        Vector3 vec = targetPosition - transform.position;
        vec.Normalize();
        vec.y = 0;

        return vec;
    }

    public void LookTargetSlerp()
    {
        Vector3 vec = GetDistanceVectorToTarget();
        coroutine = LookCoroutine(transform, vec);
        StartCoroutine(coroutine);
    }

    private IEnumerator LookCoroutine(Transform transform, Vector3 destPosition)
    {
        while (true)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(destPosition), 0.2f);
            if (transform.rotation.eulerAngles == Quaternion.LookRotation(destPosition).eulerAngles)
                break;
            yield return new WaitForSeconds(0.0001f);
        }
    }

    public void SetClosedTarget(float skillMinDistance)
    {
        Debug.DrawRay(transform.position + transform.GetComponent<CharacterController>().center, transform.forward * skillMinDistance, Color.red, 1f);
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position + transform.GetComponent<CharacterController>().center, transform.forward, out hitInfo, skillMinDistance, 1 << 8))
        {
            Target = hitInfo.transform.gameObject;
            _targetPopup = TargetPointerPopup.Create(Target.transform);
        }
    }

    /*
     * 타겟이 스킬의 최소 사용거리에 있는지 확인
     */
    public bool IsClosedDistanceToTarget(float skillMinDistance)
    {
        float targetDistance = GetTargetDistance();
        return targetDistance <= skillMinDistance;
    }

    public float GetTargetDistance()
    {
        Vector3 distance = transform.position - Target.transform.position;
        return Mathf.Abs(distance.magnitude);
    }
}
