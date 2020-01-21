using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOnActive : MonoBehaviour
{
    public Vector3 playerPosition;
    public Vector3 playerRotation;

    public void OnEnable()
    {
        PlayerStats.playerPosition = playerPosition;
        PlayerStats.playerRotation = playerRotation;
        InitializeGame();
        SceneManager.LoadScene("SpawnScene");
    }

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

    // Runs this method after the game has been started for the first time
    public void InitializeGame()
    {
        // inventory
        // disable all weapons when the game starts
        Inventory = new int[Enum.GetNames(typeof(Weapon)).Length];
        Inventory[(int)Weapon.Bow] = 0;
        Inventory[(int)Weapon.Bombs] = 0;
        Inventory[(int)Weapon.Boomerang] = 0;
        Inventory[(int)Weapon.Sword] = 0;
        InventoryStats.Inventory = Inventory;

        // set max values for inventory items
        InventoryCaps = new int[Enum.GetNames(typeof(Weapon)).Length];
        InventoryCaps[(int)Weapon.Bow] = 50;
        InventoryCaps[(int)Weapon.Bombs] = 8;
        InventoryCaps[(int)Weapon.Boomerang] = 1;
        InventoryCaps[(int)Weapon.Sword] = 1;
        InventoryStats.InventoryCaps = InventoryCaps;

        // disable all weapons when the game starts
        WeaponAcquired = new bool[Enum.GetNames(typeof(Weapon)).Length];
        WeaponAcquired[(int)Weapon.Bow] = false; //unlock all weapons for testing
        WeaponAcquired[(int)Weapon.Bombs] = false;
        WeaponAcquired[(int)Weapon.Boomerang] = false;
        WeaponAcquired[(int)Weapon.Sword] = false;
        InventoryStats.WeaponAcquired = WeaponAcquired;

        // shop
        // set prices of the weapons for shop
        WeaponPrices = new int[Enum.GetNames(typeof(Weapon)).Length];
        WeaponPrices[(int)Weapon.Bow] = 100;
        WeaponPrices[(int)Weapon.Bombs] = 50;
        WeaponPrices[(int)Weapon.Boomerang] = 250;
        WeaponPrices[(int)Weapon.Sword] = 0;
        ShopStats.WeaponPrices = WeaponPrices;

        // set prices of ammo for the shop
        AmmoPrices = new int[Enum.GetNames(typeof(Weapon)).Length];
        AmmoPrices[(int)Weapon.Bow] = 10;
        AmmoPrices[(int)Weapon.Bombs] = 50;
        AmmoPrices[(int)Weapon.Boomerang] = 0;
        AmmoPrices[(int)Weapon.Sword] = 0;
        ShopStats.AmmoPrices = AmmoPrices;

        // set ammo quanity that can be bought from the store
        AmmoQuantity = new int[Enum.GetNames(typeof(Weapon)).Length];
        AmmoQuantity[(int)Weapon.Bow] = 20;
        AmmoQuantity[(int)Weapon.Bombs] = 8;
        AmmoQuantity[(int)Weapon.Boomerang] = 1;
        AmmoQuantity[(int)Weapon.Sword] = 1;
        InventoryStats.AmmoQuantity = AmmoQuantity;

        // the shop can't be accessed 
        ShopStats.shopOpen = false;
    }
}