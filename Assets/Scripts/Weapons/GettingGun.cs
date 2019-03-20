using UnityEngine;

////////////////////////////////////////////
/// GettingGun Weapon Class
/// ////////////////////////////////////////

public class GettingGun : Weapon
{
    [SerializeField] private Transform gunRotation; // transform to rotate the Getting Gun

    private float rotatingAngle; //  rotation amount

    private float lerpAmount;

    protected override void Awake()
    {
        base.Awake();
        weaponName = "Getting Gun";
        ammoClipSize = 200;
        ammo = ammoClipSize;
        damage = 10;
        fireRate = 0.05f;
        reloadingSpeed = 4f;
    }

  

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
       
        
        // Hardcoding rotation animation of the getting gun
        if (isFiring && ammo>0)
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
    
    protected override void UpgradeWeapon()
    {
        damage *= 3;
        ammoClipSize += 20;
        ammo = ammoClipSize;
        reloadingSpeed -= 0.2f;
        base.UpgradeWeapon();
    }
}