using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Transform m_Gun;

    [SerializeField] private GameObject[] m_Bullets;
    [SerializeField] private float m_ShootCooldown = 0.1f;
    [SerializeField] private Transform m_FirePoint;
    
    private SpriteRenderer m_GunSprite;
    private Vector2 m_Direction;

    private float m_CooldownTimer;


    private void Start()
    {
        m_GunSprite = m_Gun.GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        FaceMouse();

        if (Input.GetMouseButton(0) && m_CooldownTimer > m_ShootCooldown)
        {
            Shoot();
        }

        if (m_CooldownTimer <= m_ShootCooldown)
        {
            m_CooldownTimer += Time.deltaTime;
        }
    }

    private void FaceMouse()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 screenPoint = Camera.main.ScreenToWorldPoint(transform.position);
        m_Direction = mousePos - (Vector2)m_Gun.position;
        m_Gun.transform.right = m_Direction;
        
        float rotZ = Mathf.Atan2(m_Direction.y, m_Direction.x) * Mathf.Rad2Deg;
        if (rotZ < 90 && rotZ > -90)
            m_GunSprite.flipY = false;
        else
            m_GunSprite.flipY = true;
    }

    private void Shoot()
    {   
        m_CooldownTimer = 0;
        int bulletNumber = FindBullet();
        m_Bullets[bulletNumber].transform.position = m_FirePoint.position;
        m_Bullets[bulletNumber].GetComponent<BulletProjectile>().SetDirection(m_Direction);
    }

    private int FindBullet()
    {
        for (int i = 0; i < m_Bullets.Length; i++)
        {
            if (!m_Bullets[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}
