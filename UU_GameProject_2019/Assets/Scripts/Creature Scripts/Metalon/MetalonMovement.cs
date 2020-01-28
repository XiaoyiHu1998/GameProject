using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class MetalonMovement : MonoBehaviour, IExplodable, IShootable, IStabable, IStunable
{
    Animator m_animator;
    [SerializeField] Vector2 zRange, xRange;
    Vector3 playerPos, relativePlayerPos, targetPosition;
    bool moving, attacking;
    float idleTime, speed, timer, rotatingSpeed, detectionArea;
    float AngleDifference, AngleDifference2, AngleDifferenceTarget;
    Vector3 hornPosition;
    string MyName;
    public int health;
    public float maxHeight;

    Transform playerTransform;
    Transform hornTransform;

    void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.tag == "Player")
        {
            HealthScript healthScript = GameObject.FindObjectOfType(typeof(HealthScript)) as HealthScript;
            healthScript.TakeDamage();
        }
    }

    void Start()
    {
        idleTime = 4f;
        speed = 1;
        detectionArea = 8;
        moving = true;

        m_animator = GetComponent<Animator>();
        MyName = (this.gameObject).name;
        m_animator.SetBool("Walk Forward", true);

        SetNewTargetPosition();

        playerTransform = GameObject.Find("Player").transform;
        hornTransform = GameObject.Find(MyName + "/AttackingArea").transform;
    }

    void Update()
    {
        if (transform.position.y >= maxHeight)
            transform.position = new Vector3(transform.position.x, maxHeight - 0.5f, hornTransform.position.z);

        playerPos = playerTransform.position;
        hornPosition = hornTransform.position;
        float hornDistanceX = hornPosition.x - transform.position.x;
        float hornDistanceZ = hornPosition.z - transform.position.z;
        relativePlayerPos = new Vector3(playerPos.x - hornDistanceX, transform.position.y, playerPos.z - hornDistanceZ);

        //The first is only positive, the second also negative and the third is the difference with the targetPosition, not with the playerPositiom
        AngleDifference = Quaternion.Angle(Quaternion.Euler(Quaternion.LookRotation(playerPos - transform.position, Vector3.up).eulerAngles), transform.rotation);
        AngleDifference2 = Vector3.SignedAngle(playerPos - transform.position, transform.forward, Vector3.up);
        AngleDifferenceTarget = Vector3.SignedAngle(targetPosition - transform.position, transform.forward, Vector3.up);

        //Attacking the player
        if (Vector3.Distance(transform.position, relativePlayerPos) < 1)
            MoveAttack();

        //Rushing towards the player if the player is in front
        else if (AngleDifference < 8 && Vector3.Distance(transform.position, relativePlayerPos) < detectionArea)
            MoveRush();

        //Moving to the player
        else if (Vector3.Distance(transform.position, relativePlayerPos) < detectionArea)
            MoveBasic();

        //Standing still
        else if (!moving)
            MoveIdle();

        //Arriving at destination
        else if (Vector3.Distance(transform.position, targetPosition) < 0.3f)
            MoveArrival();

        //Moving towards destination
        else
            MoveDestination();
    }

    void MoveAttack()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Quaternion.LookRotation(relativePlayerPos - transform.position, Vector3.up).eulerAngles).normalized, Time.deltaTime * speed / 3);

        m_animator.SetBool("Run Forward", false);
        m_animator.SetBool("Walk Forward", false);
        m_animator.SetTrigger("Stab Attack");
    }

    void MoveRush()
    {
        transform.position = Vector3.MoveTowards(transform.position, relativePlayerPos, (speed * 4) * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Quaternion.LookRotation(relativePlayerPos - transform.position, Vector3.up).eulerAngles), Time.deltaTime * speed / 2);

        m_animator.SetBool("Walk Forward", false);
        m_animator.SetBool("Run Forward", true);
    }

    void MoveBasic()
    {
        transform.position = Vector3.MoveTowards(transform.position, relativePlayerPos, speed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Quaternion.LookRotation(relativePlayerPos - transform.position, Vector3.up).eulerAngles).normalized, Time.deltaTime * speed / 2);

        m_animator.SetBool("Run Forward", false);
        m_animator.SetBool("Walk Forward", true);
    }

    void MoveIdle()
    {
        timer += Time.deltaTime;
        m_animator.SetBool("Run Forward", false);
        m_animator.SetBool("Walk Forward", false);

        if (timer > idleTime)
        {
            SetNewTargetPosition();
            moving = true;
        }
    }

    void MoveArrival()
    {
        moving = false;
    }

    void MoveDestination()
    {
        timer = 0;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Quaternion.LookRotation(targetPosition - transform.position, Vector3.up).eulerAngles), Time.deltaTime* speed);

    m_animator.SetBool("Run Forward", false);
            m_animator.SetBool("Walk Forward", true);
    }

    protected void SetNewTargetPosition()
    {
        targetPosition = new Vector3(Random.Range(xRange.x, xRange.y), transform.position.y, Random.Range(zRange.x, zRange.y));
    }

    public void getExploded() { TakeDamage(); }
    public void getShot() { TakeDamage(); }
    public void getStabbed() { TakeDamage(); }
    public void getStunned() { TakeDamage(); }

    public void TakeDamage()
    {
        m_animator.SetBool("Run Forward", false);
        m_animator.SetBool("Walk Forward", false);
        health--;

        if (health <= 0)
        {
            Die();
        }
        else
        {
            m_animator.SetTrigger("Take Damage");
        }
    }

    void Die()
    {
        m_animator.SetTrigger("Die");
        GetComponent<DropScript>().DropItems();
        Destroy(gameObject, 1);
    }
}
