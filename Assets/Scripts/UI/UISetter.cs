using UnityEngine;
using UnityEngine.UI;

public class UISetter : MonoBehaviour
{
    [SerializeField] private UITheme Theme;

    public Image crossHair;
    public Image healthBackgroundImage;
    public Image healthFillImage;
    public Image armorFillImage;

    public Image ammoBackgroundImage;
    public Image rightWeaponAmmo;
    public Image leftWeaponAmmo;

    public Text totalHealthAmount;

    private void Awake()
    {
        crossHair.sprite = Theme.CrossHair;
        healthBackgroundImage.sprite = Theme.HealthBackgroundImage;
        healthFillImage.sprite = Theme.HealthFillImage;
        armorFillImage.sprite = Theme.ArmorFillImage;

        ammoBackgroundImage.sprite = Theme.AmmoBackgroundImage;
        rightWeaponAmmo.sprite = Theme.RightWeaponAmmo;
        leftWeaponAmmo.sprite = Theme.LeftWeaponAmmo;
    }
}