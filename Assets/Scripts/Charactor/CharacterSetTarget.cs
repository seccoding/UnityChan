using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSetTarget
{
    private GameObject _object;

    public GameObject Target { get { return GetTarget(); } set { SetTarget(value); } }

    public bool HaveTarget()
    {
        return _object != null;
    }

    public void ReleaseTarget()
    {
        _object = null;
    }

    public void Destroy()
    {
        GameObject.DestroyImmediate(_object);
        ReleaseTarget();
    }

    private void SetTarget(GameObject target)
    {
        _object = target;
    }

    private GameObject GetTarget()
    {
        return _object;
    }
}
