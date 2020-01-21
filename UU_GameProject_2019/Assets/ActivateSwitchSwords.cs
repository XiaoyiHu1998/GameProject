using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSwitchSwords : MonoBehaviour
{
    public GameObject sceneSword;

    public void OnEnable()
    {
        sceneSword.gameObject.GetComponent<Renderer>().enabled = false;
        bool[] temp = new bool[InventoryStats.WeaponAcquired.Length];
        temp = InventoryStats.WeaponAcquired;
        temp[(int)Weapon.Sword] = true;
        InventoryStats.WeaponAcquired = temp;
        ShopStats.shopOpen = true;
    }

}
