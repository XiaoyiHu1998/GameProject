using UnityEngine;
using System.Collections;

public class ProjectileLaunchScript : MonoBehaviour
{
    public GameObject ProjectileEmitter;
    public GameObject Bomb;
    public GameObject Arrow;
    public string bombButton;
    public string arrowButton;
    
    public Vector3 BombForce; //X is forward motion, X upward and Z can correct for emitters being attached the wrong way around
    public Vector3 ArrowForce;

    public Weapon currentWeapon;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(bombButton) && currentWeapon == Weapon.Bombs) 
        {
            GameObject MyBomb = Instantiate(Bomb, ProjectileEmitter.transform.position, ProjectileEmitter.transform.rotation) as GameObject;
            MyBomb.GetComponent<Rigidbody>().AddRelativeForce(BombForce);
        }

        if (Input.GetKeyDown(arrowButton) && currentWeapon == Weapon.Bow) 
        {
            GameObject MyArrow = Instantiate(Arrow, ProjectileEmitter.transform.position, ProjectileEmitter.transform.rotation) as GameObject;
            MyArrow.GetComponent<Rigidbody>().AddRelativeForce(ArrowForce);
        }
    }
}

public enum Weapon
{
    None,
    Bow,
    Bombs,
    Boomerang,
    Sword
}