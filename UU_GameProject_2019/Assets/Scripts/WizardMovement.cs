using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardMovement : MonoBehaviour
{
    private Animator m_animator;
    protected Vector3 playerPos, relativePlayerPos;
    bool attacking;
    float waitTime, speed, timer, detectionArea, MinRotateDistance, MaxRotateDistance;
    public int health;

    void Start()
    {
        waitTime = 4f;
        speed = 3;
        detectionArea = 20;
        MinRotateDistance = 5;
        MaxRotateDistance = 6;
        attacking = false;

        m_animator = GetComponent<Animator>();
        m_animator.SetBool("move_forward", true);
        m_animator.SetBool("idle_combat", false);
        m_animator.SetBool("attack_short_001", false);
    }

    void Update()
    {
        playerPos = GameObject.Find("ThirdPersonController").transform.position;
        relativePlayerPos = new Vector3(playerPos.x, transform.position.y, playerPos.z);
        
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Quaternion.LookRotation(relativePlayerPos - transform.position, Vector3.up).eulerAngles), Time.deltaTime * speed);

        //Moving to the player
        if (Vector3.Distance(transform.position, relativePlayerPos) < detectionArea && Vector3.Distance(transform.position, relativePlayerPos) > MaxRotateDistance && !attacking)
        {
            transform.position = Vector3.MoveTowards(transform.position, relativePlayerPos, speed * Time.deltaTime);

            m_animator.SetBool("move_forward", true);
            m_animator.SetBool("idle_combat", false);
        }

        //Moving away from the player
        else if (Vector3.Distance(transform.position, relativePlayerPos) < MinRotateDistance && !attacking)
        {
            transform.position = Vector3.MoveTowards(transform.position, relativePlayerPos, -speed * Time.deltaTime);

            m_animator.SetBool("move_forward", false);
            m_animator.SetBool("idle_combat", true);
        }

        //Moving around the player
        else
        {
            timer += Time.deltaTime;
            if (timer > waitTime + 1)
            {
                m_animator.SetBool("attack_short_001", false);
                timer = 0;
                attacking = false;
            }
            else if (timer > waitTime)
            {
                m_animator.SetBool("attack_short_001", true);
                attacking = true;
            }
            m_animator.SetBool("move_forward", false);
            m_animator.SetBool("idle_combat", true);
        }
    }

    public void TakeDamage()
    {
        m_animator.SetBool("move_forward", false);
        m_animator.SetBool("idle_combat", true);
        health--;

        if (health <= 0)
        {
            Die();
        }
        else
        {
            m_animator.SetBool("damage_001", true);
        }
    }

    void Die()
    {
        m_animator.SetTrigger("dead");
        Destroy(gameObject, 1);
    }
}
