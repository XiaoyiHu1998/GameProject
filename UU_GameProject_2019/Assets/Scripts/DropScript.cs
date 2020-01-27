using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropScript : MonoBehaviour
{
    public GameObject DroppedHealth;
    public GameObject DroppedMoney;
    public GameObject DroppedWeapon;
    public int healthAmount;
    public int moneyAmount;
    public int weaponAmount;
    public Weapon weaponType;

    public void DropHealth()
    {
        GameObject healthDrop = Instantiate(DroppedHealth, transform.position, transform.rotation) as GameObject;
        healthDrop.amount = healthAmount;
    }

    public void DropMoney()
    {
        GameObject moneyDrop = Instantiate(DroppedMoney, transform.position, transform.rotation) as GameObject;
        moneyDrop.amount = moneyAmount;
    }

    public void DropWeapon()
    {
        GameObject weaponDrop = Instantiate(DroppedWeapon, transform.position, transform.rotation) as GameObject;
        weaponDrop.amount = weaponAmount;
        weaponDrop.weapon = weaponType;
    }
}