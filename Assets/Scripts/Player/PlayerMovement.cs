using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Animator animator;

    [SerializeField] public float speed = 10f;

    private Vector3 moveDelta;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();    
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
    
        transform.Translate(moveDelta * speed * Time.deltaTime);

    }

}
