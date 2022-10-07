using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWalking : MonsterMovement
{

    public MonsterWalking(GameObject monster) : base(monster)
    {

    }

    public override void Move(GameObject target, float speed)
    {
        if (target != null)
        {
            float step = speed * Time.deltaTime;

            m_Monster.transform.position = Vector2.MoveTowards(m_Monster.transform.position, target.transform.position, step);

        }
    }

}
