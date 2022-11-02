using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeProp : IPropAction
{
    private Prop m_Prop;
    private float m_ExplodeRange = 3f;
    private float m_Damage = 20f;

    public ExplodeProp(Prop prop)
    {
        m_Prop = prop;
    }
    public void DoAction()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(m_Prop.transform.position, m_ExplodeRange, m_Prop.GetDeadlDamageLayerMask());
        foreach (Collider2D hit in hitColliders)
        {
            PlayerHealth playerHealth = hit.GetComponent<PlayerHealth>();
            if (playerHealth)
            {
                playerHealth.TakeDamage((int)m_Damage);
            }

            MonsterHealth monsterHealth = hit.GetComponent<MonsterHealth>();
            if (monsterHealth)
            {
                monsterHealth.TakeDamage((int)m_Damage);
            }

            KnockBack(hit.gameObject);
        }


    }

    private void KnockBack(GameObject target)
    {
        // add explosion knockback force
        Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
        if (rb)
        {
            // distance vector
            Vector3 v = (target.transform.position - m_Prop.transform.position).normalized;
            Vector2 forceVector = new Vector2(v.x, v.y);
            rb.AddForce(forceVector * Time.deltaTime, ForceMode2D.Impulse);
        }
    }
}
