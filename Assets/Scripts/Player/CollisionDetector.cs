using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private BoxCollider2D collider_;
    [SerializeField] private LayerMask trapMask;
    public bool slowed;
    public bool walkedOnTrapdoor;
    // Start is called before the first frame update

    void Start()
    {
        slowed = false;
        walkedOnTrapdoor = false;
        string[] name = { "Traps" };
        trapMask = LayerMask.GetMask(name);
        //pm = gameObject.GetComponent<PlayerMovement>();
        collider_ = GetComponent<BoxCollider2D>();
        slowed = false;
    }
    void DoSomething() { }
    // Update is called once per frame
    void Update()
    {
        // TODO: do actual things according to what traps the player is colliding with 
        List<int> traps = collidedWithTraps();
        if (traps.Count > 0)
        {
            if (traps.Contains(1))
                ;   // deal damage
            if (traps.Contains(2))
                slowed = true;
            if (traps.Contains(3))
                walkedOnTrapdoor = true;
        }
        // Do stuff
        DoSomething();

        // Reset state for next update
        slowed = false;
        walkedOnTrapdoor=false;
    }

    private List<int> collidedWithTraps()
    {
        List<int> list = new List<int>();
        Collider2D[] trapColliders = 
            Physics2D.OverlapBoxAll(collider_.bounds.center, collider_.bounds.size, 0f, trapMask);
        if (trapColliders.Length > 0)
        {
            foreach (Collider2D trapCollider in trapColliders) {
                GameObject host = trapCollider.gameObject;
                
                if (host.transform.parent.gameObject.name == "poison" && ((CircleCollider2D)(trapCollider)).radius == .4f)
                    if (!list.Contains(2))
                        list.Add(2);
                else if (host.transform.parent.gameObject.name == "impaler")
                        if (!list.Contains(1))
                            list.Add(1);
                else if (host.transform.parent.gameObject.name == "trapdoor")
                            if (!list.Contains(3))
                                list.Add(3);
            }
                        
        }
        return list;
    }
}
