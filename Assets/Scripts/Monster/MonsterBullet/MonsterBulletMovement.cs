using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBulletMovement : MonoBehaviour
{
    private Vector3 m_TargetPosition;
    private bool m_Moving;


    private Animator m_Animator;



    [SerializeField] private float m_BulletSpeed = 5f;
    [SerializeField] private float m_BulletExplodeRange = 1f;
    [SerializeField] private LayerMask m_PlayerLayerMask;
    [SerializeField] private float m_Damage = 1f;


    private void Awake()
    {
        m_Moving = false;
        m_Animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if(m_Moving)
        {
            if(Vector3.Distance(transform.position, m_TargetPosition) > 0.1f)
            {
                float step = m_BulletSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, m_TargetPosition, step);
            }
            else
            {
                Explode();
            }
        }
       
    }

    private void Explode()
    {
        OnExplosionUI();
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, m_BulletExplodeRange, m_PlayerLayerMask);
        foreach (Collider2D hit in hitColliders)
        {
            PlayerHealth playerHealth = hit.GetComponent<PlayerHealth>();
            if (playerHealth)
            {
                playerHealth.TakeDamage((int)m_Damage);
            }
        }
    }


    private void OnExplosionUI()
    {
        m_Animator.SetBool("explode", true);
    }


    private void DestroyBullet()
    {
        gameObject.SetActive(false);
        m_Animator.SetBool("explode", false);

    }


    public void SetTargetPosition(Vector3 targetPosition)
    {
        m_TargetPosition = targetPosition;
        m_Moving = true;
    }

}
