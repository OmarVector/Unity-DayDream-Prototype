using System.Collections.Generic;
using UnityEngine;

////////////////////////////////////////////
/// EnemySpawner Class
/// ////////////////////////////////////////

public class EnemySpawner : MonoBehaviour
{
    // Enemy Pool Size
    static int POOLSIZE = 10; // Enemy Pool Size
    
    // Enemy Prefab we going to use to instantiate .
    [SerializeField] private GameObject EnemyGO;
    // Enemy Pool List
    [HideInInspector] public List<GameObject> EnemyPool;
    //Spawning Rate
    public float SpawningRate = 1.0f; 
    // Enemy Basic Initial health
    private int basicHealth = 100;
    // Enemy basic Initial Damage;
    private int basicDamage = 50;
    

    // Initializing the pool and making starting spawning in random time range.
    private void Awake()
    {
        float rand = Random.Range(0, 4);
        InitializeEnemyPool();
        InvokeRepeating("GetEnemyFromPool",rand,SpawningRate);
    }
    

    // Initializing Pool
    private void InitializeEnemyPool()
    {
        for (int i = 0; i < POOLSIZE; ++i)
        {
            
            GameObject go = Instantiate(EnemyGO, transform.position, Quaternion.identity) ;
           
            go.transform.SetParent(transform);
            Enemy GoEnemy = go.GetComponent<Enemy>();
           
            GoEnemy.SetEnemyPower(basicHealth,basicDamage,Color.blue, ScoreManager.scoreManager.LevelDiff);
            GoEnemy.Spawner = this;
            go.SetActive(false);
            EnemyPool.Add(go);
        }
        
    }

    
    // Getting Enemy from the pool
    private void GetEnemyFromPool()
    {
        for (int i = 0; i < POOLSIZE; ++i)
        {
            if (!EnemyPool[i].activeInHierarchy)
            {
                var go = EnemyPool[i];
                go.transform.position = HelperClass.RandomLocationWithinCircle(transform.position, 0, 2); 
                go.SetActive(true);
                return;
            }
        }
    }

    // Recycling enemy
    public void ReturnEnemyToPool(GameObject go)
    {
        go.SetActive(false);
        go.transform.position = transform.position;
    }
}
