using System.Collections;
using System;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int[] Inventory;
    public int[] InventoryCaps;
    public bool[] WeaponAcquired;

    public int playerHealth;

    void Start()
    {
        Inventory = new int[Enum.GetNames(typeof(Weapon)).Length];

        Inventory[(int)Weapon.Bow] = 50; //give ourself all weapons for testing
        Inventory[(int)Weapon.Bombs] = 8;
        Inventory[(int)Weapon.Boomerang] = 1;
        Inventory[(int)Weapon.Sword] = 1;

        InventoryCaps = new int[Enum.GetNames(typeof(Weapon)).Length];

        InventoryCaps[(int)Weapon.Bow] = 50;
        InventoryCaps[(int)Weapon.Bombs] = 8;
        InventoryCaps[(int)Weapon.Boomerang] = 1;
        InventoryCaps[(int)Weapon.Sword] = 1;

        WeaponAcquired = new bool[Enum.GetNames(typeof(Weapon)).Length];

        WeaponAcquired[(int)Weapon.Bow] = true; //unlock all weapons for testing
        WeaponAcquired[(int)Weapon.Bombs] = true;
        WeaponAcquired[(int)Weapon.Boomerang] = true;
        WeaponAcquired[(int)Weapon.Sword] = true;
    }

    void OnCollisionEnter(Collision target) //picking up health and ammo drops
    {
        ItemDropScript lootDrop = target.gameObject.GetComponent<ItemDropScript>();

        if (lootDrop != null)
        {
            lootObject(lootDrop.weapon, lootDrop.amount);
            Destroy(lootDrop.gameObject);
            return; //if the pickup was ammo, then the below can not be true
        }

        HealthDropScript healthDrop = target.gameObject.GetComponent<HealthDropScript>();

        if (healthDrop != null)
        {
            lootHealth(healthDrop.amount);
            Destroy(healthDrop.gameObject);
        }
    }

    public void lootObject(Weapon lootedObject, int amount)
    {
        Inventory[(int)lootedObject] += amount;
        if (Inventory[(int)lootedObject] > InventoryCaps[(int)lootedObject])
            Inventory[(int)lootedObject] = InventoryCaps[(int)lootedObject];
    }

    public void lootHealth(int amount)
    {
        playerHealth += amount;
    }
}
