using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private float m_Speed = 20f;
    
    private BoxCollider2D m_Coll;
    private bool m_Hit;
    private Vector2 m_Direction;
    private float m_LifeTime;
    private Animator m_Anim;

    private void Awake()
    {
        m_Anim = GetComponent<Animator>();
        m_Coll = GetComponent<BoxCollider2D>();
    }

    public void Explode()
    {
        m_Hit = true;
        m_Coll.enabled = false;
        m_Anim.SetTrigger("explode");
    }

    private void Update()
    {
        if (m_Hit) return;
        float movementSpeed = m_Speed * Time.deltaTime;

        // preventing the bullet go through gameObject
        RaycastHit2D hit = Physics2D.Raycast(transform.position, m_Direction, 2 * movementSpeed);
        if (hit) 
        {
            string collName = hit.collider.name;
            if (collName == "Collision" || collName == "Zombie") 
            {
                transform.position = hit.point;
                Explode();
            }
        }

        transform.Translate(movementSpeed, 0, 0);

        m_LifeTime += Time.deltaTime;

        if (m_LifeTime > 3)
        {
            Explode();
        }   
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {   
        if (collision.gameObject.layer == LayerMask.NameToLayer("Blocking"))
        {
            Explode();
        }
    }

    public void SetDirection(Vector2 _direction)
    {   
        m_LifeTime = 0f;
        m_Direction = _direction;
        gameObject.SetActive(true);
        m_Hit = false;
        m_Coll.enabled = true;

        transform.right = m_Direction;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
