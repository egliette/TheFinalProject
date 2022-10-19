using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class TrapdoorTrigger : TrapTrigger
{
    // Start is called before the first frame update
    protected override void OnStart()
    {
        colliders = GetComponents<Collider2D>();
        anim = GetComponent<Animator>();
        playerMask = LayerMask.GetMask(name_);
        anim.enabled = false;
    }

    protected override void OnUpdate()
    {
        if (!alreadyTriggered && triggered())
        {
            anim.enabled = true;
            alreadyTriggered = true;
        }
    }

    // Update is called once per frame
    protected override bool triggered()
    {
        BoxCollider2D triggerCollider = (BoxCollider2D)colliders[0];
        return Physics2D.OverlapBoxAll(triggerCollider.bounds.center, triggerCollider.bounds.size, 0f, playerMask).Length > 0;
    }
}
