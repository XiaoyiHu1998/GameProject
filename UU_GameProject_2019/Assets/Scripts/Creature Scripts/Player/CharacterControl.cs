using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(CharacterController))]
public class CharacterControl : MonoBehaviour
{
    float speed = 4;
    float rotSpeed = 80;
    float gravity = 8;
    public float unmovableTimer; //The player cannot move while this is bigger than 0

    [SerializeField] float boomerangTravelDistance = 8;
    public Vector3 BombForce;
    public Vector3 ArrowForce;

    //Emitter and Projectiles
    public GameObject ProjectileEmitter;
    public GameObject ArrowObject;
    public GameObject BombObject;
    public GameObject BoomerangObject;
    public GameObject BeamObject;

    //Visual Object in player hand or on player back
    public GameObject SwordObject;
    public GameObject BowObject;
    public GameObject ShieldObject;
    public GameObject BackBow;
    public GameObject BackSword;
    public GameObject BackShield;
    public GameObject ArrowInHand;
    public GameObject BoomerangInHand;
    public GameObject BombInHand;

    public string attackButton;
    public string switchButton;
    public string weaponButton;
    public string shopButton;

    public Weapon currentWeapon;
    public PlayerInventory inv;
    public HealthScript health;

    Vector3 moveDirection = Vector3.zero;
    CharacterController controller;
    Animator anim;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        inv = GetComponent<PlayerInventory>();
        health = GetComponent<HealthScript>();
        currentWeapon = PlayerStats.currentWeapon;

        inv.InventoryCursor.transform.localPosition = new Vector3(0, 300 - 100 * (int)currentWeapon, 0);

        if (!Enum.IsDefined(typeof(Weapon), currentWeapon)) //Weapon becomes sword if currentweapon is not defined
            currentWeapon = (Weapon)3;


        //First set all objects false
        SwordObject.SetActive(false);
        ShieldObject.SetActive(false);
        BowObject.SetActive(false);
        ArrowInHand.SetActive(false);
        BackSword.SetActive(false);
        BackShield.SetActive(true); //reversed
        BackBow.SetActive(false);
        BoomerangInHand.SetActive(false);
        BombInHand.transform.localScale = new Vector3(0, 0, 0);  //  %$@#*  BombInHand.SetActive(false) doesn't work, as wel as with 'true'

        //Secondly set objects on back true
        if (InventoryStats.WeaponAcquired[(int)Weapon.Bow])
            BackBow.SetActive(true);
        if (InventoryStats.WeaponAcquired[(int)Weapon.Sword])
            BackSword.SetActive(true);

        //Thirdly set according to current weapon, hand-obects true, and back-objects false
        if (currentWeapon == Weapon.Sword)
        {
            if (InventoryStats.WeaponAcquired[(int)Weapon.Sword])
            {
                SwordObject.SetActive(true);
                ShieldObject.SetActive(true);
                BackSword.SetActive(false);
                BackShield.SetActive(false);
            }
        }else if (currentWeapon == Weapon.Bow)
        {
            if (InventoryStats.WeaponAcquired[(int)Weapon.Bow])
            {
                BowObject.SetActive(true);
                BackBow.SetActive(false);
                DrawArrow(); //if you have arrows, set ArrowInHand.Active to true
            }
        }else if (currentWeapon == Weapon.Boomerang)
        {
            if (InventoryStats.WeaponAcquired[(int)Weapon.Boomerang])
            {
                BoomerangInHand.SetActive(true);
            }
        }else if (currentWeapon == Weapon.Bombs)
        {
            if (InventoryStats.WeaponAcquired[(int)Weapon.Bombs])
            {
                BombInHand.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
        }
    }

    void Update()
    {
        Movement();
        GetInput();
    }

