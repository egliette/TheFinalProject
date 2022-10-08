using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider;
    private Animator animator;
    private RaycastHit2D hit;

    [SerializeField] float speed = 10f;

    private Vector3 moveDelta;

    private void Start()
    {
        // boxCollider = GetComponent<BoxCollider2D>();    
        animator = GetComponent<Animator>();  
    }

    private void FixedUpdate() 
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        float dirY = Input.GetAxisRaw("Vertical");

        moveDelta = new Vector3(dirX, dirY, 0);

        if (moveDelta != Vector3.zero)
            animator.SetBool("running", true);
        else
            animator.SetBool("running", false);

        if (moveDelta.x > 0)
            transform.localScale = Vector3.one;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        float step = Time.deltaTime * speed;
        float deltaY = moveDelta.y * step;
        float deltaX = moveDelta.x * step;

        hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.size, 0, 
                                new Vector2(0, moveDelta.y),
                                Mathf.Abs(deltaY),
                                LayerMask.GetMask("Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(0, deltaY, 0);
        }

        hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.size, 0, 
                                new Vector2(moveDelta.x, 0),
                                Mathf.Abs(deltaX),
                                LayerMask.GetMask("Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(deltaX, 0, 0);
        }
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center, boxCollider.size); 
    }
}
