using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected string enemyName; // Enemy Name
    private int health; // Enemy Health
    private int damage; // Enemy Damage
    private Transform goTransform; // Transfrom so I force enemy to loot at player
    private Player player; // Reference to player class to do damage once they hit 
    private bool isAttacking; // Checking if Enemy is attacking
    private Material enemyMaterial;  // Enemy Material which I use to change the glow color based on how tough they are.

    [HideInInspector] public EnemySpawner Spawner; // Spawner 
    [HideInInspector] private int defaultHealth; // Storing health to be reset once its recycled through Object pool


    protected virtual void Awake()
    {
        // Caching
        goTransform = gameObject.transform;
        enemyMaterial = gameObject.transform.GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer>().material;
    }


// Called when Game Level Difficulty is updated or Enemy is Loaded
    public void SetEnemyPower(int eHealth, int eDamage, Color eColor, int level)
    {
        health = eHealth * level;
        damage = eDamage * level;
        enemyMaterial.SetColor("_ColorEmissive", eColor); //not best approche anyway
        defaultHealth = health;
    }


    // Keep looking to player
    private void Update()
    {
        goTransform.LookAt(Camera.main.transform);
        goTransform.rotation = new Quaternion(0, transform.rotation.y, 0, 1);
    }

    // Receive Damage from player
    public void ReceiveDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            OnDeath();
        }
    }

    protected virtual void OnDeath()
    {
        Spawner.ReturnEnemyToPool(gameObject); // returning to pool
        var level = ScoreManager.scoreManager.LevelDiff; // getting diff level
        health = defaultHealth * level; //resenting health with the new diff level
       Debug.Log(health);
        // cancel invoking
        CancelInvoke(nameof(DoDamage));
        
        // Setting Emassive color of the enemy based on the diff. level
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

    
    
    
    
// DO Damage is repeatedly invoked when the enemy collide with player
    private void DoDamage()
    {
        player.ReceiveDamage(damage);
        if (player.isDead)
        {
            CancelInvoke(nameof(DoDamage));
        }
    }

// I believe this part could be much better in term of design , 
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
        if (other.collider.gameObject.name == "Player") // I would believe it could be another way than comparing two strings . 
        {
            isAttacking = false;

            CancelInvoke(nameof(DoDamage));
            Debug.Log("onExit");
        }
    }
}