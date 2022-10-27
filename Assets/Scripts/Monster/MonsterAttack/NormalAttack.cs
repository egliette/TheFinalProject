using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttack : IMonsterAttack
{
    public void DoAttack(GameObject target)
    {
        Debug.Log("MELEE ATACKK");
    }
}
