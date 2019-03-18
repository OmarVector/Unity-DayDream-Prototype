using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int health = 1000;
    public bool isDead;


    public void ReceiveDamage(int damage)
    {
        if (!isDead)
        {
            if (health > 0)
            {
                health -= damage;
            }
            else
            {
                isDead = true;
            }
        }
        
        Debug.Log(health);
    }
    
   
}