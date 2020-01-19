using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class readStatic : MonoBehaviour
{
    public GameObject InventoryCursor;

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
    // Update is called once per frame
    void Update()
    {
        InventoryCursor = InventoryStats.InventoryCursor;
        Inventory = InventoryStats.Inventory;
        InventoryCaps = InventoryStats.InventoryCaps;
        WeaponAcquired = InventoryStats.WeaponAcquired;
        WeaponPrices = ShopStats.WeaponPrices;
        AmmoPrices = ShopStats.AmmoPrices;
        AmmoQuantity = InventoryStats.AmmoQuantity;
        playerHealth = PlayerStats.playerHealth;
        maxHealth = PlayerStats.maxHealth;
        Money = InventoryStats.Money;
        shopOpen = ShopStats.shopOpen;
}
}
