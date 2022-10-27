using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Monster : MonoBehaviour
{
    [SerializeField] private LayerMask m_PlayerMask;
    [SerializeField] private MonsterConfig m_MonsterConfig;
    [SerializeField] private MonsterMovement m_MonsterMovement;
    [SerializeField] private MonsterHealth m_MonsterHealth;
    [SerializeField] private MonsterAttack m_MonsterAttack;


    // define monster states
    private IMonsterState m_MonsterCurrentState;
    public IdleState m_MonsterIdleState;
    public AttackState m_MonsterAttackState;
    public ApproachTargetState m_MonsterApproachTargetState;
    private GameObject m_Target;


    private void Awake()
    {
        m_MonsterIdleState = new IdleState(this);
        m_MonsterAttackState = new AttackState(this);
        m_MonsterApproachTargetState = new ApproachTargetState(this);
        m_MonsterCurrentState = m_MonsterIdleState;

        m_Animator = GetComponent<Animator>();

        if(m_MonsterConfig != null)
        {
            ConfigMonsterData(m_MonsterConfig);
        }
    }

    private void FixedUpdate()
    {
        Vector2 prevPos = transform.position;
       
        // do behaviors depend on current state
        m_MonsterCurrentState = m_MonsterCurrentState.DoState();

        // flip animation
        Vector2 curPos = transform.position;
        Vector3 moveDelta = new Vector3(curPos.x - prevPos.x, curPos.y - prevPos.y, 0);
        Utils.FlipAnimation(gameObject, moveDelta);
    }

    public void ConfigMonsterData(MonsterConfig config)
    {
        // set config for later use
        m_MonsterConfig = config;
        
        // set animator controller
        m_Animator.runtimeAnimatorController = config.monsterAnimator;

        // set up data for different monster's component
        GetMonsterHealth().ConfigMonsterData(config);
        GetMonsterMovement().ConfigMonsterData(config);
        GetMonsterAttack().ConfigMonsterData(config);
    }


    #region getter setter
    public MonsterConfig GetMonsterConfig()
    {
        return m_MonsterConfig;
    }
    public LayerMask GetPlayerMask()
    {
        return m_PlayerMask;
    }

    public MonsterMovement GetMonsterMovement()
    {
        return m_MonsterMovement;
    }

    public MonsterHealth GetMonsterHealth()
    {
        return m_MonsterHealth;
    }

    public MonsterAttack GetMonsterAttack()
    {
        return m_MonsterAttack;
    }

    public GameObject GetTarget()
    {
        return m_Target;
    }

    public void SetTarget(GameObject target)
    {
        m_Target = target;
    }

    #endregion
}
