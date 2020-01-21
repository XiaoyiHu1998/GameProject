using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
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
        InventoryStats.Inventory[(int)lootedObject] += amount;
        if (InventoryStats.Inventory[(int)lootedObject] > InventoryStats.InventoryCaps[(int)lootedObject])
            InventoryStats.Inventory[(int)lootedObject] = InventoryStats.InventoryCaps[(int)lootedObject];
        InventoryStats.AmmoCounters[(int)lootedObject].text = "ammo: " + InventoryStats.Inventory[(int)lootedObject].ToString();
    }

    public void lootHealth(int amount)
    {
        PlayerStats.playerHealth += amount;
    }

    public void lootMoney(int amount)
    {
        InventoryStats.Money += amount;
    }

    public void buyWeapon(Weapon weapon)
    {
        if (InventoryStats.Money >= ShopStats.WeaponPrices[(int)weapon] && !InventoryStats.WeaponAcquired[(int)weapon])
        {
            InventoryStats.Money -= ShopStats.WeaponPrices[(int)weapon];
            InventoryStats.WeaponAcquired[(int)weapon] = true;
            lootObject(weapon, InventoryStats.AmmoQuantity[(int)weapon]);
            InventoryStats.AmmoCounters[(int)weapon].text = "ammo: " + InventoryStats.Inventory[(int)weapon].ToString();
        }

        else if (InventoryStats.Money >= ShopStats.AmmoPrices[(int)weapon] && InventoryStats.Inventory[(int)weapon] < InventoryStats.InventoryCaps[(int)weapon] && InventoryStats.InventoryCaps[(int)weapon] > 1)
        {
            InventoryStats.Money -= ShopStats.AmmoPrices[(int)weapon];
            lootObject(weapon, InventoryStats.AmmoQuantity[(int)weapon]);
        }
    }
}
