using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangMovement : MonoBehaviour
{
    public float speed = 10f;
    public float RotatingSpeed = 1024;
    bool returning = false;
    Vector3 Destination, playerRelativePos;

    void Start()
    {
        Destination = BoomerangThrowing.Destination;
    }

    void Update()
    {
        Vector3 playerPos = GameObject.Find("ThirdPersonController").transform.position;
        playerRelativePos = new Vector3(playerPos.x, transform.position.y, playerPos.z);

        transform.Rotate(0, 0, RotatingSpeed * Time.deltaTime, Space.Self); //rotates 'RotatingSpeed' degrees per second around y axis

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