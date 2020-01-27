using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class WizardMovement : MonoBehaviour, IShootable, IExplodable, IStunable, IStabable
{
    public GameObject ProjectileEmitter, MagicBolt, Clone;
    public Vector3 ShootingForce;
    public float attackDelay, teleportDelay;
    public int health;
    private List<GameObject> CloneList = new List<GameObject>(3);

    private int maxHealth, cloneCount;
    private Animator m_animator;

    protected Vector3 playerPos, relativePlayerPos;
    protected Vector2 xRange, zRange;
    protected bool attacked, takingDamage, hittable, dying;
    protected float speed, RotateRadius;
    protected float timer, attackTimer, teleportTimer, damageTimer;
    protected Transform playerTransform;

    void Start()
    {
        speed = 3;
        RotateRadius = 7;
        maxHealth = health;
        cloneCount = 0;
        hittable = true;

        m_animator = GetComponent<Animator>();
        m_animator.SetBool("idle_combat", true);
        playerTransform = GameObject.Find("Player").transform;
    }

    void Update()
    {
        playerPos = playerTransform.position;
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
                attacked = false;
            }
            //Beginning attack animation
            else if (teleportTimer >= (teleportDelay * 1/3) && !attacked)
            {
                attacked = true;
                m_animator.SetTrigger("Attack");
            }

            teleportTimer += Time.deltaTime;
        }

        //Rotating around the player
        else if (health <= maxHealth / 2)
        {
            //Making clones
            for (int i = 1; i<=3; i++)
            {
                if (health <= (maxHealth * (4 - i) / 8) && cloneCount < i) { CreateClone(); }
            }

            //Stop spinning fast
            if (attackTimer > attackDelay + 2f)
            {
                attackTimer = 0;
                attacked = false;
                hittable = true;
            }
            //Spin really fast
            else if (attackTimer > attackDelay)
            {
                timer += Time.deltaTime * 20;
                hittable = false;
            }
            //Beginning attack animation
            else if (attackTimer > (attackDelay * 2/3) && !attacked)
            {
                attacked = true;
                m_animator.SetTrigger("Attack");
            }

            attackTimer += Time.deltaTime;
            timer += Time.deltaTime;
            transform.position = new Vector3((24 + Mathf.Sin(timer / 2) * RotateRadius), relativePlayerPos.y, ((16 + Mathf.Cos(timer / 2) * RotateRadius)));

            //Transform the clones
            for (int i = 0; i < cloneCount; i++)
            {
                float piRotation = Mathf.PI * (2 * (i + 1)) / (cloneCount + 1);    //One clone = 2/2;     Two clones = 2/3, 4/3;     Three clones = 2/4, 4/4, 6/4
                CloneList[i].transform.position = new Vector3((24 + Mathf.Sin((timer / 2) + piRotation) * RotateRadius), relativePlayerPos.y, ((16 + Mathf.Cos((timer / 2) + piRotation) * RotateRadius)));
                CloneList[i].transform.rotation = Quaternion.Lerp(CloneList[i].transform.rotation, Quaternion.Euler(Quaternion.LookRotation(relativePlayerPos - CloneList[i].transform.position, Vector3.up).eulerAngles), Time.deltaTime * speed);
            }
        }
    }

    void Teleport()
    {
        zRange = new Vector2(10, 22);
        xRange = new Vector2(17, 31);
        transform.position = new Vector3(Random.Range(xRange.x, xRange.y), transform.position.y, Random.Range(zRange.x, zRange.y));
        while (Vector3.Distance(transform.position, relativePlayerPos) < 4)
        {
            transform.position = new Vector3(Random.Range(xRange.x, xRange.y), transform.position.y, Random.Range(zRange.x, zRange.y));
        }
    }

    void ShootMagicBolt()
    {
        GameObject MyBolt = Instantiate(MagicBolt, ProjectileEmitter.transform.position, ProjectileEmitter.transform.rotation) as GameObject;
        MyBolt.GetComponent<Rigidbody>().AddRelativeForce(ShootingForce);
        //Rotate 90 degrees
        Vector3 rot = MyBolt.transform.rotation.eulerAngles;
        rot = new Vector3(rot.x, rot.y - 90, rot.z);
        MyBolt.transform.rotation = Quaternion.Euler(rot);
        Destroy(MyBolt, 5);
    }

    void CreateClone()
    {
        GameObject MyClone = Instantiate(Clone, transform.position, transform.rotation) as GameObject;
        MyClone.transform.parent = transform;
        CloneList.Add(MyClone);
        cloneCount++;
    }

    void TakeDamage()
    {
        if (hittable)
        {
            health--;
            if (health <= 0) { Die(); }
            else { takingDamage = true; }
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

    public void getStabbed() { TakeDamage(); }
    public void getShot() { TakeDamage(); }
    public void getExploded() { TakeDamage(); }
    public void getStunned() { TakeDamage(); }
}