using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [HideInInspector] public bool isDead;
    // public for UI , Hardcoded value
    public int Health = 200;
    public int Armor = 100;
    [SerializeField] private Canvas deathCanvas;
   
   

    private PlayerHUDController playerHUD;

    private void Start()
    {
        Time.timeScale = 1.0f;
        deathCanvas.enabled = false;
        playerHUD = GameObject.FindWithTag("HUD").GetComponent<PlayerHUDController>();
    }

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

    private IEnumerator OnPlayerDeath()
    {
        Time.timeScale = 0.2f;
        deathCanvas.enabled = true;
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(0);


    }
}