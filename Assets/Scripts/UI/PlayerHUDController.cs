using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUDController : MonoBehaviour
{
    [Header("UI Setter Referece")] [SerializeField]
    [HideInInspector ]public UISetter playerHUD;

    [SerializeField] private Player player;

    // in this example they are private. but I believe they should be public if I will spawn the weapons instead.
    [Header("Right Gun")] [SerializeField] private Weapon Gun_R;
    [Header("Left Gun")] [SerializeField] private Weapon Gun_L;

    private int initialHealth;
    private int initialArmor;

    // Start is called before the first frame update
    void Start()
    {
        initialHealth = player.Health;
        initialArmor = player.Armor;
       

        UpdatePlayerHUD();
    }

    public void UpdatePlayerHUD()
    {
        playerHUD.HealthFillImage.fillAmount = (float) player.Health / initialHealth;
        playerHUD.ArmorFillImage.fillAmount = (float) player.Armor / initialArmor;
       
        playerHUD.RightWeaponAmmo.fillAmount = (float) Gun_R.ammo / Gun_R.ammoClipSize;
        playerHUD.LeftWeaponAmmo.fillAmount =  (float) Gun_L.ammo / Gun_L.ammoClipSize;

        playerHUD.TotalHealthAmount.text = (player.Armor + player.Health).ToString();
        playerHUD.Score.text = "Score : " + ScoreManager.scoreManager.Score;
    }
}