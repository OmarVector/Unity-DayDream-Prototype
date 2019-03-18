using UnityEngine;

////////////////////////////////////////////
/// MP5 Weapon Class
/// ////////////////////////////////////////
public class MP5 : Weapon
{
    [SerializeField] private ParticleSystem part; // firing particles
    // Start is called before the first frame update
    void Start()
    {
        openFireParticles = part;
        weaponName = "MP5";
        ammoClipSize = 30;
        ammo = ammoClipSize;
        damage = 10;
        fireRate = 0.1f;
        UpgradeCost = 500;
    }


    public override void UpgradeWeapon()
    {
        damage *= 2;
        ammoClipSize += 5;
        ammo = ammoClipSize;
        base.UpgradeWeapon();
    }


}
