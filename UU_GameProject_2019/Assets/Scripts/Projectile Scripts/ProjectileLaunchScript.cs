using UnityEngine;
using System.Collections;
using System;

public class ProjectileLaunchScript : MonoBehaviour
{
    public PlayerState playerState;
    public GameObject InventoryCursor;
    public GameObject ProjectileEmitter;
    public GameObject SwordObject, BowObject, BombObject, BoomerangObject;
    
    //some of these are public so they can be seen/edited in the Unity GUI, will be private in the final build
    public string attackButton; 
    public string switchUpButton, switchDownButton;
    public string SwitchToSwordButton, SwitchToBowButton, SwitchToBombButton, SwitchToBoomerangButton;
    
    public Vector3 BombForce; //X is forward, X is upward and Z is sideways(legacy boomerang implementation, here in case a problem crops up with the current system and we need to revert it)
    public Vector3 ArrowForce;

    Weapon currentWeapon;

    //Stefan and Xiao Yi can place their global variables for boomerang and sword code here

    public int[] placeholderInventory; //placeholder for Nout's inventory system
    public int[] placeholderInventoryCaps;
    public bool[] placeholderWeaponAcquired;

    int placeholderHealth; //placeholder for Wietse's health system

    void Start()
    {

        placeholderInventory = new int[Enum.GetNames(typeof(Weapon)).Length];

        placeholderInventoryCaps = new int[Enum.GetNames(typeof(Weapon)).Length];

        placeholderInventoryCaps[(int)Weapon.Sword] = 1; //temporary place until finalized, to be moved to a script containing all constants for final build
        placeholderInventoryCaps[(int)Weapon.Bow] = 50;
        placeholderInventoryCaps[(int)Weapon.Bombs] = 8;
        placeholderInventoryCaps[(int)Weapon.Boomerang] = 1;

        placeholderWeaponAcquired = new bool[Enum.GetNames(typeof(Weapon)).Length];

        placeholderWeaponAcquired[(int)Weapon.Sword] = playerState.SwordUnlock; //placeholder for the shop
        placeholderWeaponAcquired[(int)Weapon.Bow] = playerState.BowUnlock;
        placeholderWeaponAcquired[(int)Weapon.Bombs] = playerState.BombUnlock;
        placeholderWeaponAcquired[(int)Weapon.Boomerang] = playerState.BoomerangUnlock;
    }

    void Update()
    {
        if(Input.GetKeyDown(attackButton)) //attack action with the currently selected weapon
        {
            switch (currentWeapon)
            {
                case Weapon.Sword:
                    UseSword();
                    break;
                case Weapon.Bow:
                    UseBow();
                    break;
                case Weapon.Bombs:
                    UseBombs();
                    break;
                case Weapon.Boomerang:
                    UseBoomerang();
                    break;
            }
        }

        if (Input.GetKeyDown(switchUpButton) && (int)currentWeapon > 0) //switch weapon selection
            SwitchWeapon(currentWeapon - 1);
        if (Input.GetKeyDown(switchDownButton) && (int)currentWeapon < placeholderInventory.Length-1)
            SwitchWeapon(currentWeapon + 1);

        if (Input.GetKeyDown(SwitchToSwordButton))
            SwitchWeapon(Weapon.Sword);
        if (Input.GetKeyDown(SwitchToBowButton))
            SwitchWeapon(Weapon.Bow);
        if (Input.GetKeyDown(SwitchToBombButton))
            SwitchWeapon(Weapon.Bombs);
        if (Input.GetKeyDown(SwitchToBoomerangButton))
            SwitchWeapon(Weapon.Boomerang);
    }

    void SwitchWeapon(Weapon switchto)
    {
        currentWeapon = switchto;
        playerState.InvetorySelection = (int)switchto;
        InventoryCursor.transform.localPosition = new Vector3(0, 300 - 100 * (int)switchto, 0);
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
        }
    }

    public void lootObject(Weapon lootedObject, int amount) //placeholder for Nout's inventory system
    {
        placeholderInventory[(int)lootedObject] += amount;
        if (placeholderInventory[(int)lootedObject] > placeholderInventoryCaps[(int)lootedObject])
            placeholderInventory[(int)lootedObject] = placeholderInventoryCaps[(int)lootedObject];
    }

    public void lootHealth(int amount) //placeholder for Wietse's health system
    {
        placeholderHealth += amount;
    }

    void UseSword() //swinging the sword
    {
        //Xiao Yi's code
    }

    void UseBow() //shooting an arrow
    {
        if (placeholderInventory[(int)Weapon.Bow] > 0 && placeholderWeaponAcquired[(int)Weapon.Bow])
        {
            placeholderInventory[(int)Weapon.Bow]--;
            GameObject MyArrow = Instantiate(BowObject, ProjectileEmitter.transform.position, ProjectileEmitter.transform.rotation) as GameObject;
            MyArrow.GetComponent<Rigidbody>().AddRelativeForce(ArrowForce);
        }

    }

    void UseBombs() //throwing a bomb
    {
        if(placeholderInventory[(int)Weapon.Bombs] > 0 && placeholderWeaponAcquired[(int)Weapon.Bombs])
        {
            placeholderInventory[(int)Weapon.Bombs]--;
            GameObject MyBomb = Instantiate(BombObject, ProjectileEmitter.transform.position, ProjectileEmitter.transform.rotation) as GameObject;
            MyBomb.GetComponent<Rigidbody>().AddRelativeForce(BombForce);
        }
    }

    void UseBoomerang() //throwing the boomerang
    {
        //Stefan's code
    }
}