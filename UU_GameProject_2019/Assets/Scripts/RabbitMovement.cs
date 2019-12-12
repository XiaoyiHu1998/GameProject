using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitMovement : MonoBehaviour
{
    // Start is called before the first frame update

    private string[] m_buttonNames = new string[] { "Idle", "Run", "Dead" };

    private Animator m_animator;

    protected Vector2 zRange, xRange;
    protected Vector3 targetPosition;
    bool moving;

    float idleTime, speed, timer;
    // Use this for initialization
    void Start()
    {
        zRange = new Vector2(10, 18);
        xRange = new Vector2(13, 28);
        idleTime = 4f;
        speed = 1;
        moving = true;
        m_animator = GetComponent<Animator>();
        SetNewTargetPosition();
        m_animator.SetInteger("AnimIndex", 1);
        m_animator.SetTrigger("Next");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, targetPosition) < 0.3f)
        {
            moving = false;
        }

        if (!moving)
        {
            timer += Time.deltaTime;
            m_animator.SetInteger("AnimIndex", 0);
            m_animator.SetTrigger("Next");

            if (timer > idleTime)
            {
                SetNewTargetPosition();
                moving = true;
            }
        }
        else
        {
            timer = 0;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Quaternion.LookRotation(targetPosition - transform.position, Vector3.up).eulerAngles), Time.deltaTime * speed);
            m_animator.SetInteger("AnimIndex", 1);
            m_animator.SetTrigger("Next");
        }
    }

    protected void SetNewTargetPosition()
    {
        targetPosition = new Vector3(Random.Range(xRange.x, xRange.y), transform.position.y, Random.Range(zRange.x, zRange.y));
    }
}
