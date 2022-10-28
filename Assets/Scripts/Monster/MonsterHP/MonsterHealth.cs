using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    [SerializeField] private Monster m_Monster;
    [SerializeField] private SpriteRenderer m_Sprite;

    private Color originColor;
    private float m_StartHealth;
    private float m_CurrentHealth;
    private bool m_Dead;
    private float m_HurtDelayTime = 0.2f;


    private void OnEnable()
    {
        m_Dead = false;
        originColor = m_Sprite.color;
    }

    private void OnDeath()
    {
        m_Dead = true;
        m_Monster.OnDeadUI();
    }

    private IEnumerator Hurt()
    {
        m_Sprite.color = Color.red;

        yield return new WaitForSeconds(m_HurtDelayTime);

        m_Sprite.color = originColor;
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
        if (m_CurrentHealth <= 0 && !m_Dead)
        {
            OnDeath();
        }
        else
        {
            StartCoroutine(Hurt());
        }
    }

}
