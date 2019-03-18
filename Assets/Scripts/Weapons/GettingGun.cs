﻿using UnityEngine;

////////////////////////////////////////////
/// GettingGun Weapon Class
/// ////////////////////////////////////////

public class GettingGun : Weapon
{
    [SerializeField] private Transform gunRotation; // transform to rotate the Getting Gun
    [SerializeField] private ParticleSystem part; // firing particles

    private float rotatingAngle; //  rotation amount

    private float lerpAmount;

    // Start is called before the first frame update
    void Start()
    {
        weaponName = "Getting Gun";
        ammoClipSize = 200;
        ammo = ammoClipSize;
        damage = 10;
        fireRate = 0.05f;
        openFireParticles = part;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        
        // Hardcoding rotating animation of the getting gun
        if (isFiring)
        {
            lerpAmount = 0;
            rotatingAngle += 20;
            gunRotation.localRotation = Quaternion.Euler(0, 0, rotatingAngle);
        }
        else
        {
            if (lerpAmount > 1)
                return;
            lerpAmount += Time.deltaTime;
            rotatingAngle = Mathf.Lerp(rotatingAngle, 0, lerpAmount);
            gunRotation.localRotation = Quaternion.Euler(0, 0, rotatingAngle);
        }
    }

    //Upgrading TODO UI
    public override void UpgradeWeapon()
    {
        damage *= 3;
        ammoClipSize += 20;
        ammo = ammoClipSize;
        base.UpgradeWeapon();
    }
}