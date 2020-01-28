﻿using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public GameObject InventoryCursor;
    public Text[] AmmoCounters = new Text[Enum.GetNames(typeof(Weapon)).Length];
    public Text MoneyCounter;

    public HealthScript healthScript;

    void Start()
    {
        for (int i = 0; i < Enum.GetNames(typeof(Weapon)).Length; i++)
            AmmoCounters[i].text = "ammo: " + InventoryStats.Inventory[i].ToString();
        MoneyCounter.text = "Gold: " + InventoryStats.Money.ToString();

        healthScript = GetComponent<HealthScript>();
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
        InventoryStats.WeaponAcquired[(int)lootedObject] = true;
        InventoryStats.Inventory[(int)lootedObject] += amount;
        if (InventoryStats.Inventory[(int)lootedObject] > InventoryStats.InventoryCaps[(int)lootedObject])
            InventoryStats.Inventory[(int)lootedObject] = InventoryStats.InventoryCaps[(int)lootedObject];
        AmmoCounters[(int)lootedObject].text = "ammo: " + InventoryStats.Inventory[(int)lootedObject].ToString();
    }

    public void lootHealth(int amount)
    {
        PlayerStats.playerHealth += amount;
        healthScript.updateHealthBar();
    }

    public void lootMoney(int amount)
    {
        InventoryStats.Money += amount;
        MoneyCounter.text = "Gold: " + InventoryStats.Money.ToString();
    }

    public void buyWeapon(Weapon weapon)
    {
        if (InventoryStats.Money >= ShopStats.WeaponPrices[(int)weapon] && !InventoryStats.WeaponAcquired[(int)weapon])
        {
            InventoryStats.Money -= ShopStats.WeaponPrices[(int)weapon];
            InventoryStats.WeaponAcquired[(int)weapon] = true;
            lootObject(weapon, InventoryStats.AmmoQuantity[(int)weapon]);
            AmmoCounters[(int)weapon].text = "ammo: " + InventoryStats.Inventory[(int)weapon].ToString();
        }

        else if (InventoryStats.Money >= ShopStats.AmmoPrices[(int)weapon] && InventoryStats.Inventory[(int)weapon] < InventoryStats.InventoryCaps[(int)weapon] && InventoryStats.InventoryCaps[(int)weapon] > 1)
        {
            InventoryStats.Money -= ShopStats.AmmoPrices[(int)weapon];
            lootObject(weapon, InventoryStats.AmmoQuantity[(int)weapon]);
        }
        MoneyCounter.text = "Gold: " + InventoryStats.Money.ToString();
    }
}