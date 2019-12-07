using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalonMovement : MonoBehaviour
{
    private Animator m_animator;
    protected Vector2 zRange, xRange;
    protected Vector3 targetPosition;
    bool moving;
    float idleTime, speed, timer;

    //Temporary viuals
    public float AngleDifference;

    // Start is called before the first frame update
    void Start()
    {
        zRange = new Vector2(10, 23);
        xRange = new Vector2(13, 36);
        idleTime = 4f;
        speed = 1;
        moving = true;
        m_animator = GetComponent<Animator>();
        SetNewTargetPosition();
        m_animator.SetBool("Walk Forward", true);
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
            m_animator.SetBool("Walk Forward", false);

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

            //temp stuff (used for better rotation animation, but can't tell the difference between rotating to the left or the right)
            //Make a: if AngleDifference > 90 degrees or something then, first rotate_animation then movement
            AngleDifference = Quaternion.Angle(Quaternion.Euler(Quaternion.LookRotation(targetPosition - transform.position, Vector3.up).eulerAngles), transform.rotation);

            m_animator.SetBool("Walk Forward", true);
        }
    }

    protected void SetNewTargetPosition()
    {
        targetPosition = new Vector3(Random.Range(xRange.x, xRange.y), transform.position.y, Random.Range(zRange.x, zRange.y));
    }
}
