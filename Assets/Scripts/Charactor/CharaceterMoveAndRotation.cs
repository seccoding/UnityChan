using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaceterMoveAndRotation
{
    CharacterBase _characterBase;
    IEnumerator coroutine;

    public CharaceterMoveAndRotation(CharacterBase characterBase)
    {
        _characterBase = characterBase;
    }
    public void MoveToTargetAndAttack()
    {
        float moveDistance = 1f;
        _characterBase._state = Define.PlayerState.Moving;

        float distance = _characterBase.GetTargetDistance();
        if (_characterBase._skill.MinDistance >= distance)
        {
            CharacterBase targetBase = _characterBase.Target.Target.GetComponent<CharacterBase>();
            _characterBase._skillController.InteractionSkill(_characterBase._skill, targetBase);
            //_characterBase._skillController.ContinuosDamage(_characterBase._skill, targetBase);
            _characterBase.DieProcess();

            _characterBase._isAttack = false;
            _characterBase._positionToTarget = Vector3.zero;
            _characterBase._skill = null;
        }
        else
            Move(Vector3.forward, moveDistance);

        if (!_characterBase.Target.HaveTarget())
        {
            _characterBase._state = Define.PlayerState.Idle;
            _characterBase._isAttack = false;
            _characterBase._positionToTarget = Vector3.zero;
            _characterBase._skill = null;
        }
    }

    public void Move(Vector3 angle, float distance)
    {
        if (coroutine != null)
            _characterBase.StopCoroutine(coroutine);

        Debug.Log(_characterBase._state);

        if (_characterBase._state == Define.PlayerState.Sturn
            || _characterBase._state == Define.PlayerState.Knockback)
            return;

        Transform player = _characterBase.gameObject.transform;
        if (!_characterBase.Target.HaveTarget())
        {
            player.rotation = Quaternion.Slerp(player.rotation, Quaternion.LookRotation(angle), 0.2f);
            player.Translate(Vector3.forward * (_characterBase._speed * distance) * Time.deltaTime);
        }
        else
        {
            AroundRotationTarget(_characterBase.Target.Target.transform);
            player.Translate(angle * (_characterBase._speed * distance) * Time.deltaTime);
        }
    }

    private Vector3 GetDistanceVector(Transform target)
    {
        if (coroutine != null)
            _characterBase.StopCoroutine(coroutine);

        _characterBase.Target.Target = target.gameObject;

        Vector3 targetPosition = _characterBase.Target.Target.transform.position;
        Vector3 vec = targetPosition - _characterBase.transform.position;
        vec.Normalize();
        vec.y = 0;

        return vec;
    }

    public void AroundRotationTarget(Transform target)
    {
        Vector3 vec = GetDistanceVector(target);
        _characterBase.gameObject.transform.rotation = Quaternion.LookRotation(vec);
    }

    /*
     * 타겟을 바라봄
     */
    public void LookTarget(Transform target)
    {
        Vector3 vec = GetDistanceVector(target);
        coroutine = LookCoroutine(_characterBase.transform, vec);
        _characterBase.StartCoroutine(coroutine);
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
