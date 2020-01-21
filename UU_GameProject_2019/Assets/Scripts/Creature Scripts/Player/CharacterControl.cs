using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterControl : MonoBehaviour
{
    float speed = 4;
    float rotSpeed = 80;
    float gravity = 8;
    float unmovableTimer;      //The player cannot move while this is bigger than 0

    [SerializeField] float boomerangTravelDistance = 8;
    public Vector3 BombForce;
    public Vector3 ArrowForce;

    public GameObject ProjectileEmitter;
    public GameObject BowObject;
    public GameObject ArrowObject;
    public GameObject BombObject;
    public GameObject BoomerangObject;
    public GameObject SwordObject;
    public GameObject ShieldObject;
    public GameObject BeamObject;

    public GameObject BackBow;
    public GameObject BackSword;
    public GameObject BackShield;
    public GameObject ArrowInHand;

    public string attackButton;
    public string switchButton;
    public string weaponButton;
    public string shopButton;

    private Weapon currentWeapon;
    private bool[] weaponAcquired;
    public PlayerInventory inv;

    Vector3 moveDirection = Vector3.zero;
    CharacterController controller;
    Animator anim;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        inv = GetComponent<PlayerInventory>();

        currentWeapon = playerStats.CurrentWeapon;
        weaponAcquired = new bool[InventoryStats.WeaponAcquired.Length];
        weaponAcquired = InventoryStats.WeaponAcquired;

        SwordObject.SetActive(false);
        ShieldObject.SetActive(false);
        BowObject.SetActive(false);
        ArrowInHand.SetActive(false);
        BackSword.SetActive(false);
        BackShield.SetActive(false);
        BackBow.SetActive(false);

        if (!Enum.IsDefined(typeof(Weapon), currentWeapon)) //loop back to 0 when the selection goes OOB
            currentWeapon = (Weapon)0;

        if (currentWeapon == Weapon.Sword)
        {
            SwordObject.SetActive(true);
            ShieldObject.SetActive(true);
            if (weaponAcquired[(int)currentWeapon]) 
                BackBow.SetActive(true);
        }
        else if (currentWeapon == Weapon.Bow)
        {
            BackSword.SetActive(true);
            BackShield.SetActive(true);
            BowObject.SetActive(true);
            ArrowInHand.SetActive(true);
        }
    }

    void Update()
    {
        if (inv.WeaponAcquired[(int)Weapon.Sword]) { SwordObject.gameObject.GetComponent<Renderer>().enabled = true; }
        else { SwordObject.gameObject.GetComponent<Renderer>().enabled = false; }

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
                if (inv.Inventory[(int)currentWeapon] > 0 && inv.WeaponAcquired[(int)currentWeapon])
                {
                    inv.Inventory[(int)currentWeapon]--;
                    inv.AmmoCounters[(int)currentWeapon].text = "ammo: " + inv.Inventory[(int)currentWeapon].ToString();
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
            if (inv.shopOpen)
            {
                inv.buyWeapon(currentWeapon);
            }
        }
    }

    void UseSword() //Swinging sword
    {
        anim.SetTrigger("Attack");
        unmovableTimer = 1f;
    }

    void ShootBeam() //Shoots the hitbox for doing damage with the sword
    {
        GameObject myBeam = Instantiate(BeamObject, ProjectileEmitter.transform.position, ProjectileEmitter.transform.rotation) as GameObject;
        myBeam.gameObject.GetComponent<BeamScript>().SetOwner(this);
        if (inv.playerHealth == inv.maxHealth) //sword beam if at full health
            myBeam.gameObject.GetComponent<BeamScript>().SetTimer(5f);
        else
        {
            myBeam.gameObject.GetComponent<BeamScript>().SetTimer(0.05f);
            myBeam.gameObject.GetComponent<Renderer>().enabled = false; //short invisible beam only representing melee hit if not
        }
    }

    void UseBow() //shooting an arrows
    {
        anim.SetTrigger("Shoot");
        unmovableTimer = 0.9f;
    }

    void LaunchArrow()
    {
        GameObject MyArrow = Instantiate(ArrowObject, ProjectileEmitter.transform.position, ProjectileEmitter.transform.rotation) as GameObject;
        
        //Change Rotation
        Vector3 rot = MyArrow.transform.rotation.eulerAngles;
        rot = new Vector3(rot.x, rot.y, rot.z + 90);
        MyArrow.transform.rotation = Quaternion.Euler(rot);

        MyArrow.GetComponent<Rigidbody>().AddRelativeForce(ArrowForce);
        Destroy(MyArrow, 5);
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
        MyBoomerang.gameObject.GetComponent<BoomerangScript>().SetDestination(ProjectileEmitter.transform.position + (transform.forward * boomerangTravelDistance));
        MyBoomerang.gameObject.GetComponent<BoomerangScript>().SetOwner(this);
    }

    //Creates particle effects for animations
    void OnEventFx(GameObject InEffect)
    {
        GameObject newSpell = Instantiate(InEffect);
        newSpell.transform.position = transform.position;
        newSpell.transform.rotation = transform.rotation;
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
            BackSword.SetActive(true);
            ShieldObject.SetActive(false);
            BackShield.SetActive(true);
        } else if (currentWeapon == Weapon.Bow)
        {
            ArrowObject.SetActive(false);
            BackBow.SetActive(true);
            ArrowInHand.SetActive(false);
        } else if (currentWeapon == Weapon.Boomerang)
        {

        } else if (currentWeapon == Weapon.Bombs)
        {

        }

        currentWeapon++;
        if (!Enum.IsDefined(typeof(Weapon), currentWeapon)) //loop back to 0 when the selection goes OOB
            currentWeapon = (Weapon)0;
        while (!weaponAcquired[(int)currentWeapon]) //while weapon is unacquired, go to next weapon
        {
            currentWeapon++;
        }
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
            ArrowObject.SetActive(true);
            BackBow.SetActive(false);
            ArrowInHand.SetActive(true);
        }
        else if (currentWeapon == Weapon.Boomerang)
        {

        }
        else if (currentWeapon == Weapon.Bombs)
        {

        }
    }
}
