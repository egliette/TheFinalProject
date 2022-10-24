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
        Debug.Log(traps.ToString());
        if (traps.Count > 0)
        {
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
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
                //Debug.Log(host.name);
                if (host.transform.parent.gameObject.name == "traps__6" || host.name.Substring(0, 5) == "trap1")
                {
                    if (!list.Contains(2))
                        list.Add(2);
                }
                else if (host.transform.parent.gameObject.name == "impaler" || host.name.Substring(0, 5).Equals("trap0"))
                {
                    if (!list.Contains(1))
                        list.Add(1);
                }
                else if (host.transform.parent.gameObject.name == "trapdoor" || host.name.Substring(0, 5).Equals("trap2"))
                    if (!list.Contains(3))
                        list.Add(3);
            }
                        
        }
        return list;
    }
}
