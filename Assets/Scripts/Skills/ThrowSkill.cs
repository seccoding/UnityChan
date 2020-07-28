using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class ThrowSkill : MonoBehaviour
{

    private float _hitCount;

    public Define.SkillCaster _caster;
    public Vector3 _startPos;

    public Skill _skill;

    public Transform _player;
    public GameObject _target;

    private bool _isFire;

    private void OnTriggerStay(Collider other)
    {
        if (_skill.IsTracking)
            OnHit(other.gameObject, "Trigger");
    }

    private void OnTriggerEnter(Collider other)
    {
        OnHit(other.gameObject, "Trigger");
    }

    private void OnCollisionStay(Collision collision)
    {
        if (_skill.IsTracking)
            OnHit(collision.gameObject, "Collision");
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnHit(collision.gameObject, "Collision");
    }

    private void OnHit(GameObject hitObject, string type)
    {
        if (_player.gameObject == hitObject) return;
        if (hitObject.name == "Plane") return;

        _hitCount++;

        if (_caster == Define.SkillCaster.Player)
        {
            EnemyController targetController = hitObject.GetComponent<EnemyController>();
            targetController.SetAndLookTarget(_player.gameObject);
            StartEffect(hitObject);
        }

        if (_skill.IsMultipleTarget && _hitCount >= _skill.MaxTargetCount)
            Destroy(_skill.Prefab);
        else if (!_skill.IsMultipleTarget)
            Destroy(_skill.Prefab);
    }

    private void StartEffect(GameObject target)
    {
        Managers.Skill.Push(_skill.Effect(_player, target, _caster));
    }

    public void Fire()
    {
        if (_player != null && _target != null)
        {
            _startPos = _player.position;
            _startPos.y += _player.GetComponent<Collider>().bounds.size.y / 2.0f;
            transform.position = _startPos;
            transform.LookAt(_target.transform);

            transform.GetComponent<Collider>().isTrigger = _skill.IsPenetrating;
        }

        _isFire = _player != null && _skill != null && _target != null;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isFire)
        {
            if (_skill.IsTracking)
                transform.LookAt(_target.transform);

            transform.Translate(Vector3.forward * Time.deltaTime * _skill.Speed);

            if (_hitCount < _skill.MaxTargetCount)
            {
                Vector3 skillPosition = transform.position;
                Vector3 skillDistance = skillPosition - _startPos;
                skillDistance.y = 0;

                if (skillDistance.magnitude > _skill.MinDistance )
                    Destroy(_skill.Prefab);
            }
        }
    }

}
