using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewMonsterWave", menuName = "Monster Wave")]
public class MonsterWaveConfig : ScriptableObject
{
    public Vector3 SpawnPosition; 
    public int TotalNormalMonster;
    public int TotalRangeMonster;
    public int TotalExplodeMonster;
}
