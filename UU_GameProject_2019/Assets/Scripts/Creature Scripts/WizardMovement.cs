using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardMovement : MonoBehaviour, IShootable, IExplodable, IStunnable
{
    public GameObject ProjectileEmitter, MagicBolt, Clone;
    public Vector3 ShootingForce;
    public float attackDelay, teleportDelay;
    public int health;

    private GameObject Clone1, Clone2, Clone3;
    private int maxHealth, cloneCount;
    private Animator m_animator;

    protected Vector3 playerPos, relativePlayerPos;
    protected Vector2 xRange, zRange;
    protected bool attacking, takingDamage, dying;
    protected float speed, RotateRadius;
    protected float timer, attackTimer, teleportTimer, damageTimer;

    void Start()
    {
        speed = 3;
        RotateRadius = 4;
        maxHealth = health;
        cloneCount = 0;

        m_animator = GetComponent<Animator>();
        m_animator.SetBool("idle_combat", true);
    }

    void Update()
    {
        playerPos = GameObject.Find("ThirdPersonController").transform.position;
        relativePlayerPos = new Vector3(playerPos.x, transform.position.y, playerPos.z);

        //Looking at Player
        if (!dying)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Quaternion.LookRotation(relativePlayerPos - transform.position, Vector3.up).eulerAngles), Time.deltaTime * speed);
        }

        //Dying Animation
        if (dying) { }

        //Taking Damage Animation
        else if (takingDamage)
        {
            damageTimer += Time.deltaTime;
            m_animator.SetBool("damage_001", true);
            timer = Random.Range(0, 2 * Mathf.PI);
            if (damageTimer >= 0.5f)
            {
                damageTimer = 0;
                takingDamage = false;
                m_animator.SetBool("damage_001", false);
            }
        }

        //Teleporting around
        else if (health > maxHealth / 2)
        {
            //If the player is close, the wizard will teleport sooner
            if (Vector3.Distance(transform.position, relativePlayerPos) < 3)
            {
                teleportTimer += Time.deltaTime; 
            }
            //Teleport
            if (teleportTimer > teleportDelay)
            {
                Teleport();
                teleportTimer = 0;
            }
            //Shooting projectile
            else if (teleportTimer >= (teleportDelay / 2) + 0.8f && attacking)
            {
                m_animator.SetBool("attack_short_001", false);
                ShootMagicBolt();
                attacking = false;
            }
            //Beginning attack animation
            else if (teleportTimer >= teleportDelay / 2 && teleportTimer < (teleportDelay / 2) + 0.8f)
            {
                m_animator.SetBool("attack_short_001", true);
                attacking = true;
            }

            teleportTimer += Time.deltaTime;
        }

        //Rotating around the player
        else if (health <= maxHealth / 2)
        {
            //Making 1 Clone
            if (health <= (maxHealth * 3 / 8) && cloneCount < 1)
            {
                Clone1 = Instantiate(Clone, transform.position, transform.rotation) as GameObject;
                Clone1.transform.parent = transform;
                cloneCount = 1;
                Debug.Log("Made a Clone");
            }
            //Making a second Clone
            if (health <= maxHealth / 4 && cloneCount < 2)
            {
                Clone2 = Instantiate(Clone, transform.position, transform.rotation) as GameObject;
                Clone2.transform.parent = transform;
                cloneCount = 2;
                Debug.Log("Made a second Clone");
            }

            //Rotating
            if (attackTimer >= attackDelay + 0.8f && attacking)
            {
                m_animator.SetBool("attack_short_001", false);
                attackTimer = 0;
                ShootMagicBolt();
                attacking = false;
            }
            else if (attackTimer > attackDelay && attackTimer < attackDelay + 0.8f)
            {
                m_animator.SetBool("attack_short_001", true);
                attacking = true;
            }

            attackTimer += Time.deltaTime;
            timer += Time.deltaTime;
            transform.position = new Vector3((relativePlayerPos.x + Mathf.Sin(timer / 2) * RotateRadius), relativePlayerPos.y, ((relativePlayerPos.z + Mathf.Cos(timer / 2) * RotateRadius)));

            //Transform the position of the clones
            if (cloneCount == 1)
            {
                Clone1.transform.position = new Vector3((relativePlayerPos.x + Mathf.Sin((timer / 2) + Mathf.PI) * RotateRadius), relativePlayerPos.y, ((relativePlayerPos.z + Mathf.Cos((timer / 2) + Mathf.PI) * RotateRadius)));
                Clone1.transform.rotation = Quaternion.Lerp(Clone1.transform.rotation, Quaternion.Euler(Quaternion.LookRotation(relativePlayerPos - Clone1.transform.position, Vector3.up).eulerAngles), Time.deltaTime * speed);
            }
            else if (cloneCount == 2)
            {
                Clone1.transform.position = new Vector3((relativePlayerPos.x + Mathf.Sin((timer / 2) + (Mathf.PI * 2 / 3)) * RotateRadius), relativePlayerPos.y, ((relativePlayerPos.z + Mathf.Cos((timer / 2) + (Mathf.PI * 2 / 3)) * RotateRadius)));
                Clone1.transform.rotation = Quaternion.Lerp(Clone1.transform.rotation, Quaternion.Euler(Quaternion.LookRotation(relativePlayerPos - Clone1.transform.position, Vector3.up).eulerAngles), Time.deltaTime * speed);
                
                Clone2.transform.position = new Vector3((relativePlayerPos.x + Mathf.Sin((timer / 2) + (Mathf.PI * 4 / 3)) * RotateRadius), relativePlayerPos.y, ((relativePlayerPos.z + Mathf.Cos((timer / 2) + (Mathf.PI * 4 / 3)) * RotateRadius)));
                Clone2.transform.rotation = Quaternion.Lerp(Clone2.transform.rotation, Quaternion.Euler(Quaternion.LookRotation(relativePlayerPos - Clone2.transform.position, Vector3.up).eulerAngles), Time.deltaTime * speed);
            }
        }
    }

    void Teleport()
    {
        zRange = new Vector2(8, 20);
        xRange = new Vector2(14, 31);
        transform.position = new Vector3(Random.Range(xRange.x, xRange.y), transform.position.y, Random.Range(zRange.x, zRange.y));
        while (Vector3.Distance(transform.position, relativePlayerPos) < 6)
        {
            transform.position = new Vector3(Random.Range(xRange.x, xRange.y), transform.position.y, Random.Range(zRange.x, zRange.y));
        }
    }

    protected void ShootMagicBolt()
    {
        GameObject MyBolt = Instantiate(MagicBolt, ProjectileEmitter.transform.position, ProjectileEmitter.transform.rotation) as GameObject;
        MyBolt.GetComponent<Rigidbody>().AddRelativeForce(ShootingForce);
        //Rotate 90 degrees
        Vector3 rot = MyBolt.transform.rotation.eulerAngles;
        rot = new Vector3(rot.x, rot.y - 90, rot.z);
        MyBolt.transform.rotation = Quaternion.Euler(rot);
        Destroy(MyBolt, 5);
    }

    void TakeDamage()
    {
        health--;

        if (health <= 0)
        {
            Die();
        }
        else
        {
            takingDamage = true;
        }
    }

    void Die()
    {
        dying = true;
        m_animator.SetTrigger("dead");
        Destroy(gameObject, 5);
    }

    public void getShot()
    {
        TakeDamage();
    }

    public void getExploded()
    {
        TakeDamage();
    }

    public void getStunned()
    {
        TakeDamage();
    }
}