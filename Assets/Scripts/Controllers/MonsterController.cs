using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : CharacterBase
{
    // Start is called before the first frame update
    void Start()
    {
        SetEnemyType(EnemyType.Orc);
        Behavior = new CharaceterMoveAndRotation(this);
        _maxHp = float.MaxValue;
        _hp = _maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        Vector3 startRayPosition = transform.position;
        Debug.DrawRay(startRayPosition, transform.forward * 5f, Color.red, 1f);
        RaycastHit hit;
        if ( Physics.Raycast(startRayPosition, transform.forward, out hit, 5f, 1 << 11) )
        {
            Target.Target = hit.transform.gameObject;
            Behavior.Move(Vector3.forward, 0.5f);

            if (_skillController != null)
            {
                CharacterBase targetBase = Target.Target.GetComponent<CharacterBase>();
                _skillController.InteractionSkill(_skill, targetBase);
                //_skillController.ContinuosDamage(_skill, targetBase);
                DieProcess();
            }
            
        }
    }

    
}
