using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMovement : MonoBehaviour
{
    public GameObject arrow;

    private Animator m_animator;
    protected Vector2 zRange, xRange;
    protected Vector3 relativePlayerPos, targetPosition;
    bool moving, attacking;
    float idleTime, speed, timer, rotatingSpeed, detectionArea;
    float AngleDifference, AngleDifference2, AngleDifferenceTarget;
    public int health;
    Transform playerPos;

    void Start()
    {
        zRange = new Vector2(10, 23);
        xRange = new Vector2(13, 36);
        idleTime = 4f;
        speed = 1;
        detectionArea = 12;
        moving = true;

        m_animator = GetComponent<Animator>();
        m_animator.SetBool("Running", true);
        playerPos = GameObject.Find("ThirdPersonController").transform;

        SetNewTargetPosition();
    }

    void Update()
    {
        
    }

    void Attack()
    {

    }

    void ShootArrow()
    {
        GameObject MyArrow = Instantiate(arrow, transform.position, transform.rotation) as GameObject;
    }

    protected void SetNewTargetPosition()
    {
        targetPosition = new Vector3(Random.Range(xRange.x, xRange.y), transform.position.y, Random.Range(zRange.x, zRange.y));
    }

    public void TakeDamage()
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
}
