using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMovement : MonoBehaviour, IShootable, IStunnable, IExplodable
{
    public GameObject ProjectileEmitter;
    public Vector3 ShootingForce;
    [SerializeField] float speed, idleTime, attackDelay, detectionArea, fleeDistance;
    public int health;

    private Animator m_animator;
    private Vector2 zRange, xRange;
    private Vector3 relativePlayerPos, targetPosition;
    bool moving, attacking;
    float idleTimer, attackTimer, rotatingSpeed;
    float AngleDifference, AngleDifference2, AngleDifferenceTarget;
    protected Transform playerTrans;

    void Start()
    {
        zRange = new Vector2(8, 20);
        xRange = new Vector2(14, 31);
        moving = true;

        m_animator = GetComponent<Animator>();
        m_animator.SetBool("Running", true);
        playerTrans = GameObject.Find("ThirdPersonController").transform;

        SetNewTargetPosition();
    }

    void Update()
    {
        relativePlayerPos = playerTrans.position;

        if (Vector3.Distance(transform.position, relativePlayerPos) < fleeDistance)
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
        MyArrow.transform.parent = transform;
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
        m_animator.SetBool("Running", false);
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
        Destroy(gameObject, 1);
    }

    public void getStabbed() { TakeDamage(); }
    public void getShot() { TakeDamage(); }
    public void getExploded() { TakeDamage(); }
    public void getStunned()
    {

    }
}
