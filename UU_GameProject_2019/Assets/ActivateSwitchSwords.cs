using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSwitchSwords : MonoBehaviour
{
    public GameObject sceneSword;
    public GameObject playerSword;

    public void OnEnable()
    {
        playerSword.SetActive(true);
        sceneSword.gameObject.GetComponent<Renderer>().enabled = false;
        bool[] temp = new bool[InventoryStats.WeaponAcquired.Length];
        temp = InventoryStats.WeaponAcquired;
        temp[(int)Weapon.Sword] = true;
        InventoryStats.WeaponAcquired = temp;
        ShopStats.shopOpen = true;
        PlayerStats.questMarkerPosition = new Vector3(500, 120, 0);
    }

}
