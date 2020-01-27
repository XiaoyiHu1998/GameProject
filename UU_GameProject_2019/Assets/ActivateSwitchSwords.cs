using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSwitchSwords : MonoBehaviour
{
    public GameObject sceneSword;
    public GameObject playerSword;
    public GameObject player;
    private CharacterControl playerScript;

    public void OnEnable()
    {
        playerSword.SetActive(true);
        sceneSword.gameObject.GetComponent<Renderer>().enabled = false;
        bool[] temp = new bool[InventoryStats.WeaponAcquired.Length];
        temp = InventoryStats.WeaponAcquired;
        temp[(int)Weapon.Sword] = true;
        InventoryStats.WeaponAcquired = temp;
        InventoryStats.Inventory[(int)Weapon.Sword] = 1;
        PlayerStats.currentWeapon = Weapon.Sword;
        playerScript = player.GetComponent<CharacterControl>();
        playerScript.currentWeapon = Weapon.Sword;
        ShopStats.shopOpen = true;
        PlayerStats.questMarkerPosition = new Vector3(500, 120, 0);
    }

}
