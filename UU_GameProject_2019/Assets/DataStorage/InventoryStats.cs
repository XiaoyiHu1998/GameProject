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

    public static int Money;



}
