using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardMovement : MonoBehaviour, IShootable, IExplodable, IStunnable
{
    public GameObject ProjectileEmitter, MagicBolt, Clone;
    public Vector3 ShootingForce;
    public float attackDelay, teleportDelay;
    public int health;
    private List<GameObject> CloneList = new List<GameObject>(3);

    private GameObject Clone1, Clone2, Clone3;
    private int maxHealth, cloneCount;
    private Animator m_animator;

    protected Vector3 playerPos, relativePlayerPos;
    protected Vector2 xRange, zRange;
    protected bool attacking, takingDamage, hittable, dying;
    protected float speed, RotateRadius;
    protected float timer, attackTimer, teleportTimer, damageTimer;

    void Start()
    {
        speed = 3;
        RotateRadius = 4;
        maxHealth = health;
        cloneCount = 0;
        hittable = true;

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
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Quaternion.LookRotation(relativePlayerPos - transform.position, Vector3.up).eulerAngles), Time.deltaTime * 100);
        }

        //Dying Animation
        if (dying) { }

        //Taking Damage Animation
        else if (takingDamage)
        {
            hittable = false;
            attacking = false;
            damageTimer += Time.deltaTime;
            m_animator.SetBool("damage_001", true);

            if (damageTimer >= 1f)
            {
                damageTimer = 0;
                teleportTimer = teleportDelay + 1;
                takingDamage = false;
                m_animator.SetBool("damage_001", false);
                timer = Random.Range(0, 2 * Mathf.PI);
                hittable = true;
            }
        }

        //Teleporting around
        else if (health > maxHealth / 2)
        {
            //Teleport
            if (teleportTimer > teleportDelay)
            {
                Teleport();
                teleportTimer = 0;
            }
            //Shooting projectile
            else if (teleportTimer >= (teleportDelay * 1/3) + 0.8f && attacking)
            {
                m_animator.SetBool("attack_short_001", false);
                ShootMagicBolt();
                attacking = false;
            }
            //Beginning attack animation
            else if (teleportTimer >= (teleportDelay * 1/3) && teleportTimer < (teleportDelay * 1/3) + 0.8f)
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
            if (health <= (maxHealth * 3/8) && cloneCount < 1)
            {
                Clone1 = Instantiate(Clone, transform.position, transform.rotation) as GameObject;
                Clone1.transform.parent = transform;
                CloneList.Add(Clone1);
                cloneCount = 1;
                Debug.Log("Made a Clone");
            }
            //Making a second Clone
            if (health <= maxHealth / 4 && cloneCount < 2)
            {
                Clone2 = Instantiate(Clone, transform.position, transform.rotation) as GameObject;
                Clone2.transform.parent = transform;
                CloneList.Add(Clone2);
                cloneCount = 2;
                Debug.Log("Made a second Clone");
            }
            //Making a third Clone
            if (health <= (maxHealth * 1/8) && cloneCount < 3)
            {
                Clone3 = Instantiate(Clone, transform.position, transform.rotation) as GameObject;
                Clone3.transform.parent = transform;
                CloneList.Add(Clone3);
                cloneCount = 3;
                Debug.Log("Made a third Clone");
            }

            //Stop spinning fast
            if (attackTimer > attackDelay + 2f)
            {
                attackTimer = 0;
                hittable = true;
            }
            //Spin really fast
            else if (attackTimer > attackDelay)
            {
                timer += Time.deltaTime * 20;
                hittable = false;
            }
            //Shooting Projectile
            else if (attackTimer >= (attackDelay * 2/3) + 0.8f && attacking)
            {
                m_animator.SetBool("attack_short_001", false);
                attacking = false;
                ShootMagicBolt();
            }
            //Beginning attack animation
            else if (attackTimer > (attackDelay * 2/3) && attackTimer < (attackDelay * 2/3) + 0.8f)
            {
                m_animator.SetBool("attack_short_001", true);
                attacking = true;
            }

            attackTimer += Time.deltaTime;
            timer += Time.deltaTime;
            transform.position = new Vector3((relativePlayerPos.x + Mathf.Sin(timer / 2) * RotateRadius), relativePlayerPos.y, ((relativePlayerPos.z + Mathf.Cos(timer / 2) * RotateRadius)));

            //Transform the clones
            for (int i = 0; i < cloneCount; i++)
            {
                float piRotation = Mathf.PI * (2 * (i + 1)) / (cloneCount + 1);    //One clone = 2/2;     Two clones = 2/3, 4/3;     Three clones = 2/4, 4/4, 6/4
                CloneList[i].transform.position = new Vector3((relativePlayerPos.x + Mathf.Sin((timer / 2) + piRotation) * RotateRadius), relativePlayerPos.y, ((relativePlayerPos.z + Mathf.Cos((timer / 2) + piRotation) * RotateRadius)));
                CloneList[i].transform.rotation = Quaternion.Lerp(CloneList[i].transform.rotation, Quaternion.Euler(Quaternion.LookRotation(relativePlayerPos - CloneList[i].transform.position, Vector3.up).eulerAngles), Time.deltaTime * speed);
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
        if (hittable)
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
    }

    void Die()
    {
        dying = true;
        m_animator.SetTrigger("dead");
        for (int j = 0; j < cloneCount; j++)
        {
            Animator clone_animator = CloneList[j].GetComponent<Animator>();
            clone_animator.SetTrigger("dead");
        }
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