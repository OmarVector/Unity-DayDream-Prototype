using System.Collections.Generic;
using UnityEngine;
using static HelperClass;

public class EnemySpawner : MonoBehaviour
{
    static int POOLSIZE = 20;
    
    [SerializeField] private GameObject EnemyGO;
    [HideInInspector] public List<GameObject> EnemyPool;
    
    public float SpawningRate = 5f;
    private int basicHealth = 100;
    private int basicDamage = 50;

    private int difficultly = 1; // max 10
    
    

    private void Awake()
    {
        InitializeEnemyPool();
        InvokeRepeating("GetEnemyFromPool",0,SpawningRate);
    }
    

    private void InitializeEnemyPool()
    {
        for (int i = 0; i < POOLSIZE; ++i)
        {
            var go = Instantiate(EnemyGO, transform.position, Quaternion.identity);
            go.SetActive(false);
            var GoEnemy = go.GetComponent<Enemy>();
            GoEnemy.SetEnemyPower(basicHealth,basicDamage,Color.blue, difficultly);
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
