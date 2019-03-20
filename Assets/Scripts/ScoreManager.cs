using UnityEngine;

////////////////////////////////////////////
/// ScoreManager Class SingleTone.
/// ////////////////////////////////////////
public class ScoreManager : MonoBehaviour
{
    // static scoremanger to ensure its single tone.
    public static ScoreManager scoreManager;
    
    // Score
    [HideInInspector] public int Score; // with this points, we can upgrade weapons later on

    // Level Difficulty 
    [HideInInspector] public int LevelDiff = 1;

    // Initializing Single Tone.
    private void Awake()
    {
        if (scoreManager == null)
            scoreManager = this;
        else if (scoreManager != this)
        {
            Destroy(gameObject);
        }
        
       DontDestroyOnLoad(gameObject); 
        
        InvokeRepeating(nameof(LevelDiffUp),10,10);//increase the hardness of the game every 10 sec
    }
    
    
    private void LevelDiffUp()
    {
        LevelDiff++;
    }

    public void ResetDifficulty()
    {
        LevelDiff = 1;
    }

}