using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterMovement
{
    protected GameObject m_Monster;

    public MonsterMovement(GameObject monster)
    {
        m_Monster = monster;
    }

    public abstract void Move(GameObject target, float speed);
   
}
