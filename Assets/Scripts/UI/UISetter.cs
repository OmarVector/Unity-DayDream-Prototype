using UnityEngine;
using UnityEngine.UI;

public class UISetter : MonoBehaviour
{
   [SerializeField] private UITheme Theme;
  
   [SerializeField] private Image crossHair;
   [SerializeField] private Image healthBackgroundImage;
   [SerializeField] private Image healthFillImage;
   [SerializeField] private Image armorFillImage;
 
   [SerializeField] private Image ammoBackgroundImage;
   [SerializeField] private Image rightWeaponAmmo;
   [SerializeField] private Image leftWeaponAmmo;

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
