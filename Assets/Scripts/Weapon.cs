
using UnityEngine;


public class Weapon : MonoBehaviour
{
    protected string Weaponname { get; set; }
    protected int damage { get; set; }
    protected int accuracy { get; set; }
    protected int recoil { get; set; }
    protected float fireRate { get; set; }
    protected float reloadSpeed { get; set; }
    protected int ammoClipSize { get; set; }
    protected int ammo { get; set; }
    protected bool isFiring;
    protected Material weaponMat;
    protected int UpgradeCost;
    protected ParticleSystem openFireParticles;

    public enum Levels
    {
        Level_01 = 1,
        Level_02 = 2,
        Level_03 = 3,
        Level_04 = 4,
        Level_05 = 5
    }

    protected Levels WeaponLevel;

    private int layer_mask;

    // it can be extended as much as we want, like adding audio as well. attachments ..etc
    private void Awake()
    {
        layer_mask = LayerMask.GetMask("EnemyLayer");
        weaponMat = gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material;

    }

    private Ray ray;
    private RaycastHit hit;

    protected virtual void Fire()
    {
        if (ammo > 0)
        {
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
            Debug.Log("Reload");
        }
    }

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

#endif

/*#if UNITY_ANDROID || UNITY_IOS
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
#endif*/
    }

    public void Reload()
    {
        ammo = ammoClipSize;
    }

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