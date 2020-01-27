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
        GameObject healthDrop = Instantiate(DroppedHealth, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation) as GameObject;
        healthDrop.GetComponent<HealthDropScript>().amount = healthAmount;
    }

    public void DropMoney()
    {
        GameObject moneyDrop = Instantiate(DroppedMoney, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation) as GameObject;
        moneyDrop.GetComponent<MoneyDropScript>().amount = moneyAmount;
    }

    public void DropWeapon()
    {
        GameObject weaponDrop = Instantiate(DroppedWeapon, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation) as GameObject;
        weaponDrop.GetComponent<ItemDropScript>().amount = weaponAmount;
        weaponDrop.GetComponent<ItemDropScript>().weapon = weaponType;
    }
}