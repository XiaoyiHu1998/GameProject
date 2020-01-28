using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider))]
public class ArcherMovement : MonoBehaviour, IShootable, IStunable, IExplodable, IStabable
{
    public GameObject ProjectileEmitter;
    Animator m_animator;
    [SerializeField] Vector2 zRange, xRange;
    Vector3 relativePlayerPos, targetPosition;
    [SerializeField] Vector3 ShootingForce;
    [SerializeField] float speed, idleTime, attackDelay, detectionArea, fleeDistance;
    float idleTimer, attackTimer, stunnedTimer;
    Transform playerTrans;
    public int health;

    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_animator.SetBool("Running", true);
        playerTrans = GameObject.Find("Player").transform;

        SetNewTargetPosition();
    }

    void Update()
    {
        relativePlayerPos = playerTrans.position;
        stunnedTimer -= Time.deltaTime; // (stunnedTimer >= 0) == stunned,    (stunnedTimer < -1) == stunnable

        if (stunnedTimer >= 0) { } //Do nothing
        else if (Vector3.Distance(transform.position, relativePlayerPos) < fleeDistance)
        {
            Flee();
        }
        else
        {
            if (Vector3.Distance(transform.position, relativePlayerPos) < detectionArea)
            {
                attackTimer += Time.deltaTime;
                Attack();
            }
            else
            {
                Move();
            }
        }
        Vector3 currentEulerAngles = (transform.rotation).eulerAngles;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, currentEulerAngles.y, 0), Time.deltaTime * speed);
    }

    //Moving away from player
    void Flee()
    {
        transform.position = Vector3.MoveTowards(transform.position, relativePlayerPos, -speed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Quaternion.LookRotation(transform.position - relativePlayerPos, Vector3.up).eulerAngles).normalized, Time.deltaTime * speed);

        SetNewTargetPosition();
        m_animator.SetBool("Running", true);
    }

    //Walking around
    void Move()
    {
        //Arriving at destination
        if (Vector3.Distance(transform.position, targetPosition) < 0.5f)
        {
            m_animator.SetBool("Running", false);
        }

        //Standing still
        if (!m_animator.GetBool("Running"))
        {
            idleTimer += Time.deltaTime;
            if (idleTimer > idleTime)
            {
                SetNewTargetPosition();
                m_animator.SetBool("Running", true);
            }
        }
        //Moving towards destination
        else if (m_animator.GetBool("Running"))
        {
            idleTimer = 0;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Quaternion.LookRotation(targetPosition - transform.position, Vector3.up).eulerAngles), Time.deltaTime * speed);
        }
    }

    //Attacking loop
    void Attack()
    {
        m_animator.SetBool("Running", false);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Quaternion.LookRotation(relativePlayerPos - transform.position, Vector3.up).eulerAngles).normalized, Time.deltaTime * speed);
        
        if (attackTimer > attackDelay + 1f)
        {
            attackTimer = 0;
            m_animator.SetTrigger("Attack");
        }
    }

    //AttackAnimation will call this to shoot
    public void LaunchArrow(GameObject Arrow)
    {
        GameObject MyArrow = Instantiate(Arrow, ProjectileEmitter.transform.position, ProjectileEmitter.transform.rotation) as GameObject;

        //Change Rotation
        Vector3 rot = MyArrow.transform.rotation.eulerAngles;
        rot = new Vector3(rot.x, rot.y, rot.z + 90);
        MyArrow.transform.rotation = Quaternion.Euler(rot);
        
        MyArrow.GetComponent<Rigidbody>().AddRelativeForce(ShootingForce);
        Destroy(MyArrow, 5);
    }

    protected void SetNewTargetPosition()
    {
        targetPosition = new Vector3(Random.Range(xRange.x, xRange.y), transform.position.y, Random.Range(zRange.x, zRange.y));
        while (Vector3.Distance(targetPosition, relativePlayerPos) < fleeDistance)
        {
            targetPosition = new Vector3(Random.Range(xRange.x, xRange.y), transform.position.y, Random.Range(zRange.x, zRange.y));
        }
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
            m_animator.SetTrigger("TakeDamage");
        }
    }

    void Die()
    {
        m_animator.SetTrigger("Die");
        GetComponent<DropScript>().DropItems();
        Destroy(gameObject, 4);
    }

    //Creates particle effects for animations
    void OnEventFx(GameObject InEffect)
    {
        GameObject newSpell = Instantiate(InEffect, transform.position, transform.rotation) as GameObject;
        newSpell.transform.parent = transform;
        newSpell.transform.localPosition = new Vector3(0, 1.666f, 0.555f);
        Destroy(newSpell, 1.0f);
    }

    public void getStabbed() { TakeDamage(); }
    public void getShot() { TakeDamage(); }
    public void getExploded() { TakeDamage(); }
    public void getStunned()
    {
        if (stunnedTimer <= -1f)
        {
            m_animator.SetTrigger("GetStunned");
            stunnedTimer = 2.3f;
        }
    }
}
