using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IMonsterState
{
    private Monster m_Monster;

    public AttackState(Monster monster)
    {
        this.m_Monster = monster;
    }

    public IMonsterState DoState()
    {
        m_Monster.OnAtackUI();
        float distanceToTarget = Vector3.Distance(m_Monster.transform.position, m_Monster.GetTarget().transform.position);
        if (distanceToTarget <= m_Monster.GetMonsterConfig().attackRange)
        {
            return m_Monster.m_MonsterAttackState;
        }
        else if (distanceToTarget <= m_Monster.GetMonsterConfig().detectTargetRange)
        {
            return m_Monster.m_MonsterApproachTargetState;
        }
        else
        {
            return m_Monster.m_MonsterIdleState;
        }
    }


    string IMonsterState.StateName()
    {
        return Enums.MonsterBehavior.ATTACK.ToString();
    }

    private void DoAttack()
    {
        m_Monster.GetMonsterAttack().DoAttack(m_Monster.GetTarget());
    }
}
