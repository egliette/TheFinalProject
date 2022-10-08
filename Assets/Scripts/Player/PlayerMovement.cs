using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] float m_Speed = 10f;

    private Animator m_Animator;
    private RaycastHit2D m_Hit;
    private BoxCollider2D m_BoxCollider;
    private Vector3 m_moveDelta;

    private void Start()
    {
        m_BoxCollider = GetComponent<BoxCollider2D>();    
        m_Animator = GetComponent<Animator>();  
    }

    private void FixedUpdate() 
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        float dirY = Input.GetAxisRaw("Vertical");

        m_moveDelta = new Vector3(dirX, dirY, 0);

        if (m_moveDelta != Vector3.zero)
            m_Animator.SetBool("running", true);
        else
            m_Animator.SetBool("running", false);

        if (m_moveDelta.x > 0)
            transform.localScale = Vector3.one;
        else if (m_moveDelta.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        float step = Time.deltaTime * m_Speed;
        float deltaY = m_moveDelta.y * step;
        float deltaX = m_moveDelta.x * step;

        m_Hit = Physics2D.BoxCast(m_BoxCollider.bounds.center, m_BoxCollider.size, 0, 
                                new Vector2(0, m_moveDelta.y),
                                Mathf.Abs(deltaY),
                                LayerMask.GetMask("Blocking"));
        if (m_Hit.collider == null)
        {
            transform.Translate(0, deltaY, 0);
        }

        m_Hit = Physics2D.BoxCast(m_BoxCollider.bounds.center, m_BoxCollider.size, 0, 
                                new Vector2(m_moveDelta.x, 0),
                                Mathf.Abs(deltaX),
                                LayerMask.GetMask("Blocking"));
        if (m_Hit.collider == null)
        {
            transform.Translate(deltaX, 0, 0);
        }
    }
}
