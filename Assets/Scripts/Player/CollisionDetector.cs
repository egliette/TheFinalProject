using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private BoxCollider2D collider;
    [SerializeField] private LayerMask trapMask;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: do actual things according to what traps the player is colliding with 
        if (collidedWithTraps() > 0) GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
    }

    private int collidedWithTraps()
    {
        Collider2D[] trapColliders = 
            Physics2D.OverlapBoxAll(collider.bounds.center, collider.bounds.size, 0f, trapMask);
        if (trapColliders.Length > 0)
        {
            // TODO: detecting multiple types of traps, returning the trap with the highest dealing damage
            GameObject host = trapColliders[0].gameObject;
            if (host.GetComponent<SpriteRenderer>().sprite.name.Substring(0, 7) == "traps__")
                return 1;
        }
        return 0;
    }
}
