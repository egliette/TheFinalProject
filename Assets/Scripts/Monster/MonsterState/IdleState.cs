using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IMonsterState
{
    private Monster m_Monster;
    public IdleState(Monster monster)
    {
        m_Monster = monster;
    }
    public IMonsterState DoState()
    {
        m_Monster.OnIdleUI();
        DetectTarget();
        if (m_Monster.GetTarget() != null)
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
        return Enums.MonsterBehavior.IDLE.ToString();
    }

    // Return true if detect player layermask in detect range
    private void DetectTarget()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(m_Monster.transform.position, m_Monster.GetMonsterConfig().detectTargetRange, m_Monster.GetPlayerMask());
        // if detect no target, return false
        if (hitColliders.Length == 0)
        {
            m_Monster.SetTarget(null);
            return;
        }

        float minDistance = float.MaxValue;

        foreach (Collider2D hitCollider in hitColliders)
        {
            float distanceToHitCollider = Vector2.Distance(m_Monster.transform.position, hitCollider.gameObject.transform.position);
            if (minDistance > distanceToHitCollider)
            {
                minDistance = distanceToHitCollider;
                m_Monster.SetTarget(hitCollider.gameObject);
            }
        }


    }


}
