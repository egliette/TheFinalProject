using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproachTargetState : IMonsterState
{
    private Monster m_Monster;
    public ApproachTargetState(Monster monster)
    {
        m_Monster = monster;
    }

    IMonsterState IMonsterState.DoState()
    {
        MoveMonster();
        m_Monster.OnRunningUI();

        // get distance to current target
        float distanceToTarget = Vector3.Distance(m_Monster.transform.position, m_Monster.GetTarget().transform.position);
        if (distanceToTarget <= m_Monster.GetMonsterConfig().attackRange)
        {
            return m_Monster.m_MonsterAttackState;
        }
        else if(distanceToTarget <= m_Monster.GetMonsterConfig().detectTargetRange)
        {
            return m_Monster.m_MonsterApproachTargetState;
        }
        return m_Monster.m_MonsterIdleState;
        

    }

    string IMonsterState.StateName()
    {
        return Enums.MonsterBehavior.APPROACH_FOR_TARGET.ToString();
    }

    private void MoveMonster()
    {
        m_Monster.GetMonsterMovement().Move(m_Monster.GetTarget().transform);
    }

}
