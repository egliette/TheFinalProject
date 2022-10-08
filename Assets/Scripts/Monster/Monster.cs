using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private LayerMask m_PlayerMask;
    [SerializeField] private float m_Speed = 5f;
    [SerializeField] private float m_DetectTargetRange = 5f;
    [SerializeField] private MonsterMovement m_MonsterMovement;

    private Animator m_Animator;
    private GameObject m_Target = null;

    private void Start()
    {
        m_Animator = GetComponent<Animator>();

        m_MonsterMovement = new MonsterWalking(this.gameObject);
    }

    private void Update()
    {
        DetectTarget();
    }

    private void FixedUpdate()
    {
        if (m_Target != null)
        {
            Vector2 prevPos = transform.position;

            m_MonsterMovement.Move(m_Target, m_Speed);

            Vector2 curPos = transform.position;
            Vector3 moveDelta = new Vector3(curPos.x - prevPos.x, curPos.y - prevPos.y, 0);
            Utils.FlipAnimation(this.gameObject, moveDelta);

            // start running animation
            m_Animator.SetFloat("Speed", 1f);
        }
        else
        {
            m_Animator.SetFloat("Speed", 0f);
        }
    }

    // Return true if detect player layermask in detect range
    private bool DetectTarget()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, m_DetectTargetRange, m_PlayerMask);
        // if detect no target, return false
        if (hitColliders.Length == 0)
        {
            m_Target = null;
            return false;
        }

        float minDistance = float.MaxValue;

        foreach (Collider2D hitCollider in hitColliders)
        {
            float distanceToHitCollider = Vector2.Distance(transform.position, hitCollider.gameObject.transform.position);
            if (minDistance > distanceToHitCollider)
            {
                minDistance = distanceToHitCollider;
                m_Target = hitCollider.gameObject;
            }
        }

        return true;
    }
}
