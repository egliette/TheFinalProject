using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField] private List<Monster> m_ActiveMonsters;
        
    [SerializeField] private GameObject m_MonsterPrefab;
    [SerializeField] private MonsterConfig[] monsterConfigs;

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
        MonsterConfig monsterConfig= MonsterManager.Instance.monsterConfigs[ID];
        // create a game object presenting monster
        GameObject newMonsterObj = ObjectPooler.Instance.SpawnFromPool(monsterConfig.monsterName, position, Quaternion.identity);
        //GameObject newMonsterObj = Instantiate(m_MonsterPrefab, position, Quaternion.identity);
        Monster newMonster = newMonsterObj.GetComponent<Monster>();
        


        // Change sprite, animation and intialize config depend on monster type
        newMonster.ConfigMonsterData(monsterConfig);

        return newMonster;
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


    #region Getter setter
    public List<Monster> GetMonsterList()
    {
        return m_ActiveMonsters;
    }


    #endregion

}
