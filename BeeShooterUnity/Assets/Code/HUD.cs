using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static HUD Instance { get; private set; }
    public Image healthBarFill;
    public Image ammoFill;


    private void Awake()
    {
        Instance = this;
    }
    public void DisplayHealth(int currentArmor, int MaxArmor)
    {
        float healthAmount = (float) currentArmor / (float) MaxArmor;
        healthBarFill.fillAmount = healthAmount;
    }
    public void DisplayAmmo(int currentAmmo, int maxAmmo)
    {
        float ammoAmount = (float)currentAmmo / (float)maxAmmo;
        ammoFill.fillAmount = ammoAmount;
    }


    public void DisplayWave(int currentWave)
    {

    }
}
