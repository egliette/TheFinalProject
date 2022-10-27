using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    private IMonsterAttack m_AttackMethod;
    

    public void DoAttack(GameObject target)
    {
        m_AttackMethod.DoAttack(target);
    }

    public void ConfigMonsterData(MonsterConfig config)
    {
        m_AttackMethod = GetAttackMethod(config);   
    }

    private IMonsterAttack GetAttackMethod(MonsterConfig config)
    {
        switch (config.attackType)
        {
            case Enums.MonsterAttackType.NORMAL:
                {
                    return new NormalAttack();
                }
            case Enums.MonsterAttackType.RANGE:
                {
                    return new RangeAttack();
                }
            case Enums.MonsterAttackType.EXPLODE:
                {
                    return new ExplodeAttack();
                }
            default:
                {
                    return null;
                }
        }
    }
}
