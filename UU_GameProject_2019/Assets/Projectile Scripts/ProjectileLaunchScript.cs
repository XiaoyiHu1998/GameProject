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

    public string attackButton; //een hoop van dit is public om het in de unity inspector zichtbaar te maken, zet op private voor final build
    public string switchButton;
    
    public Vector3 BombForce; //X is forward motion, X upward and Z can correct for emitters being attached the wrong way around
    public Vector3 ArrowForce;

    Weapon currentWeapon;

    //Stefan en Xiao Yi kunnen hier hun variabelen plaatsen voor boomerang en sword code

    public int[] placeholderInventory; //placeholder tot Nout's code klaar is
    public int[] placeholderInventoryCaps;
    public bool[] placeholderWeaponAcquired;

    int placeholderHealth; //placeholder tot Wietse's code klaar is

    void Start()
    {
        placeholderInventory = new int[Enum.GetNames(typeof(Weapon)).Length];

        placeholderInventoryCaps = new int[Enum.GetNames(typeof(Weapon)).Length];

        placeholderInventoryCaps[(int)Weapon.Bow] = 50; //staat tijdelijk hier, later ergens een mooi script met constants zetten
        placeholderInventoryCaps[(int)Weapon.Bombs] = 8;
        placeholderInventoryCaps[(int)Weapon.Boomerang] = 1;
        placeholderInventoryCaps[(int)Weapon.Sword] = 1;

        placeholderWeaponAcquired = new bool[Enum.GetNames(typeof(Weapon)).Length];

        placeholderWeaponAcquired[(int)Weapon.Bow] = true; //placeholder tot de shop/andere manieren om items te krijgen klaar zijn
        placeholderWeaponAcquired[(int)Weapon.Bombs] = true;
        placeholderWeaponAcquired[(int)Weapon.Boomerang] = true;
        placeholderWeaponAcquired[(int)Weapon.Sword] = true;
    }

    void Update()
    {
        if(Input.GetKeyDown(attackButton)) //aanvallen
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

        if (Input.GetKeyDown(switchButton)) //van wapen wisselen
        {
            currentWeapon++;
            if (!Enum.IsDefined(typeof(Weapon), currentWeapon)) //loop terug naar 0 als de index OOB gaat
                currentWeapon = (Weapon)0;
        }
    }

    void OnCollisionEnter(Collision target) //health en ammo drops oppakken
    {
        ItemDropScript lootDrop = target.gameObject.GetComponent<ItemDropScript>();

        if (lootDrop != null)
        {
            lootObject(lootDrop.weapon, lootDrop.amount);
            Destroy(lootDrop.gameObject);
            return; //als de pickup een ammo item was kan het geen health zijn en is onderstaande if statement toch altijd false
        }

        HealthDropScript healthDrop = target.gameObject.GetComponent<HealthDropScript>();

        if (healthDrop != null)
        {
            lootHealth(healthDrop.amount);
            Destroy(healthDrop.gameObject);
        }
    }

    public void lootObject(Weapon lootedObject, int amount) //placeholder voor code van Nout
    {
        placeholderInventory[(int)lootedObject] += amount;
        if (placeholderInventory[(int)lootedObject] > placeholderInventoryCaps[(int)lootedObject])
            placeholderInventory[(int)lootedObject] = placeholderInventoryCaps[(int)lootedObject];
    }

    public void lootHealth(int amount) //placeholder voor code van Wietse
    {
        placeholderHealth += amount;
    }

    void UseBow() //schiet een pijl
    {
        if (placeholderInventory[(int)Weapon.Bow] > 0 && placeholderWeaponAcquired[(int)Weapon.Bow])
        {
            placeholderInventory[(int)Weapon.Bow]--;
            GameObject MyArrow = Instantiate(BowObject, ProjectileEmitter.transform.position, ProjectileEmitter.transform.rotation) as GameObject;
            MyArrow.GetComponent<Rigidbody>().AddRelativeForce(ArrowForce);
        }

    }

    void UseBombs() //gooi een bom
    {
        if(placeholderInventory[(int)Weapon.Bombs] > 0 && placeholderWeaponAcquired[(int)Weapon.Bombs])
        {
            placeholderInventory[(int)Weapon.Bombs]--;
            GameObject MyBomb = Instantiate(BombObject, ProjectileEmitter.transform.position, ProjectileEmitter.transform.rotation) as GameObject;
            MyBomb.GetComponent<Rigidbody>().AddRelativeForce(BombForce);
        }
    }

    void UseBoomerang() //gooi met de boomerang
    {
        //Stefan's code
    }

    void UseSword() //zwaai met het zwaard
    {
        //Xiao Yi's code
    }
}