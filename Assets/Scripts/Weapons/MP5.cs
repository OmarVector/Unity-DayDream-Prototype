﻿using UnityEngine;

////////////////////////////////////////////
/// MP5 Weapon Class
/// ////////////////////////////////////////
public class MP5 : Weapon
{
    // firing particles
    // Start is called before the first frame update
    protected override void Awake()
    {
        weaponName = "MP5";
        ammoClipSize = 30;
        ammo = ammoClipSize;
        damage = 10;
        fireRate = 0.1f;
        UpgradeCost = 500;
        reloadingSpeed = 1f;
        base.Awake();
       
    }

    protected override void UpgradeWeapon()
    {
        damage *= 2;
        reloadingSpeed -= 0.1f;
        ammoClipSize += 5;
        ammo = ammoClipSize;
        base.UpgradeWeapon();
    }


}
