using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBase : MonoBehaviour
{
    [SerializeField] private LayerMask m_HeroMask;
    [SerializeField] private float m_Speed = 5f;
    [SerializeField] private float m_DetectTargetRange = 5f;

    private Animator m_Animator;
    private GameObject m_Target = null;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectTarget();
    }


    private void FixedUpdate()
    {
        if(m_Target != null)
        {
            float step = m_Speed * Time.deltaTime;

            Vector2 prevPos = transform.position;
            transform.position = Vector2.MoveTowards(transform.position, m_Target.transform.position, step);
            Vector2 curPos = transform.position;

            Vector3 moveDelta = new Vector3(curPos.x - prevPos.x, curPos.y - prevPos.y, 0);

            Utils.FlipAnimation(this.gameObject, moveDelta);
        }
    }

    // Return true if detect player layermask in detect range
    private bool DetectTarget()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, m_DetectTargetRange, m_HeroMask);

        float minDistance = float.MaxValue;

        foreach(Collider2D hitCollider in hitColliders)
        {
            float distanceToHitCollider = Vector2.Distance(transform.position, hitCollider.gameObject.transform.position);
            if (minDistance > distanceToHitCollider)
            {
                minDistance = distanceToHitCollider;
                m_Target = hitCollider.gameObject;
            }
        }

        // if detect no target, return false
        if (hitColliders.Length == 0)
        {
            m_Target = null;
            return false;
        }
        return true;
    }
}
