using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int m_StartingHealth = 10;
    [SerializeField] private int m_CurrentHealth;
    [SerializeField] private float m_HurtDelayTime = 0.5f;
    [SerializeField] private Behaviour[] m_Components;
    [SerializeField] private HealthBar m_HealthBar;

    private SpriteRenderer m_Sprite;
    private Animator m_Animator;
    
    private bool m_Invincible = false;
    private bool m_IsDeath = false;

    public GameObject m_gameOverCanvas;

    private void Start() 
    {
        m_CurrentHealth = m_StartingHealth;
        m_HealthBar.SetMaxHealth(m_StartingHealth);
        m_Sprite = GetComponent<SpriteRenderer>();
        m_Animator = GetComponent<Animator>();
        m_gameOverCanvas.SetActive(false);
    }


    public void TakeDamage(int damage)
    {
        if (m_Invincible == true || m_IsDeath)
            return;
        
        m_Invincible = true;
        m_CurrentHealth = m_CurrentHealth - damage;

        if (m_CurrentHealth < 0)
            m_CurrentHealth = 0;

        m_HealthBar.SetHealth(m_CurrentHealth);
        
        if (m_CurrentHealth > 0)
        {
            StartCoroutine(Hurt());
        }
        else 
        {
            Die();
        }
    }

    private IEnumerator Hurt()
    {
        Color originColor = m_Sprite.color;
        m_Sprite.color = Color.red;

        yield return new WaitForSeconds(m_HurtDelayTime);

        for (float t = 0; t < 1.0f; t += Time.deltaTime/m_HurtDelayTime)
        {
            m_Sprite.color = Color.Lerp(Color.red, originColor, t);

            yield return null;
        }

        m_Sprite.color = originColor;
        m_Invincible = false;
    }

    public void Die()
    {
        if (m_IsDeath)
            return;
            
        m_IsDeath = true;
        m_HealthBar.SetHealth(0);
        m_Animator.SetTrigger("death");
        foreach (Behaviour component in m_Components)
            component.enabled = false;
    }

    

    private void OnTriggerStay2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }        
    }
}
