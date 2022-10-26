using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    private float m_StartHealth;
    private float m_CurrentHealth;
    private bool m_Dead;

    private void OnEnable()
    {
        m_Dead = false;
    }

    private void OnDeath()
    {
        Debug.Log("Monster Dead");
        m_Dead = true;
        gameObject.SetActive(false);
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
        m_CurrentHealth -= amount;
        if (m_CurrentHealth < 0 && !m_Dead)
        {
            OnDeath();
        }
    }

}
