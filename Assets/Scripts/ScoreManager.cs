using UnityEngine;


public class ScoreManager : MonoBehaviour
{
    public static ScoreManager scoreManager;
    
    [HideInInspector] public int Score; // with this points, we can upgrade weapons later on

    [HideInInspector] public int LevelDiff = 1;

    
    private void Awake()
    {
        if (scoreManager == null)
            scoreManager = this;
        else if (scoreManager != this)
        {
            Destroy(gameObject);
        }
     

        //Enable while switching level, but since we are only working on one Level, no need for it.
       // DontDestroyOnLoad(gameObject);
        
        InvokeRepeating(nameof(LevelDiffUp),10,10);//increase the hardness of the game every 10 sec
    }
    
    
    private void LevelDiffUp()
    {
        LevelDiff++;
    }

}