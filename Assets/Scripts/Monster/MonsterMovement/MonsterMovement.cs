using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public void Move(Transform target, float speed)
    {
        if (target != null)
        {
            //float step = speed * Time.deltaTime;

            Vector3 distanceVector = target.position - transform.position;
            distanceVector.Normalize();

            //self.position = Vector2.MoveTowards(self.position, target.position, step);
            transform.Translate(distanceVector * speed * Time.deltaTime);
        }
    }

    public void ConfigMonsterData(MonsterConfig config)
    {
        // Later for further develop
    }
}
