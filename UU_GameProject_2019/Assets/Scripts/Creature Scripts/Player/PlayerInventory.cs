using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public GameObject InventoryCursor;
    public Text[] AmmoCounters = new Text[Enum.GetNames(typeof(Weapon)).Length];

    public int[] Inventory;
    public int[] InventoryCaps;
    public bool[] WeaponAcquired;
    public int[] WeaponPrices;
    public int[] AmmoPrices;
    public int[] AmmoQuantity;

    public int playerHealth;
    public int maxHealth;
    public int Money;

    public bool shopOpen;

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

        WeaponPrices = new int[Enum.GetNames(typeof(Weapon)).Length];

        WeaponPrices[(int)Weapon.Bow] = 100;
        WeaponPrices[(int)Weapon.Bombs] = 50;
        WeaponPrices[(int)Weapon.Boomerang] = 250;
        WeaponPrices[(int)Weapon.Sword] = 0;

        AmmoPrices = new int[Enum.GetNames(typeof(Weapon)).Length];

        AmmoPrices[(int)Weapon.Bow] = 10;
        AmmoPrices[(int)Weapon.Bombs] = 50;
        AmmoPrices[(int)Weapon.Boomerang] = 0;
        AmmoPrices[(int)Weapon.Sword] = 0;

        AmmoQuantity = new int[Enum.GetNames(typeof(Weapon)).Length];

        AmmoQuantity[(int)Weapon.Bow] = 20;
        AmmoQuantity[(int)Weapon.Bombs] = 8;
        AmmoQuantity[(int)Weapon.Boomerang] = 1;
        AmmoQuantity[(int)Weapon.Sword] = 1;
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
            return; //if the pickup was health, then the below can not be true
        }

        MoneyDropScript moneyDrop = target.gameObject.GetComponent<MoneyDropScript>();

        if (moneyDrop != null)
        {
            lootMoney(moneyDrop.amount);
            Destroy(moneyDrop.gameObject);
        }
    }

    public void lootObject(Weapon lootedObject, int amount)
    {
        Inventory[(int)lootedObject] += amount;
        if (Inventory[(int)lootedObject] > InventoryCaps[(int)lootedObject])
            Inventory[(int)lootedObject] = InventoryCaps[(int)lootedObject];
        AmmoCounters[(int)lootedObject].text = "ammo: " + Inventory[(int)lootedObject].ToString();
    }

    public void lootHealth(int amount)
    {
        playerHealth += amount;
    }

    public void lootMoney(int amount)
    {
        Money += amount;
    }

    public void buyWeapon(Weapon weapon)
    {
        if (Money >= WeaponPrices[(int)weapon] && !WeaponAcquired[(int)weapon])
        {
            Money -= WeaponPrices[(int)weapon];
            WeaponAcquired[(int)weapon] = true;
            lootObject(weapon, AmmoQuantity[(int)weapon]);
            AmmoCounters[(int)weapon].text = "ammo: " + Inventory[(int)weapon].ToString();
        }

        else if (Money >= AmmoPrices[(int)weapon] && Inventory[(int)weapon] < InventoryCaps[(int)weapon] && InventoryCaps[(int)weapon] > 1)
        {
            Money -= AmmoPrices[(int)weapon];
            lootObject(weapon, AmmoQuantity[(int)weapon]);
        }
    }
}
