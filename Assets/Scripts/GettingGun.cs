using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GettingGun : Weapon
{
    [SerializeField] private Transform gunRotation;
    [SerializeField] private ParticleSystem part;

    private float rotatingAngle;

    private float lerpAmount;

    // Start is called before the first frame update
    void Start()
    {
        openFireParticles = part;

        Weaponname = "Getting Gun";
        ammoClipSize = 200;
        ammo = ammoClipSize;
        damage = 10;
        fireRate = 0.05f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
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

    public override void UpgradeWeapon()
    {
        damage *= 3;
        ammoClipSize += 20;
        ammo = ammoClipSize;
        base.UpgradeWeapon();
    }
}