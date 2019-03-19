using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "UI Theme/Player UI")]
public class UITheme : ScriptableObject
{
    [Header("Player CrossHair")]
    public Sprite CrossHair;
   
    [Header("Player State UI")]
    public Sprite HealthBackgroundImage;
    public Sprite HealthFillImage;
    public Sprite ArmorFillImage;

    [Header("Weapon State UI")] 
    public Sprite AmmoBackgroundImage;
    public Sprite RightWeaponAmmo;
    public Sprite LeftWeaponAmmo;

}
