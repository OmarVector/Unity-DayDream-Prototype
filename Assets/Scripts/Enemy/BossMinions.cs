using UnityEngine;

public class BossMinions : Enemy
    {
        // dealing with our custom animator , //quite hard coded animation
        private Animator anim;
    
        //overriding Awake Function just cach the animator on Awake
        protected override void Awake()
        {
            enabled = true;
            base.Awake();
            anim = GetComponent<Animator>();
            enemyName = "Boss Minion";
        }
    
        //overriding OnDeath to disable the animator
        protected override void OnDeath()
        {
            base.OnDeath();
            anim.enabled = false;
        }

        private void OnEnable()
        {
            //Hardcoded animation just to give the feel
            anim.enabled = true;
            anim.Play("Take 001");
        }
    
      
    }

