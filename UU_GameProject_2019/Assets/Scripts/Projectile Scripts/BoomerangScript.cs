using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangScript : MonoBehaviour
{
    public float speed = 10f;
    float RotatingSpeed, timer;
    bool returning;
    Vector3 Destination, playerRelativePos;

    void Start()
    {
        returning = false;
        RotatingSpeed = 1024;
        timer = 8;
        Destination = BoomerangThrowing.Destination;
    }

    void Update()
    {
        Vector3 playerPos = GameObject.Find("ThirdPersonController").transform.position;
        playerRelativePos = new Vector3(playerPos.x, transform.position.y, playerPos.z);

        //rotates a 'RotatingSpeed' degrees per second around y axis
        transform.Rotate(0, 0, RotatingSpeed * Time.deltaTime, Space.Self); 

        if (returning)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerRelativePos, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, Destination, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, Destination) < 3)
            {
                returning = true;
            }
        }
        //if the boomerang glitches out, then the timer will reach zero. when it does it returns the boomerang to the player
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision target)
    {
        IStunnable stunnable = target.gameObject.GetComponent<IStunnable>();
        IBoomerangDamagable damageable = target.gameObject.GetComponent<IBoomerangDamagable>();

        if (stunnable != null)
        {
            stunnable.getStunned();
            returning = true;
        }
        if (damageable != null)
        {
            damageable.GetDamaged();
            returning = true;
        }
        if (target.gameObject.name == "ThirdPersonController" && returning)
        {
            Destroy(gameObject);
        }
        else
        {
            returning = true;
        }
    }
}
