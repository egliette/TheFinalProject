using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    [SerializeField] private Monster m_Monster;
    private float m_StartHealth;
    private float m_CurrentHealth;
    private bool m_Dead;

    private void OnEnable()
    {
        m_Dead = false;
    }


    private void OnDeath()
    {
        m_Dead = true;
        m_Monster.OnDeadUI();
    }


    public void ConfigMonsterData(MonsterConfig config)
    {
        SetStartHealth(config.health);
    }

    private void SetStartHealth(float amount)
    {
        m_StartHealth = amount;
        m_CurrentHealth = m_StartHealth;
    }


    public void TakeDamage(float amount)
    {
        Debug.Log("TAKEDAMAGE");
        m_CurrentHealth -= amount;
        if (m_CurrentHealth <= 0 && !m_Dead)
        {
            OnDeath();
        }
    }

}
