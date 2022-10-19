using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrapTrigger : MonoBehaviour
{
    protected Animator anim;
    protected Collider2D[] colliders;
    protected LayerMask playerMask;
    protected string[] name_ = { "Player" };
    protected bool alreadyTriggered = false;
    // Start is called before the first frame update
    protected void Start()
    {
        OnStart();
    }

    protected abstract void OnStart();
    protected abstract void OnUpdate();

    // Update is called once per frame
    protected void Update()
    {
        OnUpdate();
    }

    protected abstract bool triggered();
}
