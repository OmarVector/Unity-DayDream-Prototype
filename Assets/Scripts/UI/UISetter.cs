using UnityEngine;
using UnityEngine.UI;

public class UISetter : MonoBehaviour
{
    [SerializeField] private UITheme Theme;

    public Image CrossHair;
    public Image HealthBackgroundImage;
    public Image HealthFillImage;
    public Image ArmorFillImage;

    public Image AmmoBackgroundImage;
    public Image RightWeaponAmmo;
    public Image LeftWeaponAmmo;

    public Text TotalHealthAmount;
    public Text Score;

    private void Awake()
    {
        CrossHair.sprite = Theme.CrossHair;
        HealthBackgroundImage.sprite = Theme.HealthBackgroundImage;
        HealthFillImage.sprite = Theme.HealthFillImage;
        ArmorFillImage.sprite = Theme.ArmorFillImage;

        AmmoBackgroundImage.sprite = Theme.AmmoBackgroundImage;
        RightWeaponAmmo.sprite = Theme.RightWeaponAmmo;
        LeftWeaponAmmo.sprite = Theme.LeftWeaponAmmo;
    }
}