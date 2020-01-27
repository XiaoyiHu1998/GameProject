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
        Vector3 test = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        GameObject healthDrop = Instantiate(DroppedHealth, test, transform.rotation) as GameObject;
        healthDrop.GetComponent<HealthDropScript>().amount = healthAmount;
    }

    public void DropMoney()
    {
        GameObject moneyDrop = Instantiate(DroppedMoney, transform.position, transform.rotation) as GameObject;
        moneyDrop.GetComponent<MoneyDropScript>().amount = moneyAmount;
    }

    public void DropWeapon()
    {
        GameObject weaponDrop = Instantiate(DroppedWeapon, transform.position, transform.rotation) as GameObject;
        weaponDrop.GetComponent<ItemDropScript>().amount = weaponAmount;
        weaponDrop.GetComponent<ItemDropScript>().weapon = weaponType;
    }
}