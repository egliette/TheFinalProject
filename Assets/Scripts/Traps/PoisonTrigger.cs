using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTrigger : TrapTrigger
{
    // Start is called before the first frame update
    protected override void OnStart()
    {
        colliders = GetComponents<CircleCollider2D>();
        anim = GetComponent<Animator>();
        playerMask = LayerMask.GetMask(name_);
        anim.enabled = false;
        colliders[1].enabled = false;
        //gameObject.AddComponent<TrapdoorTrigger>();
    }

    protected override void OnUpdate()
    {
        if (triggered() && !alreadyTriggered)
        {
            anim.enabled = true;
            colliders[1].enabled = true;
            alreadyTriggered = true;
        }
    }

    // Update is called once per frame
    protected override bool triggered()
    {
        CircleCollider2D triggerCollider = (CircleCollider2D)colliders[0];
        return Physics2D.OverlapCircleAll(triggerCollider.bounds.center, triggerCollider.radius, playerMask).Length > 0;
    }
}
