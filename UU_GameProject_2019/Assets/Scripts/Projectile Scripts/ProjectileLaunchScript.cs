using UnityEngine;
using System.Collections;
using System;

public class ProjectileLaunchScript : MonoBehaviour
{
    public PlayerState playerState;
    public GameObject InventoryCursor;

    public GameObject ProjectileEmitter;
    public GameObject BowObject;
    public GameObject BombObject;
    public GameObject BoomerangObject;
    public GameObject SwordObject;
    public GameObject BeamObject;

    public string attackButton; //some of these are public so they can be seen/edited in the Unity GUI, will be private in the final build
    public string switchButton;

    public Vector3 BombForce; //X is forward, X is upward and Z is sideways(legacy boomerang implementation, here in case a problem crops up with the current system and we need to revert it)
    public Vector3 ArrowForce;

    public Weapon currentWeapon;

    //Stefan and Xiao Yi can place their global variables for boomerang and sword code here

    //Stefan:
    public float BoomerangTravelDistance = 8;

    //Xiao Yi:



    public int[] placeholderInventory; //placeholder for Nout's inventory system
    public int[] placeholderInventoryCaps;
    public bool[] placeholderWeaponAcquired;

    public int placeholderHealth; //placeholder for Wietse's health system
    public int placeholderMaxHealth;

    void Start()
    {
        placeholderInventory = new int[Enum.GetNames(typeof(Weapon)).Length];

        placeholderInventory[(int)Weapon.Bow] = 50; //give ourself all weapons for testing
        placeholderInventory[(int)Weapon.Bombs] = 8;
        placeholderInventory[(int)Weapon.Boomerang] = 1;
        placeholderInventory[(int)Weapon.Sword] = 1;

        placeholderInventoryCaps = new int[Enum.GetNames(typeof(Weapon)).Length];

        placeholderInventoryCaps[(int)Weapon.Bow] = 50; //temporary place until finalized, to be moved to a script containing all constants for final build
        placeholderInventoryCaps[(int)Weapon.Bombs] = 8;
        placeholderInventoryCaps[(int)Weapon.Boomerang] = 1;
        placeholderInventoryCaps[(int)Weapon.Sword] = 1;

        placeholderWeaponAcquired = new bool[Enum.GetNames(typeof(Weapon)).Length];

        placeholderWeaponAcquired[(int)Weapon.Bow] = true; //unlock all weapons for testing
        placeholderWeaponAcquired[(int)Weapon.Bombs] = true;
        placeholderWeaponAcquired[(int)Weapon.Boomerang] = true;
        placeholderWeaponAcquired[(int)Weapon.Sword] = true;

        SwordObject.gameObject.GetComponent<SwordSwing>().SetOwner(this); //the sword object always exists, so it only needs to receive an owner once, as opposed to the boomerang which is reinstantiated with every throw for physics efficiency purposes
    }

    void Update()
    {
        if (Input.GetKeyDown(attackButton)) //attack action with the currently selected weapon
        {
            if (placeholderInventory[(int)currentWeapon] > 0 && placeholderWeaponAcquired[(int)currentWeapon])
            {
                placeholderInventory[(int)currentWeapon]--;
                switch (currentWeapon)
                {
                    case Weapon.Bow: UseBow(); break;
                    case Weapon.Bombs: UseBombs(); break;
                    case Weapon.Boomerang: UseBoomerang(); break;
                    case Weapon.Sword: UseSword(); break;
                }
            }
        }

        if (Input.GetKeyDown(switchButton)) //switch weapon selection
        {
            currentWeapon++;
            if (!Enum.IsDefined(typeof(Weapon), currentWeapon)) //loop back to 0 when the selection goes OOB
                currentWeapon = (Weapon)0;
            InventoryCursor.transform.localPosition = new Vector3(0, 300 - 100 * (int)currentWeapon, 0);
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
        print("looted a thing!");
        placeholderInventory[(int)lootedObject] += amount;
        if (placeholderInventory[(int)lootedObject] > placeholderInventoryCaps[(int)lootedObject])
            placeholderInventory[(int)lootedObject] = placeholderInventoryCaps[(int)lootedObject];
    }

    public void lootHealth(int amount) //placeholder for Wietse's health system
    {
        placeholderHealth += amount;
    }

    void UseBow() //shooting an arrows
    {
        GameObject MyArrow = Instantiate(BowObject, ProjectileEmitter.transform.position, ProjectileEmitter.transform.rotation) as GameObject;
        MyArrow.GetComponent<Rigidbody>().AddRelativeForce(ArrowForce);
        MyArrow.gameObject.GetComponent<ArrowScript>().SetOwner(this);
    }

    void UseBombs() //throwing a bomb
    {
        GameObject MyBomb = Instantiate(BombObject, ProjectileEmitter.transform.position, ProjectileEmitter.transform.rotation) as GameObject;
        MyBomb.GetComponent<Rigidbody>().AddRelativeForce(BombForce);
    }

    void UseBoomerang() //throwing the boomerang
    {
        GameObject MyBoomerang = Instantiate(BoomerangObject, ProjectileEmitter.transform.position, ProjectileEmitter.transform.rotation) as GameObject;
        MyBoomerang.transform.rotation = BoomerangObject.transform.rotation;
        MyBoomerang.gameObject.GetComponent<BoomerangScript>().SetDestination(ProjectileEmitter.transform.position + (transform.forward * BoomerangTravelDistance));
        MyBoomerang.gameObject.GetComponent<BoomerangScript>().SetOwner(this);
    }

    void UseSword() //swinging the sword
    {
        SwordObject.gameObject.GetComponent<SwordSwing>().StartSwing();
        if (placeholderHealth == placeholderMaxHealth)
        {
            GameObject MyBeam = Instantiate(BeamObject, ProjectileEmitter.transform.position, ProjectileEmitter.transform.rotation) as GameObject;
            MyBeam.gameObject.GetComponent<BeamScript>().SetOwner(this);
        }
    }
}