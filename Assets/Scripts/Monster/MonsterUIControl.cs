using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Monster
{
    private Animator m_Animator;


    public void OnAtackUI()
    {
        // stop running animation
        m_Animator.SetFloat("Speed", 0f);
        // start attack animation
        m_Animator.SetBool("OnAttack", true);

    }

    public void OnRunningUI()
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
    }


}
