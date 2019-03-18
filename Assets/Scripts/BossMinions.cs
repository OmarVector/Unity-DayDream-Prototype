using UnityEngine;

public class BossMinions : Enemy
{
    // dealing with our custom animator
    private Animator anim;
    
    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        enemyName = "Boss Minion";
    }
    
    protected override void OnDeath()
    {
       base.OnDeath();
        anim.enabled = false;
    }

    protected override void OnEnable()
    {
       base.OnEnable();
        anim.enabled = true;
        anim.Play("Take 001");
    }
    
   
}
