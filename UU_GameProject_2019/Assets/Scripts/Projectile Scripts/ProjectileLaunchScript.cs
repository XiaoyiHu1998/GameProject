using UnityEngine;
using System.Collections;
using System;

public class ProjectileLaunchScript : MonoBehaviour
{
    public GameObject ProjectileEmitter;
    public GameObject BowObject;
    public GameObject BombObject;
    public GameObject BoomerangObject;
    public GameObject SwordObject;

    public string attackButton; //some of these are public so they can be seen/edited in the Unity GUI, will be private in the final build
    public string switchButton;
    
    public Vector3 BombForce; //X is forward, X is upward and Z is sideways(legacy boomerang implementation, here in case a problem crops up with the current system and we need to revert it)
    public Vector3 ArrowForce;

    Weapon currentWeapon;

    //Stefan and Xiao Yi can place their global variables for boomerang and sword code here
    //Stefan:
    public static Vector3 Destination;
    public float BoomerangTravelDistance = 8;
    //Xiao Yi:

    public int[] placeholderInventory; //placeholder for Nout's inventory system
    public int[] placeholderInventoryCaps;
    public bool[] placeholderWeaponAcquired;

    int placeholderHealth; //placeholder for Wietse's health system

    void Start()
    {
        placeholderInventory = new int[Enum.GetNames(typeof(Weapon)).Length];

        placeholderInventoryCaps = new int[Enum.GetNames(typeof(Weapon)).Length];

        placeholderInventoryCaps[(int)Weapon.Bow] = 50; //temporary place until finalized, to be moved to a script containing all constants for final build
        placeholderInventoryCaps[(int)Weapon.Bombs] = 8;
        placeholderInventoryCaps[(int)Weapon.Boomerang] = 1;
        placeholderInventoryCaps[(int)Weapon.Sword] = 1;

        placeholderWeaponAcquired = new bool[Enum.GetNames(typeof(Weapon)).Length];

        placeholderWeaponAcquired[(int)Weapon.Bow] = true; //placeholder for the shop
        placeholderWeaponAcquired[(int)Weapon.Bombs] = true;
        placeholderWeaponAcquired[(int)Weapon.Boomerang] = true;
        placeholderWeaponAcquired[(int)Weapon.Sword] = true;
    }

    void Update()
    {
        if(Input.GetKeyDown(attackButton)) //attack action with the currently selected weapon
        {
            switch (currentWeapon)
            {
                case Weapon.Bow:
                    UseBow();
                    break;
                case Weapon.Bombs:
                    UseBombs();
                    break;
                case Weapon.Boomerang:
                    UseBoomerang();
                    break;
                case Weapon.Sword:
                    UseSword();
                    break;
            }
        }

        if (Input.GetKeyDown(switchButton)) //switch weapon selection
        {
            currentWeapon++;
            if (!Enum.IsDefined(typeof(Weapon), currentWeapon)) //loop back to 0 when the selection goes OOB
                currentWeapon = (Weapon)0;
        }
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
        if (placeholderInventory[(int)Weapon.Bombs] > 0 && placeholderWeaponAcquired[(int)Weapon.Bombs])
        {
            placeholderInventory[(int)Weapon.Bombs]--;
            GameObject MyBomb = Instantiate(BombObject, ProjectileEmitter.transform.position, ProjectileEmitter.transform.rotation) as GameObject;
            MyBomb.GetComponent<Rigidbody>().AddRelativeForce(BombForce);
        }
    }

    void UseBoomerang() //throwing the boomerang
    {
        if (placeholderInventory[(int)Weapon.Boomerang] > 0 && placeholderWeaponAcquired[(int)Weapon.Boomerang])
        {
            //Code below is inactive for testing purposes
            //  placeholderInventory[(int)Weapon.Boomerang]--;
            GameObject MyBoomerang = Instantiate(BoomerangObject, ProjectileEmitter.transform.position, ProjectileEmitter.transform.rotation) as GameObject;
            MyBoomerang.transform.rotation = BoomerangObject.transform.rotation;
            Destination = ProjectileEmitter.transform.position + (transform.forward * BoomerangTravelDistance);
            // REMINDER _ add the boomerang to inventory after it gets Destroyed (timer / catching)
        }
    }

    void UseSword() //swinging the sword
    {
        //Xiao Yi's code
    }
}