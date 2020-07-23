using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType
{
    None,
    Knight,
    Mage,
    Archor,
}

public enum EnemyType
{
    None,
    Slime,
    Orc,
    Skeleton,
}

public class Stat : MonoBehaviour
{
    public CharacterType _characterType;
    public EnemyType _enemyType;

    public byte _level;

    public float _maxHp; // 최대 체력
    public float _maxMana; // 최대 마나

    public float _hp; // 체력
    public float _mana; // 마나

    public float _con; // 체력 - 체력 증가
    public float _strength; // 힘 - 근거리 물리 데미지 증가, 아이템 소지가능 무게 증가
    public float _dex; // 민첩 - 근접 물리공격 회피율 증가, 원거리 데미지 증가
    public float _wis; // 지혜 - 마나 양/회복율 증가, 마법 방어율 증가
    public float _know; // 지식 (int) - 마법 데미지 증가, 마법 명중률 증가

    public float _ciritical; // 치명타율
    public float _avoidance; // 회피율

    public void SetEnemyType(EnemyType enemyType)
    {
        _enemyType = enemyType;
        _characterType = CharacterType.None;

        if (_enemyType == EnemyType.Slime)
        {
            _level = 1;
            _con = 2f;
            _strength = 2f;
            _dex = 1f;
            _wis = 1f;
            _know = 1f;
            _ciritical = 10f;
            _avoidance = 5f;
        }
        else if (_enemyType == EnemyType.Orc)
        {
            _level = 3;
            _con = 5f;
            _strength = 3f;
            _dex = 2f;
            _wis = 1f;
            _know = 2f;
            _ciritical = 10f;
            _avoidance = 5f;
        }
        else if (_enemyType == EnemyType.Skeleton)
        {
            _level = 5;
            _con = 10f;
            _strength = 3f;
            _dex = 3f;
            _wis = 1f;
            _know = 3f;
            _ciritical = 10f;
            _avoidance = 5f;
        }

        _maxHp = _hp = _con * 10f;
        _maxMana = _mana = _wis * 5f;
    }

    public void SetCharacterType(CharacterType characterType)
    {
        _characterType = characterType;
        _enemyType = EnemyType.None;
        _level = 1;

        if (_characterType == CharacterType.Knight)
        {
            _con = 50f;
            _strength = 10f;
            _dex = 1f;
            _wis = 6f;
            _know = 1f;
            _ciritical = 20f;
            _avoidance = 15f;
        }
        else if (_characterType == CharacterType.Mage)
        {
            _con = 30f;
            _strength = 3f;
            _dex = 2f;
            _wis = 60f;
            _know = 10f;
            _ciritical = 25f;
            _avoidance = 20f;
        }
        else if (_characterType == CharacterType.Archor)
        {
            _con = 40f;
            _strength = 4f;
            _dex = 10f;
            _wis = 20f;
            _know = 2f;
            _ciritical = 30f;
            _avoidance = 25f;
        }

        _maxHp = _hp = _con * 10f;
        _maxMana = _mana =  _wis * 5f;
    }

    public void LevelUp()
    {
        _level += 1;

        if (_characterType == CharacterType.Knight)
        {
            _con += 3f;
            _strength += 3f;
            _dex += 1f;
            _wis += 1f;
            _know += 1f;
        }
        else if (_characterType == CharacterType.Mage)
        {
            _con += 1f;
            _strength += 1f;
            _dex += 1f;
            _wis += 3f;
            _know += 3f;
        }
        else if (_characterType == CharacterType.Archor)
        {
            _con += 2f;
            _strength = 2f;
            _dex = 3f;
            _wis = 1.5f;
            _know = 1.5f;
        }

        _maxHp = _hp += (_con * 10f);
        _maxMana = _mana += (_wis * 5f);
    }
}


