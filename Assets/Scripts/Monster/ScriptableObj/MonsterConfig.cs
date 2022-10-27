using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Monster", menuName = "Monster")]
public class MonsterConfig : ScriptableObject
{
    public int monsterID;
    public string monsterName;

    // health
    public float health;

    // movement behaviors
    public float speed;
    public float detectTargetRange;

    // attack behaviors
    public Enums.MonsterAttackType attackType;
    public float attackRange;
    public float damage;

    public RuntimeAnimatorController monsterAnimator;
}
