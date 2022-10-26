using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] float m_Speed = 10f;

    private Animator m_Animator;
    private RaycastHit2D m_Hit;
    private BoxCollider2D m_BoxCollider;
    private SpriteRenderer m_Sprite;
    private Vector3 m_MoveDelta;

    private void Start()
    {
        m_BoxCollider = GetComponent<BoxCollider2D>();    
        m_Animator = GetComponent<Animator>();  
        m_Sprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() 
    {
        FaceMouse();

        float dirX = Input.GetAxisRaw("Horizontal");
        float dirY = Input.GetAxisRaw("Vertical");

        m_MoveDelta = new Vector3(dirX, dirY, 0);

        if (m_MoveDelta != Vector3.zero)
            m_Animator.SetBool("running", true);
        else
            m_Animator.SetBool("running", false);

        float step = Time.deltaTime * m_Speed;
        float deltaY = m_MoveDelta.y * step;
        float deltaX = m_MoveDelta.x * step;

        m_Hit = Physics2D.BoxCast(m_BoxCollider.bounds.center, m_BoxCollider.size, 0, 
                                new Vector2(0, m_MoveDelta.y),
                                Mathf.Abs(deltaY),
                                LayerMask.GetMask("Blocking"));
        if (m_Hit.collider == null)
        {
            transform.Translate(0, deltaY, 0);
        }

        m_Hit = Physics2D.BoxCast(m_BoxCollider.bounds.center, m_BoxCollider.size, 0, 
                                new Vector2(m_MoveDelta.x, 0),
                                Mathf.Abs(deltaX),
                                LayerMask.GetMask("Blocking"));
        if (m_Hit.collider == null)
        {
            transform.Translate(deltaX, 0, 0);
        }
    }

    private void FaceMouse()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 screenPoint = Camera.main.ScreenToWorldPoint(transform.position);
        Vector2 direction = mousePos - (Vector2)transform.position;
        
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (rotZ < 90 && rotZ > -90)
            m_Sprite.flipX = false;
        else
            m_Sprite.flipX = true;
    }
}