    void Movement()
    {
        //If on the ground
        if (controller.isGrounded)
        {
            //Get movement input
            Vector2 movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Vector3 moveDirection = (Vector3.forward * movementInput.y + Vector3.right * movementInput.x).normalized;

            //Set movement direction
            if (moveDirection != Vector3.zero)
            {
                anim.SetBool("Running", true);
                if (unmovableTimer <= 0)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), rotSpeed);
                    moveDirection.y -= gravity * Time.deltaTime;
                    controller.Move(moveDirection * speed * Time.deltaTime);
                }
            }
            else
            {
                anim.SetBool("Running", false);
            }
        }
        //Else if airborne
        else if (!controller.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
        }

        unmovableTimer -= Time.deltaTime;
    }

    void GetInput()
    {
        if (controller.isGrounded && unmovableTimer <= 0)
        {
            if (Input.GetKeyDown(attackButton))
            {
                UseSword();
            }
            if (Input.GetKeyDown(weaponButton)) //attack action with the currently selected weapon
            {
                if (InventoryStats.Inventory[(int)currentWeapon] > 0 && InventoryStats.WeaponAcquired[(int)currentWeapon])
                {
                    InventoryStats.Inventory[(int)currentWeapon]--;
                    inv.AmmoCounters[(int)currentWeapon].text = "ammo: " + InventoryStats.Inventory[(int)currentWeapon].ToString();
                    switch (currentWeapon)
                    {
                        case Weapon.Bow: UseBow(); break;
                        case Weapon.Bombs: UseBombs(); break;
                        case Weapon.Boomerang: UseBoomerang(); break;
                        case Weapon.Sword: UseSword(); break;
                    }
                }
            }
        }
        if (Input.GetKeyDown(switchButton)) //switch weapon selection
        {
            anim.SetTrigger("SwitchWeapon");
            unmovableTimer = 0.9f;
        }

        if (Input.GetKeyDown(shopButton)) //attempt to purchase
        {
            if (ShopStats.shopOpen)
            {
                inv.buyWeapon(currentWeapon);
            }
        }
    }


    void UseSword() //Swinging sword animation, triggers ShootBeam()
    {
        anim.SetTrigger("Attack");
        InventoryStats.Inventory[(int)currentWeapon]++;
        unmovableTimer = 1.0f;
    }
    void ShootBeam() //Shoots the hitbox for doing damage with the sword
    {
        GameObject myBeam = Instantiate(BeamObject, ProjectileEmitter.transform.position, ProjectileEmitter.transform.rotation) as GameObject;
        myBeam.gameObject.GetComponent<BeamScript>().SetOwner(this);
        if (health.CurrentHealth == health.MaxHealth) //sword beam if at full health
            myBeam.gameObject.GetComponent<BeamScript>().SetTimer(5f);
        else
        {
            myBeam.gameObject.GetComponent<BeamScript>().SetTimer(0.05f);
            myBeam.gameObject.GetComponent<Renderer>().enabled = false; //short invisible beam only representing melee hit if not
        }
    }


    void UseBow() //shooting arrow animation, triggers LaunchArrow() and then DrawArrow()
    {
        anim.SetTrigger("ShootArrow");
        unmovableTimer = 0.9f;
    }
    void LaunchArrow()
    {
        GameObject MyArrow = Instantiate(ArrowObject, ProjectileEmitter.transform.position, ProjectileEmitter.transform.rotation) as GameObject;
        ArrowInHand.SetActive(false);

        //Change Rotation
        Vector3 rot = MyArrow.transform.rotation.eulerAngles;
        rot = new Vector3(rot.x, rot.y, rot.z + 90);
        MyArrow.transform.rotation = Quaternion.Euler(rot);

        MyArrow.GetComponent<ArrowScript>().SetOwner(inv);
        MyArrow.GetComponent<Rigidbody>().AddRelativeForce(ArrowForce);
        Destroy(MyArrow, 5);
    }
    void DrawArrow()
    {
        if (InventoryStats.Inventory[(int)currentWeapon] > 0)
            ArrowInHand.SetActive(true);
    }


    void UseBombs() //throwing bomb animation, triggers LaunchBomb()
    {
        anim.SetTrigger("ThrowBomb");
        unmovableTimer = 1.0f;
    }
    void LaunchBomb()
    {
        GameObject MyBomb = Instantiate(BombObject, ProjectileEmitter.transform.position, ProjectileEmitter.transform.rotation) as GameObject;
        MyBomb.transform.rotation = BombObject.transform.rotation;
        MyBomb.GetComponent<Rigidbody>().AddRelativeForce(BombForce);
    }


    void UseBoomerang() //throwing boomerang animation, triggers LaunchBoomerang()
    {
        anim.SetTrigger("ThrowBoomerang");
        unmovableTimer = 0.8f;
    }
    void LaunchBoomerang()
    {
        GameObject MyBoomerang = Instantiate(BoomerangObject, ProjectileEmitter.transform.position, ProjectileEmitter.transform.rotation) as GameObject;
        MyBoomerang.transform.rotation = BoomerangObject.transform.rotation;
        MyBoomerang.gameObject.GetComponent<BoomerangScript>().SetDestination(ProjectileEmitter.transform.position + (transform.forward * boomerangTravelDistance));
        MyBoomerang.gameObject.GetComponent<BoomerangScript>().SetOwner(this);
        BoomerangInHand.SetActive(false);
    }


    //Creates particle effects for animations
    void OnEventFx(GameObject InEffect)
    {
        GameObject newSpell = Instantiate(InEffect, transform.position, transform.rotation) as GameObject;
        newSpell.transform.parent = transform;

        if (newSpell.name == "fx_attack01(Clone)")
        {
            newSpell.transform.localPosition = new Vector3(0, 0.914f, 0.79f);
        }

        Destroy(newSpell, 1.0f);
    }


    void SwitchWeapon()
    {
        if (currentWeapon == Weapon.Sword)
        {
            SwordObject.SetActive(false);
            ShieldObject.SetActive(false);
            if (InventoryStats.WeaponAcquired[(int)Weapon.Sword])
            {
                BackShield.SetActive(true);
                BackSword.SetActive(true);
            }
        } 
        else if (currentWeapon == Weapon.Bow)
        {
            BowObject.SetActive(false);
            ArrowInHand.SetActive(false);
            if (InventoryStats.WeaponAcquired[(int)Weapon.Bow])
                BackBow.SetActive(true);
        } 
        else if (currentWeapon == Weapon.Boomerang)
        {
            BoomerangInHand.SetActive(false);
        } 
        else if (currentWeapon == Weapon.Bombs)
        {
            BombInHand.transform.localScale = new Vector3(0, 0, 0);
        }

        currentWeapon++;
        if (!Enum.IsDefined(typeof(Weapon), currentWeapon)) //loop back to 0 when the selection goes OOB
            currentWeapon = (Weapon)0;
        while (!InventoryStats.WeaponAcquired[(int)currentWeapon]) //while weapon is unacquired, go to next weapon
        {
            currentWeapon++;
            if ((int)currentWeapon >= 4) //Prevents infinite loop
                break;
        }
        PlayerStats.currentWeapon = currentWeapon;
        inv.InventoryCursor.transform.localPosition = new Vector3(0, 300 - 100 * (int)currentWeapon, 0);

        if (currentWeapon == Weapon.Sword)
        {
            SwordObject.SetActive(true);
            BackSword.SetActive(false);
            ShieldObject.SetActive(true);
            BackShield.SetActive(false);
        }
        else if (currentWeapon == Weapon.Bow)
        {
            BowObject.SetActive(true);
            BackBow.SetActive(false);
            DrawArrow();
        }
        else if (currentWeapon == Weapon.Boomerang)
        {
            BoomerangInHand.SetActive(true);
        }
        else if (currentWeapon == Weapon.Bombs)
        {
            if (InventoryStats.Inventory[(int)currentWeapon] > 0)
            {
                BombInHand.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
        }
    }
}
