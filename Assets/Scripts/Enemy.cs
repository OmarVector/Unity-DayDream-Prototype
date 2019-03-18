using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    protected string enemyName;
    private int health;
    private int damage;
    private Transform goTransform;
    private Player player;
    private bool isAttacking;
    private Material enemyMaterial;

    [HideInInspector] public EnemySpawner Spawner;
    [HideInInspector] private int defaultHealth;


    protected virtual void Awake()
    {
        goTransform = gameObject.transform;
        enemyMaterial = gameObject.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().material;
    }


// Called when Game Level Difficulty is updated
    public void SetEnemyPower(int eHealth, int eDamage, Color eColor, int level)
    {
        health = eHealth * level;
        damage = eDamage * level;
        enemyMaterial.SetColor("_ColorEmissive", eColor); //not best approche anyway
    }


    private void Update()
    {
        goTransform.LookAt(Camera.main.transform);
        goTransform.rotation = new Quaternion(0, transform.rotation.y, 0, 1);
    }

    public void ReceiveDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            // void DeadEnemy() any logic related to dead enemy;
            OnDeath();
            //Adding Scrore 
            //Destory
        }
    }

    protected virtual void OnDeath()
    {
        Spawner.ReturnEnemyToPool(gameObject);
        var level = ScoreManager.scoreManager.LevelDiff;
        health = defaultHealth * level;
        CancelInvoke(nameof(DoDamage));
        
        switch (level)
        {
            case 1:
                enemyMaterial
                    .SetColor("_ColorEmissive", Color.red);
                break;
            case 2:
                enemyMaterial
                    .SetColor("_ColorEmissive", Color.blue);
                break;
            case 3:
                enemyMaterial
                    .SetColor("_ColorEmissive", Color.yellow);
                break;
            case 4 : 
                enemyMaterial
                    .SetColor("_ColorEmissive", Color.green);
                break;
            case 5 : 
                enemyMaterial
                    .SetColor("_ColorEmissive", Color.magenta);
                break;
        }

        ScoreManager.scoreManager.Score += 100;
    }


    protected virtual void OnEnable()
    {
        defaultHealth = health;
    }


    private void DoDamage()
    {
        player.ReceiveDamage(damage);
        if (player.isDead)
        {
            CancelInvoke(nameof(DoDamage));
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.gameObject.GetComponent<Player>())
        {
            player = other.collider.gameObject.GetComponent<Player>();
            if (player.isDead)
            {
                CancelInvoke(nameof(DoDamage));
            }

            if (!isAttacking)
            {
                InvokeRepeating(nameof(DoDamage), 0, 1);
            }

            isAttacking = true;
        }
    }


    private void OnCollisionExit(Collision other)
    {
        if (other.collider.gameObject.name == "Player")
        {
            isAttacking = false;

            CancelInvoke(nameof(DoDamage));
            Debug.Log("onExit");
        }
    }
}