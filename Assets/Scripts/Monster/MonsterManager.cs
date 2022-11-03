using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField] private List<Monster> m_ActiveMonsters;
    [SerializeField] private MonsterConfig[] m_MonsterConfigs;
    [SerializeField] private MonsterWaveConfig[] m_MonsterWaveConfigs;

    private float m_SpawnTimeInterval = 2f;

    public MonsterAssetsPath m_MonsterAssets;

    #region Singleton
    public static MonsterManager Instance;
    private void Awake()
    {
        Instance = this;
        m_ActiveMonsters = new List<Monster>();
        m_MonsterAssets = new MonsterAssetsPath();

    }
    #endregion

    void Start()
    {
    }


    public Monster CreateNewMonster(int ID, Vector3 position)
    {
        MonsterConfig monsterConfig= m_MonsterConfigs[ID];
        // create a game object presenting monster
        GameObject newMonsterObj = ObjectPooler.Instance.SpawnFromPool(monsterConfig.monsterName, position, Quaternion.identity);
        //Monster newMonster = newMonsterObj.GetComponent<Monster>();



        // Change sprite, animation and intialize config depend on monster type
        //newMonster.ConfigMonsterData(monsterConfig);

        return newMonsterObj.GetComponent<Monster>();

    }


    public void AddMonster(Monster newMonster)
    {
        m_ActiveMonsters.Add(newMonster);
    }

    public Monster RemoveMonster(Monster monsterToRemove)
    {
        m_ActiveMonsters.Remove(monsterToRemove);
        return monsterToRemove;
    }


    public bool NoMonsterLeft()
    {
        return m_ActiveMonsters.Count == 0;
    }

    public void SpawnNewMonsterWave(int stageID, float waitTime)
    {
        StartCoroutine(CreateMonsterWave(stageID, waitTime));

    }


    private IEnumerator CreateMonsterWave(int stageID, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Debug.Log("After " + waitTime);

        MonsterWaveConfig config = m_MonsterWaveConfigs[stageID];
        
        // spawn monster every m_SpawnTimeInterval seconds
        for (int i = 0; i< config.TotalNormalMonster; ++i)
        {
            CreateNewMonster(0, config.SpawnPosition).SetDetecTargetRange(50);
            yield return new WaitForSeconds(m_SpawnTimeInterval);

        }

        for (int i = 0; i < config.TotalRangeMonster; ++i)
        {
            CreateNewMonster(1, config.SpawnPosition).SetDetecTargetRange(50);
            yield return new WaitForSeconds(m_SpawnTimeInterval);

        }

        for (int i = 0; i < config.TotalExplodeMonster; ++i)
        {
            CreateNewMonster(2, config.SpawnPosition).SetDetecTargetRange(50);
            yield return new WaitForSeconds(m_SpawnTimeInterval);

        }
        yield return null;

    }




    #region Getter setter
    public List<Monster> GetMonsterList()
    {
        return m_ActiveMonsters;
    }


    #endregion

}
