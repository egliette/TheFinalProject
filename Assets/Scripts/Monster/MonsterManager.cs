using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    private List<Monster> m_ActiveMonsters;
        
    [SerializeField] private GameObject m_MonsterPrefab;
    [SerializeField] private MonsterConfig[] monsterConfigs;

    public MonsterAssetsPath m_MonsterAssets;

    #region Singleton
    public static MonsterManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    void Start()
    {
        m_ActiveMonsters = new List<Monster>();
        m_MonsterAssets = new MonsterAssetsPath();
    }


    public Monster CreateNewMonster(MonsterConfig monsterConfig, Vector3 position)
    {
        // create a game object presenting monster
        GameObject newMonsterObj = Instantiate(m_MonsterPrefab, position, Quaternion.identity);
        Monster newMonster = newMonsterObj.GetComponent<Monster>();

        // Change sprite, animation and intialize config depend on monster type
        newMonster.ConfigMonsterData(monsterConfig);

        // add to manager's active list
        m_ActiveMonsters.Add(newMonster);
        return newMonster;
    }

    public bool RemoveMonster(Monster monsterToRemove)
    {
        foreach(Monster monster in m_ActiveMonsters) { 
            if(monster.GetMonsterConfig().monsterID == monsterToRemove.GetMonsterConfig().monsterID)
            {
                m_ActiveMonsters.Remove(monster);
                return true;
            }
        }
        
        return false;
    }

}
