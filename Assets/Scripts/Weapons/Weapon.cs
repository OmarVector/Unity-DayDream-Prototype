
using UnityEngine;


public class Weapon : MonoBehaviour
{
    protected string weaponName { get; set; } 
    protected int damage { get; set; }
    protected int accuracy { get; set; } //Not used but added
    protected int recoil { get; set; }//Not used but added
    protected float fireRate { get; set; }
    protected float reloadSpeed { get; set; }//Not used but added
    protected int ammoClipSize { get; set; }
    protected int ammo { get; set; }
    protected bool isFiring;
    protected int UpgradeCost; // TODO
    protected ParticleSystem openFireParticles;
    
    private Material weaponMat;
    //to avoid GC
    private Ray ray;
    private RaycastHit hit;


    // enum power level of the weapon. 
    public enum Levels
    {
        Level_01 = 1,
        Level_02 = 2,
        Level_03 = 3,
        Level_04 = 4,
        Level_05 = 5
    }

    protected Levels WeaponLevel; // TODO

    private int layer_mask;

    
    private void Awake()
    {
        layer_mask = LayerMask.GetMask("EnemyLayer");
        weaponMat = gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material;

    }

    // Virtual just if at any case we want to override thie per weapon.
    protected virtual void Fire()
    {
        if (ammo > 0)
        {
            //fire ray from center of the screen
            ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            Debug.DrawRay(ray.origin, ray.direction, Color.green, 5);

            if (Physics.Raycast(ray, out hit, 20, layer_mask))
            {
                Debug.Log(hit.collider.gameObject.name);
                hit.collider.gameObject.GetComponent<Enemy>().ReceiveDamage(damage);
            }

            ammo--;
        }
        else
        {
            Debug.Log("Reload please"); //TODO Adding UI
        }
    }

    // OpenFire system "quite primitive" I could make a seprate controller , but it's not necessary at our case here
    protected virtual void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            if (!isFiring)
            {
                InvokeRepeating("Fire", 0, fireRate);
                isFiring = true;
                openFireParticles.Play();
            }
        }
        else
        {
            CancelInvoke("Fire");
            isFiring = false;
            openFireParticles.Stop();
        }
        return;
#endif

#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            if (!isFiring)
            {
                InvokeRepeating("Fire", 0, fireRate);
                isFiring = true;
            }
        }
        else
        {
            CancelInvoke("Fire");
            isFiring = false;
        }
#endif
    }

    // Reloading //TODO UI Button
    public void Reload()
    {
        ammo = ammoClipSize;
    }

    //TODO Upgrading Weapon UI
    public virtual void UpgradeWeapon()
    {
        WeaponLevel++;
      
        switch (WeaponLevel)
        {
            case Levels.Level_01:
            {
                weaponMat.SetColor("_ColorEmissive", Color.red);
                break;
            }

            case Levels.Level_02:
            {
                weaponMat.SetColor("_ColorEmissive", Color.blue);
                break;
            }
            case Levels.Level_03:
            {
                weaponMat.SetColor("_ColorEmissive", Color.cyan);
                break;
            }
            case Levels.Level_04:
            {
                weaponMat.SetColor("_ColorEmissive", Color.yellow);
                break;
            }
            case Levels.Level_05:
            {
                weaponMat.SetColor("_ColorEmissive", Color.magenta);
                break;
            }
        }
    }
}