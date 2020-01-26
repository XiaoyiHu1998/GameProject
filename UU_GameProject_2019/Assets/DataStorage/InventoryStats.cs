using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public static class InventoryStats
{
    public static GameObject InventoryCursor;
    public static Text[] AmmoCounters = new Text[Enum.GetNames(typeof(Weapon)).Length];

    public static int[] Inventory;
    public static int[] InventoryCaps;
    public static bool[] WeaponAcquired;

    public static int[] AmmoQuantity;

    public static int Money = 999999;

    static InventoryStats()
    {
        Inventory = new int[Enum.GetNames(typeof(Weapon)).Length];

        Inventory[(int)Weapon.Bow] = 0;
        Inventory[(int)Weapon.Bombs] = 0;
        Inventory[(int)Weapon.Boomerang] = 0;
        Inventory[(int)Weapon.Sword] = 0;

        InventoryCaps = new int[Enum.GetNames(typeof(Weapon)).Length];

        InventoryCaps[(int)Weapon.Bow] = 50;
        InventoryCaps[(int)Weapon.Bombs] = 8;
        InventoryCaps[(int)Weapon.Boomerang] = 1;
        InventoryCaps[(int)Weapon.Sword] = 1;

        WeaponAcquired = new bool[Enum.GetNames(typeof(Weapon)).Length];

        WeaponAcquired[(int)Weapon.Bow] = false;
        WeaponAcquired[(int)Weapon.Bombs] = false;
        WeaponAcquired[(int)Weapon.Boomerang] = false;
        WeaponAcquired[(int)Weapon.Sword] = false;

        AmmoQuantity = new int[Enum.GetNames(typeof(Weapon)).Length];

        AmmoQuantity[(int)Weapon.Bow] = 20;
        AmmoQuantity[(int)Weapon.Bombs] = 8;
        AmmoQuantity[(int)Weapon.Boomerang] = 1;
        AmmoQuantity[(int)Weapon.Sword] = 1;

    }



}
