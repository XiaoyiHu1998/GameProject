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
    public GameObject BombObject;
    public GameObject BoomerangObject;
    public GameObject SwordObject;
    public GameObject BeamObject;

    public string attackButton;
    public string switchButton;
    public string weaponButton;
    public Weapon currentWeapon;

    public PlayerInventory inv;

    Vector3 moveDirection = Vector3.zero;
    CharacterController controller;
    Animator anim;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        inv = GetComponent<PlayerInventory>();
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
        }
    }

    void UseSword() //Swinging sword
    {
        if (inv.WeaponAcquired[(int)Weapon.Sword])
        {
            anim.SetTrigger("Attack");
            unmovableTimer = 1f;
        }
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
        if (inv.Inventory[(int)Weapon.Bow] > 0 && inv.WeaponAcquired[(int)Weapon.Bow])
        {
            inv.Inventory[(int)Weapon.Bow]--;
            GameObject MyArrow = Instantiate(BowObject, ProjectileEmitter.transform.position, ProjectileEmitter.transform.rotation) as GameObject;
            MyArrow.GetComponent<Rigidbody>().AddRelativeForce(ArrowForce);
        }
    }

    void UseBombs() //throwing a bomb
    {
        if (inv.Inventory[(int)Weapon.Bombs] > 0 && inv.WeaponAcquired[(int)Weapon.Bombs])
        {
            inv.Inventory[(int)Weapon.Bombs]--;
            GameObject MyBomb = Instantiate(BombObject, ProjectileEmitter.transform.position, ProjectileEmitter.transform.rotation) as GameObject;
            MyBomb.GetComponent<Rigidbody>().AddRelativeForce(BombForce);
        }
    }

    void UseBoomerang() //throwing the boomerang
    {
        if (inv.Inventory[(int)Weapon.Boomerang] > 0 && inv.WeaponAcquired[(int)Weapon.Boomerang])
        {
            inv.Inventory[(int)Weapon.Boomerang]--;
            GameObject MyBoomerang = Instantiate(BoomerangObject, ProjectileEmitter.transform.position, ProjectileEmitter.transform.rotation) as GameObject;
            MyBoomerang.transform.rotation = BoomerangObject.transform.rotation;
            MyBoomerang.gameObject.GetComponent<BoomerangScript>().SetDestination(ProjectileEmitter.transform.position + (transform.forward * boomerangTravelDistance));
            MyBoomerang.gameObject.GetComponent<BoomerangScript>().SetOwner(this, inv);
        }
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
}
