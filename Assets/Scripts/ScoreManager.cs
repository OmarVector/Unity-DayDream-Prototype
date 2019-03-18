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
     

        DontDestroyOnLoad(gameObject);
        
        InvokeRepeating(nameof(LevelDiffUp),20,20);//increase the hardness of the game every 20 sec
    }
    
    
    private void LevelDiffUp()
    {
        LevelDiff++;
    }

}