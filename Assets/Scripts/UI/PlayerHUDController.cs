using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUDController : MonoBehaviour
{
    [Header("UI Setter Referece")] [SerializeField]
    private UISetter playerHUD;

    [SerializeField] private Player player;

    // in this example they are private. but I believe they should be public if I will spawn the weapons instead.
    [Header("Right Gun")] [SerializeField] private Weapon Gun_R;
    [Header("Left Gun")] [SerializeField] private Weapon Gun_L;

    private int initialHealth;
    private int initialArmor;
    private float initialAmmoG_R;
    private float initialAmmoG_L;

    // Start is called before the first frame update
    void Start()
    {
        initialHealth = player.Health;
        initialArmor = player.Armor;
        initialAmmoG_R = Gun_R.ammoClipSize;
        initialAmmoG_L = Gun_L.ammoClipSize;
       

        UpdatePlayerHUD();
    }

    public void UpdatePlayerHUD()
    {
        playerHUD.healthFillImage.fillAmount = (float) player.Health / initialHealth;
        playerHUD.armorFillImage.fillAmount = (float) player.Armor / initialArmor;
        
        playerHUD.rightWeaponAmmo.fillAmount =  Gun_R.ammo / initialAmmoG_R;
        playerHUD.leftWeaponAmmo.fillAmount =  Gun_L.ammo / initialAmmoG_L;

        playerHUD.totalHealthAmount.text = (player.Armor + player.Health).ToString();
    }
}