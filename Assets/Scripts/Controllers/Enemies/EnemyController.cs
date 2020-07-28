using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private IEnumerator coroutine;
    private GameObject _player;

    private EnemyStatController _stat;
    private EnemyStatusController _status;

    void Start()
    {
        _stat = GetComponent<EnemyStatController>();
        _status = GetComponent<EnemyStatusController>();
    }

    void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        Vector3 startRayPosition = transform.position;
        Debug.DrawRay(startRayPosition, transform.forward * 5f, Color.red, 1f);
        RaycastHit hit;
        if (Physics.Raycast(startRayPosition, transform.forward, out hit, 5f, 1 << 11))
        {
            _player = hit.transform.gameObject;
            Vector3 rotationDest = GetDistanceVectorToTarget();
            transform.rotation = Quaternion.LookRotation(rotationDest);
            transform.Translate(Vector3.forward * Time.deltaTime * _stat._speed);
        }
    }

    public void SetAndLookTarget(GameObject player)
    {
        if (!HaveTarget())
            _player = player;

        if (HaveTarget())
            LookTargetSlerp();
    }

    public bool HaveTarget()
    {
        return _player != null;
    }

    public void LookTargetSlerp()
    {
        Vector3 vec = GetDistanceVectorToTarget();
        coroutine = LookCoroutine(transform, vec);
        StartCoroutine(coroutine);
    }

    public Vector3 GetDistanceVectorToTarget()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        Vector3 targetPosition = this._player.transform.position;
        Vector3 vec = targetPosition - transform.position;
        vec.Normalize();
        vec.y = 0;

        return vec;
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
}
