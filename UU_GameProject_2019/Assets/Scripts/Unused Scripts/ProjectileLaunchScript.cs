using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ProjectileLaunchScript : MonoBehaviour
{
    public GameObject InventoryCursor;

    public Text[] AmmoCounters = new Text[Enum.GetNames(typeof(Weapon)).Length];

    public GameObject ProjectileEmitter;
    public GameObject BowObject;
    public GameObject BombObject;
    public GameObject BoomerangObject;
    public GameObject SwordObject;

    public GameObject BeamObject;

    public string attackButton; //some of these are public so they can be seen/edited in the Unity GUI, will be private in the final build
    public string switchButton;
    public string shopButton;

    public Vector3 BombForce; //X is forward, X is upward and Z is sideways(legacy boomerang implementation, here in case a problem crops up with the current system and we need to revert it)
    public Vector3 ArrowForce;

    public Weapon currentWeapon;

    //Stefan and Xiao Yi can place their global variables for boomerang and sword code here

    //Stefan:
    public float BoomerangTravelDistance = 8;

    public int[] Inventory;
    public int[] InventoryCaps;
    public bool[] WeaponAcquired;
    public int[] WeaponPrices;
    public int[] AmmoPrices;
    public int[] AmmoQuantity;

    public int Health; //placeholder for Wietse's health system
    public int MaxHealth;
    public int Money;

    public bool shopOpen;

    void Start()
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

        WeaponPrices = new int[Enum.GetNames(typeof(Weapon)).Length];

        WeaponPrices[(int)Weapon.Bow] = 100;
        WeaponPrices[(int)Weapon.Bombs] = 50;
        WeaponPrices[(int)Weapon.Boomerang] = 250;
        WeaponPrices[(int)Weapon.Sword] = 0;

        AmmoPrices = new int[Enum.GetNames(typeof(Weapon)).Length];

        AmmoPrices[(int)Weapon.Bow] = 10;
        AmmoPrices[(int)Weapon.Bombs] = 50;
        AmmoPrices[(int)Weapon.Boomerang] = 0;
        AmmoPrices[(int)Weapon.Sword] = 0;

        AmmoQuantity = new int[Enum.GetNames(typeof(Weapon)).Length];

        AmmoQuantity[(int)Weapon.Bow] = 20;
        AmmoQuantity[(int)Weapon.Bombs] = 8;
        AmmoQuantity[(int)Weapon.Boomerang] = 1;
        AmmoQuantity[(int)Weapon.Sword] = 1;

        SwordObject.gameObject.GetComponent<SwordSwing>().SetOwner(this); //the sword object always exists, so it only needs to receive an owner once, as opposed to the boomerang which is reinstantiated with every throw for physics efficiency purposes

        Health = 3;
        MaxHealth = 6;
        Money = 10000;
        shopOpen = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(attackButton)) //attack action with the currently selected weapon
        {
            if (Inventory[(int)currentWeapon] > 0 && WeaponAcquired[(int)currentWeapon])
            {
                Inventory[(int)currentWeapon]--;
                AmmoCounters[(int)currentWeapon].text = "ammo: " + Inventory[(int)currentWeapon].ToString();
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

        if (Input.GetKeyDown(shopButton)) //attempt to purchase
        {
            if (shopOpen)
            {
                buyWeapon(currentWeapon);
            }
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
            return; //if the pickup was health, then the below can not be true
        }

        MoneyDropScript moneyDrop = target.gameObject.GetComponent<MoneyDropScript>();

        if (moneyDrop != null)
        {
            lootMoney(moneyDrop.amount);
            Destroy(moneyDrop.gameObject);
        }
    }

    public void lootObject(Weapon lootedObject, int amount) //placeholder for Nout's inventory system
    {
        Inventory[(int)lootedObject] += amount;
        if (Inventory[(int)lootedObject] > InventoryCaps[(int)lootedObject])
            Inventory[(int)lootedObject] = InventoryCaps[(int)lootedObject];
        AmmoCounters[(int)lootedObject].text = "ammo: " + Inventory[(int)lootedObject].ToString();
    }

    public void lootHealth(int amount) //placeholder for Wietse's health system
    {
        Health += amount;
    }

    public void lootMoney(int amount) //placeholder for Wietse's health system
    {
        Money += amount;
    }

    void UseBow() //shooting an arrows
    {
        GameObject myArrow = Instantiate(BowObject, ProjectileEmitter.transform.position, ProjectileEmitter.transform.rotation) as GameObject;
        myArrow.GetComponent<Rigidbody>().AddRelativeForce(ArrowForce);
        myArrow.gameObject.GetComponent<ArrowScript>().SetOwner(this);
    }

    void UseBombs() //throwing a bomb
    {
        GameObject myBomb = Instantiate(BombObject, ProjectileEmitter.transform.position, ProjectileEmitter.transform.rotation) as GameObject;
        myBomb.GetComponent<Rigidbody>().AddRelativeForce(BombForce);
    }

    void UseBoomerang() //throwing the boomerang
    {
        GameObject myBoomerang = Instantiate(BoomerangObject, ProjectileEmitter.transform.position, ProjectileEmitter.transform.rotation) as GameObject;
        myBoomerang.transform.rotation = BoomerangObject.transform.rotation;
        myBoomerang.gameObject.GetComponent<BoomerangScript>().SetDestination(ProjectileEmitter.transform.position + (transform.forward * BoomerangTravelDistance));
        //myBoomerang.gameObject.GetComponent<BoomerangScript>().SetOwner(this);
    }

    void UseSword() //swinging the sword
    {
        SwordObject.gameObject.GetComponent<SwordSwing>().StartSwing(); //sword itself does nothing to prevent doube hits
        GameObject myBeam = Instantiate(BeamObject, ProjectileEmitter.transform.position, ProjectileEmitter.transform.rotation) as GameObject;
        //myBeam.gameObject.GetComponent<BeamScript>().SetOwner(this);

        if (Health == MaxHealth) //sword beam if at full health
            myBeam.gameObject.GetComponent<BeamScript>().SetTimer(5f);
        else
        {
            myBeam.gameObject.GetComponent<BeamScript>().SetTimer(0.05f);
            myBeam.gameObject.GetComponent<Renderer>().enabled = false; //short invisible beam only representing melee hit if not
        }
    }

    void buyWeapon(Weapon weapon)
    {
        if (Money >= WeaponPrices[(int)weapon] && !WeaponAcquired[(int)weapon])
        {
            Money -= WeaponPrices[(int)weapon];
            WeaponAcquired[(int)weapon] = true;
            lootObject(weapon, AmmoQuantity[(int)weapon]);
            AmmoCounters[(int)weapon].text = "ammo: " + Inventory[(int)weapon].ToString();
        }

        else if (Money >= AmmoPrices[(int)weapon] && Inventory[(int)weapon] < InventoryCaps[(int)weapon] && InventoryCaps[(int)weapon] > 1)
        {
            Money -= AmmoPrices[(int)weapon];
            lootObject(weapon, AmmoQuantity[(int)weapon]);
        }
    }
}