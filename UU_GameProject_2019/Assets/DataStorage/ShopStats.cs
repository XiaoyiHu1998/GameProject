using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class ShopStats
{
    public static int[] WeaponPrices;
    public static int[] AmmoPrices;
    public static bool shopOpen;

    static ShopStats()
    {
        WeaponPrices = new int[Enum.GetNames(typeof(Weapon)).Length];

                WeaponPrices[(int)Weapon.Bow] = 100;
                WeaponPrices[(int)Weapon.Bombs] = 50;
                WeaponPrices[(int)Weapon.Boomerang] = 250;
                WeaponPrices[(int)Weapon.Sword] = 0;

                AmmoPrices = new int[Enum.GetNames(typeof(Weapon)).Length];

                AmmoPrices[(int)Weapon.Bow] = 10;
                AmmoPrices[(int)Weapon.Bombs] = 20;
                AmmoPrices[(int)Weapon.Boomerang] = 0;
                AmmoPrices[(int)Weapon.Sword] = 0;
    }
}
