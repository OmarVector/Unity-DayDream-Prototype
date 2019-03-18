using System.Collections.Generic;
using UnityEngine;
using static HelperClass;

////////////////////////////////////////////
/// EnemySpawner Weapon Class
/// ////////////////////////////////////////

public class EnemySpawner : MonoBehaviour
{
    static int POOLSIZE = 20; // Enemy Pool Size
    
    [SerializeField] private GameObject EnemyGO; // Enemy Prefab we going to use to instantiate .
    [HideInInspector] public List<GameObject> EnemyPool; // Enemy Pool List
    
    public float SpawningRate = 2.5f; 
    private int basicHealth = 100;
    private int basicDamage = 50;
    

    private void Awake()
    {
        InitializeEnemyPool();
        InvokeRepeating("GetEnemyFromPool",0,SpawningRate);
    }
    

    // Initializing Pool
    private void InitializeEnemyPool()
    {
        for (int i = 0; i < POOLSIZE; ++i)
        {
            var go = Instantiate(EnemyGO, transform.position, Quaternion.identity);
            go.SetActive(false);
            var GoEnemy = go.GetComponent<Enemy>();
            GoEnemy.SetEnemyPower(basicHealth,basicDamage,Color.blue, ScoreManager.scoreManager.LevelDiff);
            GoEnemy.Spawner = this;
            EnemyPool.Add(go);
        }
        
    }

    
    private void GetEnemyFromPool()
    {
        for (int i = 0; i < POOLSIZE; ++i)
        {
            if (!EnemyPool[i].activeInHierarchy)
            {
                var go = EnemyPool[i];
                go.transform.position = RandomLocationWithinCircle(transform.position, 0, 2); 
                go.SetActive(true);
                return;
            }
        }
    }

    public void ReturnEnemyToPool(GameObject go)
    {
        go.SetActive(false);
        go.transform.position = transform.position;
    }
}
