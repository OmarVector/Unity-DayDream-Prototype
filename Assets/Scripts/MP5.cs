using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP5 : Weapon
{
    [SerializeField] private ParticleSystem part;
    // Start is called before the first frame update
    void Start()
    {
        openFireParticles = part;
        Weaponname = "MP5";
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

//we can override any fucntion we want here to add more feature per weapon.
}
