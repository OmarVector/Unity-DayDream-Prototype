using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
////////////////////////////////////////////
/// Player Class
/// ////////////////////////////////////////
public class Player : MonoBehaviour
{
    // Player Health
    public int Health = 200;
    // Armor Health
    public int Armor = 100;
    // Canvas that render DEAD when player die
    [SerializeField] private Canvas deathCanvas;
    // To Check if the player is dead or not
    [HideInInspector] public bool isDead;
   
   
    // Reference to player HUD Controller.
    private PlayerHUDController playerHUD;

    // We set TimeScale to = 1.0 since when player die the time scale change then we restart the level.
    // Assigning playerHUD and making sure the deathCanvas is disabled.
    private void Start()
    {
        Time.timeScale = 1.0f;
        deathCanvas.enabled = false;
        playerHUD = GameObject.FindWithTag("HUD").GetComponent<PlayerHUDController>();
    }

    // When player receive damage from Enemy , its called when enemy is overlapping with player .
    public void ReceiveDamage(int damage)
    {
        playerHUD.UpdatePlayerHUD();

        if (!isDead)
        {
            if (Armor > 0)
            {
                Armor -= damage;
            }
            else
            {
                Health -= damage;
                if (Health <= 0)
                {
                    isDead = true;
                    StartCoroutine(OnPlayerDeath());
                }
            }
        }
    }

    // When player die
    private IEnumerator OnPlayerDeath()
    {
        
        Time.timeScale = 0.1f;
        deathCanvas.enabled = true;
        yield return new WaitForSeconds(0.5f);
        ScoreManager.scoreManager.ResetDifficulty();
        SceneManager.LoadScene(0);


    }
}