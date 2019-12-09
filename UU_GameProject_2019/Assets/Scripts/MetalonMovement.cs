using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalonMovement : MonoBehaviour
{
    private Animator m_animator;
    protected Vector2 zRange, xRange;
    protected Vector3 playerPosition, relativePlayerPos;
    bool moving, attacking;
    float idleTime, speed, timer;
    Vector3 hornPosition;
    string MyName;

    //Temporary viuals
    public float AngleDifference;
    public Vector3 RelativePlayerPosition, targetPosition;

    void Start()
    {
        zRange = new Vector2(10, 23);
        xRange = new Vector2(13, 36);
        idleTime = 4f;
        speed = 1;
        moving = true;
        m_animator = GetComponent<Animator>();
        MyName = (this.gameObject).name;
        SetNewTargetPosition();
        m_animator.SetBool("Walk Forward", true);
    }

    void Update()
    {
        playerPosition = GameObject.Find("ThirdPersonController").transform.position;
        hornPosition = GameObject.Find(MyName + "/AttackingArea").transform.position;
        //relativePlayerPos = new Vector3(playerPosition.x, transform.position.y, playerPosition.z);
        float hornDistanceX = hornPosition.x - transform.position.x;
        float hornDistanceZ = hornPosition.z - transform.position.z;
        relativePlayerPos = new Vector3(playerPosition.x - hornDistanceX, transform.position.y, playerPosition.z - hornDistanceZ);
        RelativePlayerPosition = playerPosition;

        if (Vector3.Distance(transform.position, relativePlayerPos) < 1)
        {
            m_animator.SetBool("Walk Forward", false);
            m_animator.SetTrigger("Stab Attack");
        }

        //Moving to the player
        else if (Vector3.Distance(transform.position, relativePlayerPos) < 8)
        {
            transform.position = Vector3.MoveTowards(transform.position, relativePlayerPos, speed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Quaternion.LookRotation(relativePlayerPos - transform.position, Vector3.up).eulerAngles), Time.deltaTime * speed);
            m_animator.SetBool("Walk Forward", true);
        }

        //Arriving at destination
        else if (Vector3.Distance(transform.position, targetPosition) < 0.3f)
        {
            moving = false;
        }

        //Standing still
        else if (!moving)
        {
            timer += Time.deltaTime;
            m_animator.SetBool("Walk Forward", false);

            if (timer > idleTime)
            {
                SetNewTargetPosition();
                moving = true;
            }
        }
        
        //Moving towards destination
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
