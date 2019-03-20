using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

////////////////////////////////////////////
/// Weapon Parent Class
/// ////////////////////////////////////////

public class Weapon : MonoBehaviour
{
    //Weapon Name
    protected string weaponName;
    //Damage 
    protected int damage;
    //Accuracy
    protected int accuracy; //Not used but added
    // Recoil
    protected int recoil; //Not used but added
    // Fire Rate
    protected float fireRate;
    // Reloading Speed
    protected float reloadingSpeed;
    // Ammo Clip Size
    public int ammoClipSize;
    // Ammo
    public int ammo;
    // Checking if the weapon is firing or not
    protected bool isFiring;
    // Checking if the weapon is reloading or not
    private bool isReloading;
    // weapon upgrade cost
    protected int UpgradeCost; //Not used but added
   
    //Weapon material which will change its emission color on each upgrade level
    private Material weaponMat;

    // Timer variables to start and reset the timer for reloading
    private float startTime;
    private float timer;

    //Ray for firing , is global to avoid CG
    private Ray ray;
    // Raycast hit result of the weapon
    private RaycastHit hit;
    // PlayerHUD reference to update the ammo amount 
    private PlayerHUDController playerHUD;
    //layer mask for ray cast to hi only enemies
    private int layer_mask;
    
    // primitive particles for open fire .
    [SerializeField] private ParticleSystem openFireParticles;
    // Reloading Fill image
    [SerializeField] private Image reloadingFill;
    // Reloading cavas which will be rendered once the weapon is reloading.
    [SerializeField] private Canvas reloadingCanvas;


    // enum power level of the weapon. 
    public enum Levels
    {
        Level_01 = 1,
        Level_02 = 2,
        Level_03 = 3,
        Level_04 = 4,
        Level_05 = 5
    }

    protected Levels WeaponLevel;


    // Here we assign the layer mask to enemylayer , and getting the weapon material ,
    // then invoking upgrading weapon so it level up every 30 sec "its hardcoded"
    protected virtual void Awake()
    {
        layer_mask = LayerMask.GetMask("EnemyLayer");
        weaponMat = gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material;
        
      
        InvokeRepeating(nameof(UpgradeWeapon),30,30);
    }

    // Getting PlayerHUD
    private void Start()
    {
        playerHUD = GameObject.FindWithTag("HUD").GetComponent<PlayerHUDController>();
        playerHUD.UpdatePlayerHUD();
    }

    // Virtual just if at any case we want to override thie per weapon.
    protected virtual void Fire()
    {
        if (ammo > 0)
        {
            if(!openFireParticles.isPlaying)
                    openFireParticles.Play();
            //fire ray from center of the screen
            ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            Debug.DrawRay(ray.origin, ray.direction, Color.green, 5);

            if (Physics.Raycast(ray, out hit, 20, layer_mask))
            {
                Debug.Log(hit.collider.gameObject.name);
                hit.collider.gameObject.GetComponent<Enemy>().ReceiveDamage(damage);
                playerHUD.playerHUD.CrossHair.color = Color.red;
            }
            else
            {
                playerHUD.playerHUD.CrossHair.color = Color.white;
            }

            ammo--;
            playerHUD.UpdatePlayerHUD();
        }
        else
        {
            if (!isReloading)
            {
                Debug.Log("Reload please"); //TODO Adding UI
                openFireParticles.Stop();
                StartCoroutine(Reload());
            }
        }
    }

    // OpenFire system "quite primitive" I could make a separate controller , but it's not necessary at our case here
    protected virtual void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            if (!isFiring && ammo >0 )
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
            playerHUD.playerHUD.CrossHair.color = Color.white;
        }

        if (isReloading)
        {
            FillReload();
        }

        return;
#endif

        // to run on android VR, but I couldn't test it since I've no VR on my end.
#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            if (!isFiring && ammo>0)
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
            playerHUD.playerHUD.CrossHair.color = Color.white;
        }
        if (isReloading)
        {
            FillReload();
        }

#endif
    }

    // Reloading Automatically once the ammo are out.
    
    IEnumerator Reload()
    {
        FillReload();
        isReloading = true;
        reloadingCanvas.enabled = true;
        yield return new WaitForSeconds(reloadingSpeed);
        reloadingCanvas.enabled = false;
        ammo = ammoClipSize;
        playerHUD.UpdatePlayerHUD();
        isReloading = false;
    }

    // Fill animation while reloading, I could use DOTween here, Image.DOFill.
    private void FillReload()
    {
        if(!isReloading)
            startTime = Time.time;
        
        timer = Time.time - startTime;
        reloadingFill.fillAmount = timer / reloadingSpeed;
        if (reloadingFill.fillAmount > 1)
            startTime = Time.time;
    }

    //Automatic Upgrade every 30 sec. 
    protected virtual void UpgradeWeapon()
    {
        if(isReloading)
            return;
        
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
                CancelInvoke(nameof(UpgradeWeapon));
                break;
            }
        }
    }
}