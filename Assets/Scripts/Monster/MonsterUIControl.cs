using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Monster
{
    private Animator m_Animator;
    private Vector3 m_PrevPos;

    public void OnAtackUI()
    {
        // stop running animation
        m_Animator.SetFloat("Speed", 0f);
        // start attack animation
        m_Animator.SetBool("OnAttack", true);

    }

    public void OnMovingUI()
    {
        // stop attack animation
        m_Animator.SetBool("OnAttack", false);
        // start running animation
        m_Animator.SetFloat("Speed", 1f);
    }

    public void OnIdleUI()
    {
        // stop attack animation
        m_Animator.SetBool("OnAttack", false);
        // stop running animation
        m_Animator.SetFloat("Speed", 0f);
    }

    public void OnDeadUI()
    {
        m_Animator.SetBool("Dead", true);
        m_Animator.SetFloat("Speed", 0);
        m_Animator.SetBool("OnAttack", false);
    }
}
