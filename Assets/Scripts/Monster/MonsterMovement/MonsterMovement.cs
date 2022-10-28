using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    [SerializeField] private BoxCollider2D m_BoxCollider;
    private float m_Speed;
    private RaycastHit2D m_Hit;
    private float m_RandomDistanceToTarget = 2f;


    public void Move(Transform target)
    {
        if (target != null)
        {
            Vector3 randomDeltaDistance = new Vector3(UnityEngine.Random.Range(-m_RandomDistanceToTarget, m_RandomDistanceToTarget), UnityEngine.Random.Range(-m_RandomDistanceToTarget, m_RandomDistanceToTarget), 0);
            Vector3 dir = target.position + randomDeltaDistance - transform.position;

            Vector3 moveDelta = new Vector3(dir.x, dir.y, 0);

            moveDelta.Normalize();

            float step = Time.deltaTime * m_Speed;
            float deltaY = moveDelta.y * step;
            float deltaX = moveDelta.x * step;

            m_Hit = Physics2D.BoxCast(m_BoxCollider.bounds.center, m_BoxCollider.size, 0,
                                    new Vector2(0, moveDelta.y),
                                    Mathf.Abs(deltaY),
                                    LayerMask.GetMask("Blocking"));
            if (m_Hit.collider == null)
            {
                transform.Translate(0, deltaY, 0);
            }

            m_Hit = Physics2D.BoxCast(m_BoxCollider.bounds.center, m_BoxCollider.size, 0,
                                    new Vector2(moveDelta.x, 0),
                                    Mathf.Abs(deltaX),
                                    LayerMask.GetMask("Blocking"));
            if (m_Hit.collider == null)
            {
                transform.Translate(deltaX, 0, 0);
            }
        }
    }

    public void ConfigMonsterData(MonsterConfig config)
    {
        m_Speed = config.speed;
        // Later for further develop
    }
}
