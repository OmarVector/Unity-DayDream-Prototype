using UnityEngine;

////////////////////////////////////////////
/// MP5 Weapon Class
/// ////////////////////////////////////////
public class MP5 : Weapon
{
    // firing particles
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        weaponName = "MP5";
        ammoClipSize = 30;
        ammo = ammoClipSize;
        damage = 10;
        fireRate = 0.1f;
        UpgradeCost = 500;
        reloadingSpeed = 1f;
       
    }

    protected override void UpgradeWeapon()
    {
        Debug.Log("MP% Upgrade");
        damage *= 2;
        reloadingSpeed -= 0.1f;
        ammoClipSize += 5;
        ammo = ammoClipSize;
        base.UpgradeWeapon();
    }


}
