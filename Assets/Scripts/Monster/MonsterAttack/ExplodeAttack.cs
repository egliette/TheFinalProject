using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeAttack : IMonsterAttack
{
    private MonsterAttack m_MonsterAttack;

    public ExplodeAttack(MonsterAttack monsterAttack)
    {
        this.m_MonsterAttack = monsterAttack;
    }

    public void DoAttack(GameObject target)
    {
        if (TargetInRange(target))
        {
            PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
            if (playerHealth)
            {
                playerHealth.TakeDamage((int)m_MonsterAttack.GetDamage());
            }

            // add explosion knockback force
            Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
            if (rb)
            {
                // distance vector
                Vector3 v = (target.transform.position - m_MonsterAttack.transform.position).normalized;
                Vector2 forceVector = new Vector2(v.x, v.y);
                rb.AddForce( forceVector * Time.deltaTime, ForceMode2D.Impulse);
            }
        }
    }

    private bool TargetInRange(GameObject target)
    {
        Vector3 curPosition = m_MonsterAttack.transform.position;

        float distance = Vector3.Distance(curPosition, target.transform.position);
        float attackRange = m_MonsterAttack.GetAttackRange();

        return distance <= attackRange;

    }
}
