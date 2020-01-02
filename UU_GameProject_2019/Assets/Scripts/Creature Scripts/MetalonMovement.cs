using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalonMovement : MonoBehaviour
{
    private Animator m_animator;
    private AnimationClip m_clip;
    protected Vector2 zRange, xRange;
    protected Vector3 playerPos, relativePlayerPos, targetPosition;
    bool moving, attacking;
    float idleTime, speed, timer, rotatingSpeed, detectionArea;
    float AngleDifference, AngleDifference2, AngleDifferenceTarget;
    protected Vector3 hornPosition;
    string MyName;
    public int health;

    void Start()
    {
        zRange = new Vector2(10, 23);
        xRange = new Vector2(13, 36);
        idleTime = 4f;
        speed = 1;
        detectionArea = 8;
        moving = true;

        m_animator = GetComponent<Animator>();
        MyName = (this.gameObject).name;
        m_animator.SetBool("Walk Forward", true);

        SetNewTargetPosition();
    }

    void Update()
    {
        playerPos = GameObject.Find("ThirdPersonController").transform.position;
        hornPosition = GameObject.Find(MyName + "/AttackingArea").transform.position;
        float hornDistanceX = hornPosition.x - transform.position.x;
        float hornDistanceZ = hornPosition.z - transform.position.z;
        relativePlayerPos = new Vector3(playerPos.x - hornDistanceX, transform.position.y, playerPos.z - hornDistanceZ);

        //The first is only positive, the second also negative and the third is the difference with the targetPosition, not with the playerPositiom
        AngleDifference = Quaternion.Angle(Quaternion.Euler(Quaternion.LookRotation(playerPos - transform.position, Vector3.up).eulerAngles), transform.rotation);
        AngleDifference2 = Vector3.SignedAngle(playerPos - transform.position, transform.forward, Vector3.up);
        AngleDifferenceTarget = Vector3.SignedAngle(targetPosition - transform.position, transform.forward, Vector3.up);

        //Attacking the player
        if (Vector3.Distance(transform.position, relativePlayerPos) < 1)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Quaternion.LookRotation(relativePlayerPos - transform.position, Vector3.up).eulerAngles).normalized, Time.deltaTime * speed / 3);

            m_animator.SetBool("Run Forward", false);
            m_animator.SetBool("Walk Forward", false);
            m_animator.SetTrigger("Stab Attack");
        }

        //Rushing towards the player if the player is in front
        else if (AngleDifference < 8 && Vector3.Distance(transform.position, relativePlayerPos) < detectionArea)
        {
            transform.position = Vector3.MoveTowards(transform.position, relativePlayerPos, (speed * 4) * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Quaternion.LookRotation(relativePlayerPos - transform.position, Vector3.up).eulerAngles), Time.deltaTime * speed / 2);
            
            m_animator.SetBool("Walk Forward", false);
            m_animator.SetBool("Run Forward", true);
        }

        //Moving to the player
        else if (Vector3.Distance(transform.position, relativePlayerPos) < detectionArea)
        {
            transform.position = Vector3.MoveTowards(transform.position, relativePlayerPos, speed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Quaternion.LookRotation(relativePlayerPos - transform.position, Vector3.up).eulerAngles).normalized, Time.deltaTime * speed / 2);
            
            m_animator.SetBool("Run Forward", false); 
            m_animator.SetBool("Walk Forward", true);
        }

        //Standing still
        else if (!moving)
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

        //Arriving at destination
        else if (Vector3.Distance(transform.position, targetPosition) < 0.3f)
        {
            moving = false;
        }
        
        //Moving towards destination
        else
        {
            timer = 0;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Quaternion.LookRotation(targetPosition - transform.position, Vector3.up).eulerAngles), Time.deltaTime * speed);

            m_animator.SetBool("Run Forward", false);
            m_animator.SetBool("Walk Forward", true);
        }
    }

    protected void SetNewTargetPosition()
    {
        targetPosition = new Vector3(Random.Range(xRange.x, xRange.y), transform.position.y, Random.Range(zRange.x, zRange.y));
    }

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
        Destroy(gameObject, 1);
    }
}
