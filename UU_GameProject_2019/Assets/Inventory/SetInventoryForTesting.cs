using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SetInventoryForTesting : MonoBehaviour
{
    public  GameObject InventoryCursor;
    public  Text[] AmmoCounters = new Text[Enum.GetNames(typeof(Weapon)).Length];

    public  int[] Inventory;
    public  int[] InventoryCaps;
    public  bool[] WeaponAcquired;
    public  int[] WeaponPrices;
    public  int[] AmmoPrices;
    public  int[] AmmoQuantity;

    public  int playerHealth;
    public  int maxHealth;
    public  int Money;

    public  bool shopOpen;


    // Start is called before the first frame update
    void Start()
    {
        Inventory = new int[Enum.GetNames(typeof(Weapon)).Length];

        Inventory[(int)Weapon.Bow] = 50; //give ourself all weapons for testing
        Inventory[(int)Weapon.Bombs] = 8;
        Inventory[(int)Weapon.Boomerang] = 1;
        Inventory[(int)Weapon.Sword] = 1;

        InventoryStats.Inventory = Inventory;

        InventoryCaps = new int[Enum.GetNames(typeof(Weapon)).Length];

        InventoryCaps[(int)Weapon.Bow] = 50;
        InventoryCaps[(int)Weapon.Bombs] = 8;
        InventoryCaps[(int)Weapon.Boomerang] = 1;
        InventoryCaps[(int)Weapon.Sword] = 1;

        InventoryStats.InventoryCaps = InventoryCaps;

        WeaponAcquired = new bool[Enum.GetNames(typeof(Weapon)).Length];

        WeaponAcquired[(int)Weapon.Bow] = true; //unlock all weapons for testing
        WeaponAcquired[(int)Weapon.Bombs] = true;
        WeaponAcquired[(int)Weapon.Boomerang] = true;
        WeaponAcquired[(int)Weapon.Sword] = true;

        InventoryStats.WeaponAcquired = WeaponAcquired;

        WeaponPrices = new int[Enum.GetNames(typeof(Weapon)).Length];

        WeaponPrices[(int)Weapon.Bow] = 100;
        WeaponPrices[(int)Weapon.Bombs] = 50;
        WeaponPrices[(int)Weapon.Boomerang] = 250;
        WeaponPrices[(int)Weapon.Sword] = 0;

        ShopStats.WeaponPrices = WeaponPrices;

        AmmoPrices = new int[Enum.GetNames(typeof(Weapon)).Length];

        AmmoPrices[(int)Weapon.Bow] = 10;
        AmmoPrices[(int)Weapon.Bombs] = 50;
        AmmoPrices[(int)Weapon.Boomerang] = 0;
        AmmoPrices[(int)Weapon.Sword] = 0;

        ShopStats.AmmoPrices = AmmoPrices;

        AmmoQuantity = new int[Enum.GetNames(typeof(Weapon)).Length];

        AmmoQuantity[(int)Weapon.Bow] = 20;
        AmmoQuantity[(int)Weapon.Bombs] = 8;
        AmmoQuantity[(int)Weapon.Boomerang] = 1;
        AmmoQuantity[(int)Weapon.Sword] = 1;

        InventoryStats.AmmoQuantity = AmmoQuantity;

    }

    private void Update()
    {
        InventoryStats.Inventory = Inventory;
        InventoryStats.InventoryCaps = InventoryCaps;
        InventoryStats.WeaponAcquired = WeaponAcquired;
        ShopStats.WeaponPrices = WeaponPrices;
        ShopStats.AmmoPrices = AmmoPrices;
        InventoryStats.AmmoQuantity = AmmoQuantity;
        
        InventoryStats.InventoryCursor = InventoryCursor;

        PlayerStats.playerHealth = playerHealth;
        PlayerStats.maxHealth = maxHealth;
        InventoryStats.Money = Money;
        ShopStats.shopOpen = shopOpen;

    }
}
